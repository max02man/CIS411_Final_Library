using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CIS411_Final_Library.Models
{
    public class Book
    {
        public int BookID { get; set; }

        public string UserId { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(50, ErrorMessage = "Title cannot be longer than 50 characters.")]
        [DisplayName("Book Title")]
        public string Title { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Authored Date")]
        public DateTime CreationDate { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        public int Quantity { get; set; }


        public string Votes { get; set; }

        ////Book Author
        //[DisplayName("Author")]
        //public string UserId { get; set; }

        //[ForeignKey("UserId")]
        //public virtual ApplicationUser ApplicationUser { get; set; }

        //Book Author

        //the book can be publish by one author
        // can be rate by many user
        // can be check by many user

        //for book list in checkouts
        [NotMapped]
        public SelectList BookList { get; set; }

        [DisplayName("Author")]
        public virtual ApplicationUser ApplicationUsers { get; set; }
        public virtual ICollection <Rate> Rates { get; set; }
        public virtual ICollection<Checkout> Checkouts { get; set; }


    }
}