using Microsoft.AspNetCore.Identity;

namespace VerticalFarmingApi.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
   
}
