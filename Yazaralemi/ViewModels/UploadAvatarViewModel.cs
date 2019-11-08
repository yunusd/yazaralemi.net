using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Yazaralemi.Attribute;

namespace Yazaralemi.ViewModels
{
    public class UploadAvatarViewModel
    {
        public string Photo { get; set; }

        [Required(ErrorMessage = "Resim dosyası seçmediniz.")]
        [ProfilePhoto(MaxFileSize = 1000000, ErrorMessage = "1Mb'dan küçük bir resim dosyası giriniz!")]
        public HttpPostedFileBase File { get; set; }
    }
}