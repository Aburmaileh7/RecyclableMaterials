using RecyclableMaterials.Models;

namespace RecyclableMaterials.Areas.Dashboard.ViewModel
{
    public class UserProductViewModel
    {
        public AppUserModel User { get; set; }

        public List<ProductModel> Products { get; set; }
    }
}
