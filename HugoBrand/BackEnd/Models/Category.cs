using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackEnd.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string Categories { get; set; }

        public ICollection<Item> Items { get; set; }
    }
}