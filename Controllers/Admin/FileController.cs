using ApiWebFood.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiWebFood.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {

        private readonly ApiDotNetContext _contextim;

        public FileController(ApiDotNetContext context)
        {
            _contextim = context;
        }
        [Route("ImgProduct")]
        [HttpPost("{id}")]
        public async Task<ActionResult> UpLoadFileImage(int id)
        {
            try
            {
                var file = Request.Form.Files;
                var ListResult = new List<string>();
                foreach (var item in file)
                {

                }
            }
            catch (Exception)
            {
                return BadRequest("Error When Upload File");
            }
        }
    }
}
