using System;
using System.Linq;
using System.Collections.Generic;
using BestBooks.BookMicroservice.BusinessEntity;
using BestBooks.BookMicroservice.DataAccess;
using Microsoft.EntityFrameworkCore;
using AWSServiceGateway.S3ServiceGateway;
using BestBooks.BookMicroService.BLRepository.OutputObject;
using BestBooks.BookMicroService.BLRepository.AWSS3Config;
using Amazon;
using Amazon.S3;
using System.IO;
using System.Net;

namespace BestBooks.BookMicroService.BLRepository.BookService
{
    public class BookService : IBookService
    {
        BookDbContext _DbContext;
        S3Config _S3Config;

        public BookService(BookDbContext DbContext, S3Config AWSS3Config)
        {
            _DbContext = DbContext;
            _S3Config = AWSS3Config;
        }

        OutputResult<List<Book>> IBookService.GetListOfAvailableBooks()
        {
            var Books = _DbContext.Book.ToList();

            List<Book> BookList = Books.Select(B => new Book()
            {
                ID = B.ID,
                Name = B.Name,
                Author = B.Author,
                ISBN = B.ISBN,
                Category = B.Category,
                Publisher = B.Publisher,
                DatePublished = B.DatePublished,
                CoverImageURL= B.CoverImageURL,
                CoverImage= string.IsNullOrEmpty(B.CoverImageURL) ? null : DownloadBookCoverImage(B.CoverImageURL)
            }).ToList<Book>();

            return new OutputResult<List<Book>>()
            {
                Success = true,
                StatusCode = 200,
                RetuenMessage = BookList
            };
        }

        OutputResult<List<Book>> IBookService.GetListOfAvailableBooks(string Category, string Publisher)
        {
 
            var Books = _DbContext.Book.ToList();

            List <Book> BookList= Books.Select(B => new Book() {
                                                        ID = B.ID, 
                                                        Name = B.Name, 
                                                        Author = B.Author, 
                                                        ISBN = B.ISBN, 
                                                        Category=B.Category,
                                                        Publisher=B.Publisher,
                                                        DatePublished = B.DatePublished
                                                        }).ToList<Book>();

            if (!string.IsNullOrEmpty(Category) && !string.IsNullOrEmpty(Publisher))
            {
                BookList = BookList.Where(B1 => B1.Category == Category).Where(B2 => B2.Publisher == Publisher).ToList<Book>();
            }
            else if(!string.IsNullOrEmpty(Category))
            {
                BookList = BookList.Where(B1 => B1.Category == Category).ToList<Book>();
            }
            else if (!string.IsNullOrEmpty(Publisher))
            {
                BookList = BookList.Where(B1 => B1.Publisher == Publisher).ToList<Book>();
            }

            return new OutputResult<List<Book>>()
            {
                Success = true,
                StatusCode = 200,
                RetuenMessage = BookList
            };

        }

        OutputResult<List<string>> IBookService.GetListOfBookCategories()
        {
            List<string> CategoryList = _DbContext.Book.Select(Book => Book).Select(X => X.Category).Distinct<string>().ToList<string>();

            return new OutputResult<List<string>>()
            {
                Success = true,
                StatusCode = 200,
                RetuenMessage = CategoryList
            };
        }

        OutputResult<List<string>> IBookService.GetListOfPublishers()
        {
            List<string> PublisherList = _DbContext.Book.Select(Book => Book).Select(X => X.Publisher).Distinct<string>().ToList<string>();

            return new OutputResult<List<string>>()
            {
                Success = true,
                StatusCode = 200,
                RetuenMessage = PublisherList
            };
        }

        OutputResult<string> IBookService.SaveBook(Book NewBook)
        {
            long NewBookID;

            using var DbTransaction = _DbContext.Database.BeginTransaction();

            //add validation

            try
            {
                if (NewBook.ID.HasValue)
                {
                    var DbBook = _DbContext.Book.Where(B => B.ID == NewBook.ID).FirstOrDefault();

                    DbBook.Name = NewBook.Name;
                    DbBook.Author = NewBook.Author;
                    DbBook.ISBN = NewBook.ISBN;
                    DbBook.Category = NewBook.Category;
                    DbBook.Publisher = NewBook.Publisher;
                    DbBook.DatePublished = NewBook.DatePublished;

                    _DbContext.SaveChanges();

                    DbTransaction.Commit();

                    NewBookID = NewBook.ID.Value;

                    if (NewBook.CoverImage.Length>0)
                    {
                        S3OperationResult<S3ObjectUploadResult, S3ErrorResult> Result = UploadCoverImage(NewBook);
                        if (Result.Success)
                        {
                            S3InputDeleteObject S3DeleteRequestObject = new S3InputDeleteObject();
                            S3DeleteRequestObject.Region = RegionEndpoint.USEast1;
                            S3DeleteRequestObject.S3Bucket = _S3Config.S3BucketName;
                            S3DeleteRequestObject.ObjectFullName = NewBook.CoverImageURL.Substring(NewBook.CoverImageURL.LastIndexOf("\\"));

                            IS3ServiceGateway AWSS3Gateway = new S3ServiceGateway();
                            AWSS3Gateway.DeleteObjectAsync(S3DeleteRequestObject);

                            DbBook = _DbContext.Book.Where(B => B.ID == NewBookID).FirstOrDefault();

                            DbBook.CoverImageURL = Result.RetrnMessage.S3ObjectName;

                            _DbContext.SaveChanges();

                            DbTransaction.Commit();

                            return new OutputResult<string>()
                            {
                                Success = true,
                                StatusCode = 200,
                                RetuenMessage = NewBookID.ToString()
                            };
                        }
                        else
                        {
                            DbTransaction.Rollback();

                            return new OutputResult<string>()
                            {
                                Success = false,
                                StatusCode = 400,
                                RetuenMessage = Result.ErrorMessage.ErroeMessage
                            };
                        }
                    }
                    else
                    {
                        DbTransaction.Commit();

                        return new OutputResult<string>()
                        {
                            Success = true,
                            StatusCode = 200,
                            RetuenMessage = NewBookID.ToString()
                        };
                    }
                }
                else
                {
                    BestBooks.BookMicroservice.DataAccess.DataModel.Book NewBookData = new BookMicroservice.DataAccess.DataModel.Book();
                    NewBookData.Name = NewBook.Name;
                    NewBookData.Author = NewBook.Author;
                    NewBookData.ISBN = NewBook.ISBN;
                    NewBookData.Category = NewBook.Category;
                    NewBookData.Publisher = NewBook.Publisher;
                    NewBookData.DatePublished = NewBook.DatePublished;

                    _DbContext.Book.Add(NewBookData);
                    _DbContext.SaveChanges();

                    NewBookID = NewBookData.ID;

                    S3OperationResult<S3ObjectUploadResult, S3ErrorResult> Result = UploadCoverImage(NewBook);
                    if (Result.Success)
                    {
                        var DbBook = _DbContext.Book.Where(B => B.ID == NewBookID).FirstOrDefault();

                        DbBook.CoverImageURL = Result.RetrnMessage.S3ObjectName;

                        _DbContext.SaveChanges();

                        DbTransaction.Commit();

                        return new OutputResult<string>()
                        {
                            Success = true,
                            StatusCode = 200,
                            RetuenMessage = NewBookID.ToString()
                        };
                    }
                    else
                    {
                        DbTransaction.Rollback();

                        return new OutputResult<string>()
                        {
                            Success = false,
                            StatusCode = 400,
                            RetuenMessage = Result.ErrorMessage.ErroeMessage
                        }; 
                    }
                }
            }
            catch(Exception ex)
            {
                DbTransaction.Rollback();

                throw new DbUpdateException();
            }
        }

        private S3OperationResult<S3ObjectUploadResult, S3ErrorResult> UploadCoverImage(Book NewBook)
        {
            IS3ServiceGateway AWSS3Gateway = new S3ServiceGateway();
            return AWSS3Gateway.UploadObjectAsync(BuildS3ObjectUpload(NewBook)).Result;
        }

        private S3InputUploadObject BuildS3ObjectUpload(Book NewBook)
        {
            S3InputUploadObject S3SourceObject = new S3InputUploadObject();

            S3SourceObject.S3Bucket = _S3Config.S3BucketName;
            S3SourceObject.Region = RegionEndpoint.USEast1;
            S3SourceObject.InputObject = NewBook.CoverImage;
            S3SourceObject.ObjectName = NewBook.CoverImageFileName.Substring(0, (NewBook.CoverImageFileName.IndexOf(".")));
            S3SourceObject.ObjectExtension = NewBook.CoverImageFileName.Substring((NewBook.CoverImageFileName.IndexOf(".")) + 1);
            S3SourceObject.AccessToObject = S3CannedACL.BucketOwnerFullControl;
            S3SourceObject.RandomizeFileName = true;

            return S3SourceObject;
        }

        private byte[] DownloadBookCoverImage (string CoverImageURL)
        {
            IS3ServiceGateway AWSS3Gateway = new S3ServiceGateway();

            if (CoverImageURL.IndexOf("https://") >= 0)
            {
                CoverImageURL = CoverImageURL.Substring(CoverImageURL.IndexOf("https://") + 8);
                CoverImageURL = CoverImageURL.Substring(CoverImageURL.IndexOf("/") + 1);
            }

            S3InputGetObject S3GetRequestObject = new S3InputGetObject();
            S3GetRequestObject.Region = RegionEndpoint.USEast1;
            S3GetRequestObject.S3Bucket = _S3Config.S3BucketName;
            S3GetRequestObject.ObjectFullName = CoverImageURL;

            S3OperationResult<S3ObjectGetResult, S3ErrorResult> S3Object= AWSS3Gateway.GetObjectDataAsync(S3GetRequestObject).Result;
            return S3Object.RetrnMessage.S3ObjectData;
        }
    }
}
