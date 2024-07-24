namespace PowerPuffBE.Service.Services;

using Data.Repositories;
using Mappers;
using Model;

public interface IReactorService
{
    Task<IEnumerable<ReactorDTO>> GetAllReactors(bool extended = false);
    Task<ReactorDTO> GetReactorWithDetails(Guid reactorId);
    Task<IEnumerable<ReactorDTO>> GetReactorWithImageList();
}

public class ReactorService : IReactorService
{
    private readonly IReactorRepository _reactorRepository;
    private readonly IReactorMapper _reactorMapper;
    private readonly IImageRepository _imageRepository;

    public ReactorService(
        IReactorRepository reactorRepository,
        IReactorMapper reactorMapper,
        IImageRepository imageRepository)
    {
        _reactorRepository = reactorRepository;
        _reactorMapper = reactorMapper;
        _imageRepository = imageRepository;
    }

    public async Task<IEnumerable<ReactorDTO>> GetAllReactors(bool extended = false)
    {
        var reactors = await _reactorRepository.GetAllReactors();
        return _reactorMapper.MapListToDTO(reactors.ToList());
    }

    public async Task<ReactorDTO> GetReactorWithDetails(Guid reactorId)
    {
        var reactor = await _reactorRepository.GetReactorExtendedById(reactorId);
        return _reactorMapper.MapToDTOWithDetails(reactor);
    }

    public async Task<IEnumerable<ReactorDTO>> GetReactorWithImageList()
    {
        var returnDtoList = new List<ReactorDTO>();
        var reactorsWithImages = await _reactorRepository.GetReactorImageList();
        var images = await _imageRepository.GetImages();
        foreach (var reactor in reactorsWithImages)
        {
            returnDtoList.Add(_reactorMapper.MapToDTOWithImage(reactor,
                images.FirstOrDefault(i => i.Id.Equals(reactor.ImageId))));
        }

        return returnDtoList;
    }
}