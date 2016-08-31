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
        public Comment()
        {
            Date = DateTime.Now;
        }
        [Key]
        public string Id { get; set; }

        public int CocktailId { get; set; }

        public DateTime Date { get; set; }

        public string UserName { get; set; }

        [Required]
        public string Body { get; set; }
        
        public Cocktail Cocktail { get; set; }

        public ApplicationUser User { get; set; }
    }
}