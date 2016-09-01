using HangoverPartII.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HangoverPartII.ViewModels
{
    public class CocktailViewModel
    {
        public IQueryable<Cocktail> Cocktails { get; set; }

        public IList<Comment> Comments { get; set; }

        public int FirstCocktailId { get; set; }

        public int LastCocktailId { get; set; }

        public int NextCocktailId { get; set; }

        public int PreviousCocktailId { get; set; }

        public int Comment_Id { get; set; }

        public string Comments_Body { get; set; }

        public int Username_Id { get; set; }

        public Cocktail Cocktail { get; set; }
    }
}