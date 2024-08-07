namespace PowerPuffBE.Service.Services;

using Data.Repositories;
using Mappers;
using Model;

public interface IReactorService
{
    Task<IEnumerable<ReactorDTO>> GetAllReactors(bool extended = false);
    Task<ReactorDTO> GetReactorWithDetails(Guid reactorId);
    Task<IEnumerable<ReactorDTO>> GetReactorWithImageList();
    Task<SafetyStatusModelDTO> GetAllReactorsToStatus(bool extended = false);
}

public class ReactorService : IReactorService
{
    private readonly IReactorRepository _reactorRepository;
    private readonly IReactorMapper _reactorMapper;
    private readonly IImageRepository _imageRepository;
    private readonly ISafetyStatusMapper _safetyStatusMapper;

    public ReactorService(
        IReactorRepository reactorRepository,
        IReactorMapper reactorMapper,
        IImageRepository imageRepository,
        ISafetyStatusMapper safetyStatusMapper)
    {
        _reactorRepository = reactorRepository;
        _reactorMapper = reactorMapper;
        _imageRepository = imageRepository;
        _safetyStatusMapper = safetyStatusMapper;
    }

    public async Task<IEnumerable<ReactorDTO>> GetAllReactors(bool extended = false)
    {
        var reactors = await _reactorRepository.GetAllReactors(extended);
        return _reactorMapper.MapListToDTO(reactors.ToList());
    }
    public async Task<SafetyStatusModelDTO> GetAllReactorsToStatus(bool extended = false)
    {
        var reactors = await _reactorRepository.GetAllReactors(extended);
        return _safetyStatusMapper.MapListToDTO(reactors.ToList());
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