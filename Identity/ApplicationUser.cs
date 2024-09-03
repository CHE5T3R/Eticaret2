using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace Eticaret2.Identity
{
    public class ApplicationUser:IdentityUser
    {
        public string Name {  get; set; }
        public string Surname {  get; set; }
    }
}