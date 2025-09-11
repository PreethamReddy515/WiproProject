using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class FilesController : ControllerBase
{
    [HttpPost("upload")]
    public IActionResult Upload(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file selected.");

        // Save file to wwwroot/uploads
        var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
        if (!Directory.Exists(uploadsPath)) Directory.CreateDirectory(uploadsPath);

        var filePath = Path.Combine(uploadsPath, file.FileName);
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            file.CopyTo(stream);
        }

        var url = $"/uploads/{file.FileName}"; // relative URL
        return Ok(new { attachmentUrl = url });
    }
}
