using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eticaret2.Identity
{
    public class ApplicationRole:IdentityRole
    {
        public String Description { get; set; }
        public ApplicationRole() { }
        public ApplicationRole(String rolename, String description)
        {
            this.Description = description;

        }
    }
}