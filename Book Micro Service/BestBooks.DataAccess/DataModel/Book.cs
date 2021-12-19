using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BestBooks.BookMicroservice.DataAccess.DataModel
{
    [Table("Book")]
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }

        [Required]
        [Column(TypeName="varchar(200)")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName="varchar(500)")]
        public string Author { get; set; }

        [Required]
        [Column(TypeName="varchar(30)")]
        public string ISBN { get; set; }

        [Required]
        [Column(TypeName="datetime")]
        public DateTime DatePublished { get; set; }

        [Required]
        [Column(TypeName ="varchar(200)")]
        public string Category { get; set; }

        [Required]
        [Column(TypeName = "varchar(200)")]
        public string Publisher { get; set; }

        public string CoverImageURL { get; set; }
    }
}
