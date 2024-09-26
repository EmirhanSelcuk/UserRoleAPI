using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserRoleAPI.Data;
using UserRoleAPI.Models;

namespace UserRoleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
         return await _context.Products.ToListAsync();
        }

        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult<List<Product>>> AddProduct(Product product)
        {
            // Kullanıcının admin olup olmadığını kontrol et
            if (!User.IsInRole("AdminPolicy"))
            {
                return BadRequest(new { message = "Yalnızca admin kullanıcılar ürün ekleyebilir." });
            }

            // Modelin geçerliliğini kontrol et
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return Ok(await _context.Products.ToListAsync());
        }
    }
}
