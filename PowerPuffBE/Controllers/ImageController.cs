namespace PowerPuffBE;

using Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Model;
using Service.Services;
using System.IO;

[Route("api/[controller]")]
[ApiController]
public class ImageController : ControllerBase
{
    private readonly IImageService _imageService;

    public ImageController(IImageService imageService)
    {
        _imageService = imageService;
    }

    [HttpGet]
    public async Task<IActionResult> GetImages()
    {
        var images = await _imageService.GetImages();
        return Ok(images);
    }

    [HttpGet]
    [Route("get-image/{name}")]
    public async Task<IActionResult> GetImageByName(string name)
    {
        try
        {
            var image = await _imageService.GetImageByName(name);
            return Ok(image);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
        
    }

    [HttpPost]
    [Route("upload")]
    public async Task<IActionResult> UploadImage([FromForm] IFormFile imageFile)
    {
        if (imageFile == null || imageFile.Length == 0)
            return BadRequest("File not send.");

        byte[] imageData;

        using (var ms = new MemoryStream())
        {
            await imageFile.CopyToAsync(ms);
            imageData = ms.ToArray();
        }

        string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(imageFile.FileName);

        var imageId = await _imageService.UploadImage(fileNameWithoutExtension, imageData);
        return Ok(imageId);
    }
    
    [HttpPost]
    [Route("upload-reactor/{id}")]
    public async Task<IActionResult> UploadReactorImage([FromRoute] Guid id, [FromForm] IFormFile imageFile)
    {
        if (imageFile == null || imageFile.Length == 0)
            return BadRequest("File not send.");

        byte[] imageData;

        using (var ms = new MemoryStream())
        {
            await imageFile.CopyToAsync(ms);
            imageData = ms.ToArray();
        }

        await _imageService.UploadForReactor(id, imageFile.Name ,imageData);
        return Ok();
    }
}