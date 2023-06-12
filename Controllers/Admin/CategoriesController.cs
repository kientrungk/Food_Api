using ApiWebFood.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiWebFood.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApiDotNetContext _contextcg;

        public CategoriesController(ApiDotNetContext contextcg)
        {
            _contextcg = contextcg;
        }

        //get list category
        [HttpGet]
        public async Task<IActionResult> GetListCategory()
        {
            var category = await _contextcg.Categories.ToListAsync();
            return Ok(category);
        }

        [HttpPost("add")]
        public async Task<IActionResult> addnewctg(string namecategori)
        {
            var cv = new Category()
            {
                Name = namecategori,
            };
            await _contextcg.Categories.AddAsync(cv);
            await _contextcg.SaveChangesAsync();
            return Ok(cv);
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> deletecatery(int id)
        {
            var ctg = await _contextcg.Categories.FindAsync(id);
            _contextcg.Categories.Remove(ctg);
            _contextcg.SaveChanges();
            return Ok(id);
        }
    }
}
