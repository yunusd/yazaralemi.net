using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace Yazaralemi.Models
{
    public static class Utils
    {
        public static string GetFullName(this IPrincipal user)
        {
            var fullNameClaim = ((ClaimsIdentity)user.Identity).FindFirst("FullName");
            if (fullNameClaim != null)
                return fullNameClaim.Value;

            return "";
        }
    }
}