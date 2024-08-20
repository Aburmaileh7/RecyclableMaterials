using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecyclableMaterials.Data;
using RecyclableMaterials.Models;

namespace RecyclableMaterials.Controllers
{
    public class CommentController : Controller
    {
        private readonly RDBContext _dbContext;

        public CommentController(RDBContext dBContext)
        {
            this._dbContext=dBContext;
        }

        [HttpPost]
        public async Task< IActionResult> Create(int productId , string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return BadRequest("Comment cannot be empty");
            }
            var comment = new CommentModel
            {
                ProductId=productId,
                Text=text,
                CreateAt=DateTime.Now,
                //UserId=Userid //////////////////////////
            };

            _dbContext.Add(comment);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Details", "Product", new { id = productId });
           
        }
    }
}
