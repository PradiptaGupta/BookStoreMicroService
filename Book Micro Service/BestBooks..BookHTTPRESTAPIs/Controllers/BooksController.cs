using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BestBooks.BookMicroService.BLRepository.BookService;
using BestBooks.BookMicroservice.BusinessEntity;
using BestBooks.BookMicroService.BLRepositoryFactory;
using BestBooks.BookMicroservice.DataAccess;
using BestBooks.BookMicroService.BLRepository.OutputObject;
using Microsoft.Extensions.Options;
using BestBooks.BookMicroService.BLRepository.AWSS3Config;
using System.ComponentModel.DataAnnotations;

namespace BestBooks.BookMicroService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        BookDbContext _DbContext;
        S3Config _S3Config;

        public BooksController(BookDbContext DbContext, IOptions<S3Config> AWSS3Config)
        {
            _DbContext = DbContext;
            _S3Config = AWSS3Config.Value;
        }

        [HttpGet]
        [Route("GetAllBooks")]
        public ActionResult<OutputResult<List<Book>>> GetListOfAllAvailableBooks()
        {
            return BLBookRepositoryFactory.BLBookServiceInstance(_DbContext,_S3Config).GetListOfAvailableBooks();
        }

        [HttpGet]
        [Route("SearchBooks")]
        public ActionResult<OutputResult<List<Book>>> GetListOfAvailableBooks(string Category = "", string Publisher = "")
        {
            return BLBookRepositoryFactory.BLBookServiceInstance(_DbContext, _S3Config).GetListOfAvailableBooks(Category, Publisher);
        }

        [HttpGet]
        [Route("GetAllCategories")]
        public ActionResult<OutputResult<List<string>>> GetAllCategories()
        {
            return BLBookRepositoryFactory.BLBookServiceInstance(_DbContext,_S3Config).GetListOfBookCategories();
        }

        [HttpGet]
        [Route("GetAllPublishers")]
        public ActionResult<OutputResult<List<string>>> GetAllPublishers()
        {
            return BLBookRepositoryFactory.BLBookServiceInstance(_DbContext,_S3Config).GetListOfPublishers();
        }

        [HttpPost]
        [Route("SaveBook")]
        public ActionResult<OutputResult<string>> SaveBook([FromBody] Book NewBook)
        {
            return BLBookRepositoryFactory.BLBookServiceInstance(_DbContext,_S3Config).SaveBook(NewBook);
        }
    }
}
