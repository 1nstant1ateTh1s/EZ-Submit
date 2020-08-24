
namespace EZSubmitApp.Core.Configuration
{
    public class AspnetRunSettings
    {
        public ConnectionStrings ConnectionStrings { get; set; }
        public Seeding Seeding { get; set; }
    }

    public class ConnectionStrings
    {
        public string DefaultConnection { get; set; }
    }

    public class Seeding
    {
        public string DefaultAdminEmail { get; set; }
        public string DefaultUserEmail { get; set; }
    }
}
