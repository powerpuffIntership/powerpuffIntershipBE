namespace PowerPuffBE.Model.Enums;

using System.ComponentModel;

public enum ReactorStatusEnum
{
    [Description("in-range")]
    InRange,
    [Description("out-of-range")]
    OutOfRange,
    [Description("critical")]
    Critical,
}