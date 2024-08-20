using Microsoft.AspNetCore.Mvc;
using RecyclableMaterials.Data;
using RecyclableMaterials.Models;

namespace RecyclableMaterials.Controllers
{
    public class RatingController : Controller
    {
        private readonly RDBContext _dbContext;

        public RatingController(RDBContext dbContext)
        {
            this._dbContext=dbContext;
        }

        [HttpPost]

        public async Task<IActionResult> Create(int productId,int stars)
        {
            if (stars < 1 || stars > 5)
            {
                return BadRequest("Invalid rating");
            }

            var rating = new RatingModel 
            {
                CreateAt = DateTime.Now,
                ProductId = productId,
                Stars= stars,
                //UserId =UserId /////////////////////////////////////////
            };

            _dbContext.Add(rating);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Details", "Product", new { id = productId });

        }
    }
}
