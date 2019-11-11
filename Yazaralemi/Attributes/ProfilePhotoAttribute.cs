using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Yazaralemi.Attributes
{
    public class ProfilePhotoAttribute : ValidationAttribute
    {
        /// <summary>
        /// Maximum file size in bytes
        /// </summary>
        public int MaxFileSize { get; set; }

        public override bool IsValid(object value)
        {
            bool isValid = true;

            if (value == null)
            {
                isValid = true;
            }

            HttpPostedFileBase file = (HttpPostedFileBase)value;


            if (file.ContentLength > MaxFileSize)
            {
                isValid = false;
            }

            if (!file.ContentType.StartsWith("image/"))
            {
                isValid = false;
            }
            return isValid;
        }
    }
}