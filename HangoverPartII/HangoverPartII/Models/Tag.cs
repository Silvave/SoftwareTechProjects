using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HangoverPartII.Models
{
    public class Tag
    {
        public Tag()
        {
            Cocktails = new HashSet<Cocktail>();
        }
        public int Id { get; set; }

        public string TagName { get; set; }

        public ICollection<Cocktail> Cocktails { get; set; }
    }
}