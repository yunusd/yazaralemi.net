﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Yazaralemi.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Required]
        [ForeignKey("Author")]
        [Display(Name = "Yazar")]
        public string AuthorId { get; set; }

        [Required]
        [Display(Name ="Kategori")]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Başlık")]
        public string Title { get; set; }

        [Display(Name = "İçerik")]
        public string Content { get; set; }

        [Required]
        public DateTime? CreatedAt { get; set; }

        public virtual ApplicationUser Author { get; set; }
        public virtual Category Category { get; set; }

    }
}