using Microsoft.AspNetCore.Identity;

namespace RecyclableMaterials.Models
{
    public class AppUserModel :IdentityUser
    {
      
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string ProfilePictureUrl { get; set; }  
            public DateTime DateOfBirth { get; set; }
            public string PhoneNumber { get; set; }
        

    }
}
