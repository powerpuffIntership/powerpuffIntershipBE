namespace PowerPuffBE.Service.Services;

using Data.Entities;
using Data.Repositories;
using Model;

public interface IImageService
{
    Task<IEnumerable<ImageDTO>> GetImages();

    Task<ImageDTO> GetImageByName(string name);

    Task<Guid> UploadImage(string name, byte[] imageData);

    Task UploadForReactor(Guid reactorId, string fileName, byte[] imageData);
    
}
public class ImageService : IImageService
{
    private readonly IImageRepository _imageRepository;
    public readonly IReactorRepository _reactorRepository;

    public ImageService(
        IImageRepository imageRepository,
        IReactorRepository reactorRepository)
    {
        _imageRepository = imageRepository;
        _reactorRepository = reactorRepository;
    }

    public async Task<IEnumerable<ImageDTO>> GetImages()
    {
        var images = await _imageRepository.GetImages();
        try
        {
            return images.Select(i =>
            {
                return new ImageDTO()
                {
                    Id = i.Id,
                    Name = i.Name,
                    ImageContent = Convert.ToBase64String(i.Image)
                };
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }

    public async Task<ImageDTO> GetImageByName(string name)
    {
        var image = await _imageRepository.GetImageByName(name);
        if (image == null)
        {
            return null;
        }

        try
        {
            return new ImageDTO()
            {
                Id = image.Id,
                Name = image.Name,
                ImageContent = image == null
                ? "No image found"
                : "data:image/png;base64," + Convert.ToBase64String(image.Image)
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Guid> UploadImage(string name, byte[] imageData)
    {
        return await _imageRepository.Add(new ImageEntity()
        {
            Name = name,
            Image = imageData
        });
    }

    public async Task UploadForReactor(Guid reactorId,string fileName, byte[] imageData)
    {
        var reactor = await _reactorRepository.GetReactorExtendedById(reactorId);
        reactor.ImageId = await _imageRepository.Add(new ImageEntity() { Name = fileName, Image = imageData });
        await _reactorRepository.Update(reactor);
    }
}