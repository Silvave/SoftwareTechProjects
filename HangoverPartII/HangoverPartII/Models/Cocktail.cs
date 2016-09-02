using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HangoverPartII.Models
{
    public class Cocktail
    {
        public Cocktail()
        {
            Date = DateTime.Now;
        }
        [Key]
        public int Id { get; set; }

        public string Author_Id { get; set; }

        [ForeignKey("Author_Id")]
        public ApplicationUser Author { get; set; }

        public DateTime Date { get; set; }

        public string Image { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [StringLength(4000)]
        [Display(Name = "Body")]
        public string Body { get; set; }

        [DefaultValue(0)]
        public int NetLikeCount { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}