using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecyclableMaterials.Data;
using RecyclableMaterials.Models;

namespace RecyclableMaterials.Controllers
{
    public class BidController : Controller
    {
        private RDBContext _dBContext;
        public BidController(RDBContext dBContext)
        {
            this._dBContext=dBContext;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int ProductId, decimal amount)
        {
            var product = await _dBContext.products.FindAsync(ProductId);
            if (product == null || amount <= 0)
            {
                return BadRequest("Invalid bid.");
            }

            var highestBid = _dBContext.Bids
                .Where(b => b.ProductId == ProductId)
                .OrderByDescending(b => b.Amount)
                .FirstOrDefault();

            if (highestBid != null && amount <= highestBid.Amount)
            {
                return BadRequest("Your bid must be higher than the current highest bid.");
            }

            var bid = new BidModel
            {
                ProductId = ProductId,
                Amount = amount,
                //UserId = Userid
                CreatedAt = DateTime.Now
            };

            _dBContext.Add(bid);
            await _dBContext.SaveChangesAsync();
            return RedirectToAction("Details", "Material", new { id = ProductId });
        }
    }
}
