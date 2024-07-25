namespace PowerPuffBE.Model;

using Enums;

public class ReactorChartDTO
{
    public string Time {get; set;}
    public int Value {get; set;}
    
    public ReactorStatusEnum Status {get; set;}
}