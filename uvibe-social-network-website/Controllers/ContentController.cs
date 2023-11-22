using Microsoft.AspNetCore.Mvc;

namespace uvibe_social_network_website.Controllers
{
    public class ContentController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> UploadImage([FromRoute] Guid id,
            [FromForm(Name = "Image")] IFormFile file)
        {
            return Ok();
        }
    }
}
