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
    public class Rate
    {
        public int RateID { get; set; }

        public string UserId { get; set; }

        public int BookId { get; set; }


        //Rating 1-10
        [Range(1, 10, ErrorMessage = "Please enter valid rating")]
        [Required(ErrorMessage = "Rating is Required")]
        public int Stars { get; set; }

        public string Comment { get; set; }

        //users can rate many book
        //books can be rate by many user

        public virtual ApplicationUser ApplicationUsers { get; set; }
        //public virtual ICollection <Book> Books { get; set; }

    }
}