using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UploadController : ControllerBase
{
    public class UploadForm
    {
        [FromForm(Name = "id")]
        public Guid Id { get; set; }

        [FromForm(Name = "file")]
        public IFormFile File { get; set; } = null!;
    }

    [HttpPost("upload")]
    public IActionResult Upload([FromForm] UploadForm form)
    {
        return Ok(new
        {
            Message = "File uploaded successfully",
            form.Id,
            form.File.FileName,
            Size = form.File.Length
        });
    }
}