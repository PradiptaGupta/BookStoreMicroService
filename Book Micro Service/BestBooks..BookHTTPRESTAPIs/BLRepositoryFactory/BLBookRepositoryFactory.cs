using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BestBooks.BookMicroService.BLRepository.BookService;
using BestBooks.BookMicroservice.DataAccess;
using BestBooks.BookMicroService.BLRepository.AWSS3Config;

namespace BestBooks.BookMicroService.BLRepositoryFactory
{
    internal static class BLBookRepositoryFactory
    {
        internal static IBookService BLBookServiceInstance(BookDbContext DbContext, S3Config AWSS3Config)
        {
            return new BookService(DbContext, AWSS3Config);
        }
    }
}

