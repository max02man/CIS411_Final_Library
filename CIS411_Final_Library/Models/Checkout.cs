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
    public class Checkout
    {
        public int CheckoutID { get; set; }


        public string UserId { get; set; }

        public int BookId { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayName("Checkout Date")]
        public DateTime CheckoutDate { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        public Int32 Quantity { get; set; }


        //need to make a method here or in controller to assign a default value based off checkout date
        //when a new record is saved
        [DataType(DataType.DateTime)]
        [DisplayName("Due Date")]
        public DateTime DueDate { get; set; }

        public virtual ApplicationUser ApplicationUsers { get; set; }       
        public virtual Book Books { get; set; }


    }
}