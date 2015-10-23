﻿namespace Advertisements.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        private ICollection<Advertisement> ads;

        public ApplicationUser()
        {
            this.ads = new HashSet<Advertisement>();
        }

        //[Required]
        public string Name { get; set; }

        [Display(Name = "Town")]
        public int? TownId { get; set; }

        public virtual Town Town { get; set; }

        public virtual ICollection<Advertisement> Advertisements
        {
            get { return this.ads; }
            set { this.ads = value; }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
