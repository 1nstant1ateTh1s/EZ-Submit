﻿
namespace EZSubmitApp.Core.DTOs
{
    public class ApplicationUserDto
    {
        public bool IsActive { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string BarNumber { get; set; }
    }
}
