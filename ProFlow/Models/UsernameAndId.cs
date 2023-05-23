using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProFlow.Models
{
    public class UsernameAndId
    {
        public string id { get; set; }
        public string name { get; set; }

        public List<SelectListItem> items { get; set; }
    }
}
