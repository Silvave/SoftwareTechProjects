using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MemeGen.Models
{
    public class Meme
    {
        public Meme()
        {
            Date = DateTime.Now;
        }
        [Key]
        public int Id { get; set; }

        public ApplicationUser Author { get; set; }

        public DateTime Date { get; set; }

        public string  Path { get; set; }

        [StringLength(200)]
        public string TopText { get; set; }

        [StringLength(200)]
        public string BottomText { get; set; }
    }
}