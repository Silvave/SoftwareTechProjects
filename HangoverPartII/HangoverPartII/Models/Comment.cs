using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HangoverPartII.Models
{
    public class Comment
    {
        public string Id { get; set; }

        public string CocktailId { get; set; }

        public DateTime DateTime { get; set; }

        public string UserName { get; set; }

        [Required]
        public string Body { get; set; }

        [DefaultValue(false)]
        public bool Deleted { get; set; }

        public Cocktail Cocktail { get; set; }
    }
}