using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CIS411_Final_Library.Models
{
	public class BookListReview
	{
      
            public int Id { get; set; }

            public string Title { get; set; }

            public DateTime CreationDate { get; set; }

            public int Quantity { get; set; }
            
            public Double AveRate { get; set; }

            public int Rate { get; set; }
        
    }
}