using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BestBooks.Web.Models;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using BestBooks.ServiceGateway;
using BestBooks.Web.GatewayFactory;
using BestBooks.ViewModel;
using System.IO;

namespace BestBooks.Web.Controllers
{
    [Route("")]
    [Route("Books")]
    public class BooksController : Controller
    {
        private IServiceGatewayFactory _ServiceGatewayFactory;

        public BooksController(IServiceGatewayFactory ServiceGatewayFactory)
        {
            _ServiceGatewayFactory = ServiceGatewayFactory;
        }

        [Route("")]
        [Route("Index")]
        [HttpGet]
        public IActionResult BookList([FromQuery] string Category, [FromQuery] string Publisher)
        {
            BookShop BestBookShop = new BookShop();

            BestBookShop.Books= _ServiceGatewayFactory.BookServiceGatewayInstance().GetBookList(Category, Publisher);

            BestBookShop.BookCategories = _ServiceGatewayFactory.BookServiceGatewayInstance().GetBookCategoryList();

            BestBookShop.BookPublishers = _ServiceGatewayFactory.BookServiceGatewayInstance().GetLBookPublisherList();

            return View(BestBookShop);
        }

        [Route("AddBook")]
        [HttpGet]
        public IActionResult AddBook()
        {
           // string strName=HttpContext.Request.Form["Name"].ToString();
           return View();
        }

        [Route("SaveBook")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveBook(BestBooks.ViewModel.Book NewBook)
        {
            if (ModelState.IsValid)
            {
                if (NewBook.CoverImageFile != null)
                {
                    NewBook.CoverImageFileName=Path.GetFileName(NewBook.CoverImageFile.FileName);
                    MemoryStream BookCoverImageStream = new MemoryStream();
                    NewBook.CoverImageFile.CopyTo(BookCoverImageStream);
                    NewBook.CoverImage = BookCoverImageStream.ToArray();
                }

                _ServiceGatewayFactory.BookServiceGatewayInstance().SaveBook(NewBook);
            }
 
            return RedirectToAction(nameof(Index));
        }
    }
}
