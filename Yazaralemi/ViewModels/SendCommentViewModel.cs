using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Yazaralemi.ViewModels
{
    public class SendCommentViewModel
    {
        public int? ParentId { get; set; }

        [Required]
        public int PostId { get; set; }

        [Required]
        public string AuthorName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage ="Lütfen geçerli bir e-mail adresi giriniz!")]
        public string AuthorEmail { get; set; }

        [Required]
        [StringLength(1500)]
        public string Content { get; set; }
    }
}