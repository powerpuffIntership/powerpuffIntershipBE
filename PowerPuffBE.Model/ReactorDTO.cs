namespace PowerPuffBE.Model;

using Enums;

public class ReactorDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public string ImageContent { get; set; }
    
    public ReactorStatusDTO Status { get; set; }

    public IEnumerable<ReactorChartDTO> ReactorPowerProduction { get; set; }
    public IEnumerable<ReactorChartDTO> ReactorCoreTemperature { get; set; }

    public List<LinkModelDTO> Links { get; set; }
}