using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecyclableMaterials.Data;
using RecyclableMaterials.Models;

namespace RecyclableMaterials.Controllers
{
    public class CommentController : Controller
    {
        private readonly RDBContext _dbContext;
        private readonly UserManager<AppUserModel> _userManager;

        public CommentController(RDBContext dBContext, UserManager<AppUserModel> userManager )
        {
           _dbContext=dBContext;
           _userManager=userManager;
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(int productId, string text)
        {
            var userId = _userManager.GetUserId(User);

            if (string.IsNullOrEmpty(text))
            {
                return BadRequest("Comment cannot be empty");
            }

            var comment = new CommentModel
            {
                ProductId = productId,
                Text = text,
                CreateAt = DateTime.Now,
                UserId = userId
            };

            _dbContext.Comments.Add(comment);


            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Details", "Product", new { id = productId });
        }


    }
}
