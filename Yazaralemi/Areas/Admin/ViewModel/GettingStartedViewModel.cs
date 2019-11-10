using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Yazaralemi.Areas.Admin.ViewModel
{
    public class GettingStartedViewModel
    {
        [Required(ErrorMessage = "{0} alanı gerekli!")]
        [Display(Name = "Site Adı")]
        public string SiteName { get; set; }

        [Required(ErrorMessage = "{0} alanı gerekli!")]
        [Display(Name = "Hakkında")]
        public string About { get; set; }

        [Display(Name = "İletişim")]
        public string Contact { get; set; }

        [Required(ErrorMessage = "{0} gerekli!")]
        [Display(Name = "Footer alanı")]
        public string Footer { get; set; }

        [StringLength(25)]
        [Required(ErrorMessage = "{0} gerekli!")]
        public string FirstName { get; set; }

        [StringLength(25)]
        [Required(ErrorMessage = "{0} gerekli!")]
        public string LastName { get; set; }
    }
}