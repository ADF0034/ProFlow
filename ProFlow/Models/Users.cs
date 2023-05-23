using System.ComponentModel;

namespace ProFlow.Models
{
    public class Users
    {
        [DisplayName("User Id")]
        public string Id { get; set; }
        [DisplayName("User name")]
        public string Name { get; set; }
        [DisplayName("User rolle")]
        public string Role { get; set; }
    }
}
