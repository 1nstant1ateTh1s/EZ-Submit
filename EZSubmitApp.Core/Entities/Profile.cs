using EZSubmitApp.Core.Entities.Base;

namespace EZSubmitApp.Core.Entities
{
    public class Profile : IntBaseEntity
    {
        public Profile()
        {
        }

        #region Properties
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string BarNumber { get; set; }
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
        #endregion
    }
}
