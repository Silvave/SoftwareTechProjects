using HangoverPartII.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HangoverPartII.ViewModels
{
    public class CocktailViewModel
    {
        public IList<Cocktail> Cocktails { get; set; }

        public IList<Comment> Comments { get; set; }

        public Cocktail FirstCocktailId { get; set; }

        public Cocktail LastCocktailId { get; set; }

        public Cocktail Cocktail { get; set; }
    }
}