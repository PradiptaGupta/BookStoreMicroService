using System;
using Microsoft.EntityFrameworkCore;
using BestBooks.BookMicroservice.DataAccess.DataModel;

namespace BestBooks.BookMicroservice.DataAccess
{
    public class BookDbContext : DbContext
    {
        public BookDbContext() : base()
        {

        }
        public BookDbContext(DbContextOptions<BookDbContext> Options) : base(Options)
        {
        }

        public DbSet<Book> Book { get; set; }

        public DbSet<BookPrice> BookPrice { get; set; }

        public DbSet<BookDiscount> BookDiscount { get; set; }

    }
}