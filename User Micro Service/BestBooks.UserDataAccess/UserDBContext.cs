using System;
using Microsoft.EntityFrameworkCore;
using BestBooks.UserMicroservice.DataAccess.DataModel;

namespace BestBooks.UserMicroservice.UserDataAccess
{
    public class UserDBContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }
    }
}
