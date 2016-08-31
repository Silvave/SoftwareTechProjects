using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HangoverPartII.Models
{
    public class Category
    {
        public Category()
        {
            Cocktails = new HashSet<Cocktail>();
        }
        public int Id { get; set; }

        public string CategoryName { get; set; }

        public ICollection<Cocktail> Cocktails { get; set; }
    }
}