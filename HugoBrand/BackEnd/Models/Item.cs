using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackEnd.Models
{
    public class Item
    {
        public int ItemID { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Details { get; set; }

        //Setting foreign key for category
        public int CategoryID { get; set; }
        public Category Category { get; set; }
    }
}