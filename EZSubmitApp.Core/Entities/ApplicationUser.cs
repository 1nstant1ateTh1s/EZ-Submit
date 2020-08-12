using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace EZSubmitApp.Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            //Profile = new Profile();
        }

        #region Properties
        public bool IsActive { get; set; }

        //[PersonalData]
        //public Profile Profile { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string BarNumber { get; set; }

        public ICollection<CaseForm> CaseForms { get; set; }
        #endregion
    }
}
