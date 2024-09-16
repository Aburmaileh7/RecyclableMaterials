using Microsoft.AspNetCore.Http.HttpResults;
using RecyclableMaterials.Models;
using System.Xml.Linq;

namespace RecyclableMaterials.ViewModels
{
    public class CommentProductViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserEmail { get; set; }
        public string CommentText { get; set; }
        public string ProfilePictureUrl { get; set; }

        public ProductModel Product { get; set; }

    }
}

