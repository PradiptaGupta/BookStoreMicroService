using System;
using System.Collections.Generic;

namespace BestBooks.UserMicroservice.BusinesEntity
{
    public class User
    {
        public long ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public List<Role> Roles { get; set; }
    }
}
