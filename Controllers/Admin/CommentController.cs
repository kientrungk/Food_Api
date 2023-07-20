using ApiWebFood.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiWebFood.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ApiDotNetContext _ContextCommenr;
        public CommentController(ApiDotNetContext ContextCommenr)
        {
            _ContextCommenr = ContextCommenr;
        }

        [Route("GettAllComment")]
        [HttpGet]
        public async Task<IActionResult> GetAllComment()
        {
            var conment = _ContextCommenr.Reviews.ToArray();
            return Ok(conment);
        }
        [Route("Addnewcomment")]
        [HttpPost]
        public async Task<IActionResult> AddNewComment(Review review)
        {
            if (review == null)
            {
                return BadRequest();
            }
            await _ContextCommenr.Reviews.AddAsync(review);
            await _ContextCommenr.SaveChangesAsync();
            return Ok("Comment Thanh công");
        }
        [Route("Deletecomment")]
        [HttpDelete]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var conment = _ContextCommenr.Reviews.FirstOrDefault(x => x.Id == id);
            if (conment == null) return BadRequest();
            _ContextCommenr.Reviews.Remove(conment);
            await _ContextCommenr.SaveChangesAsync();
            return Ok(conment);
        }
    }
}
