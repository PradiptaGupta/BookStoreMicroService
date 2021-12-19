﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BestBooks.BookMicroservice.DataAccess.DataModel
{
    [Table("BookPrice")]
    public class BookPrice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }

        [Required]
        [Column(TypeName ="Numeric(18,2")]
        public decimal Price { get; set; }

        [Required]
        [Column(TypeName ="Datetime")]
        public DateTime EffectiveFrom { get; set; }

        [Required]
        [ForeignKey("Book")]
        public long BookID { get; set; }

        public Book ApplicatetoBook { get; set; }
    }
}
