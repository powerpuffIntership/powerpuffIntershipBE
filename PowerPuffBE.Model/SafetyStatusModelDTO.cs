using PowerPuffBE.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPuffBE.Model
{
    public class SafetyStatusModelDTO
    {
        public string sectionInfo { get; set; }
        public string StatusPowerProduction { get; set; }
        public string StatusCoreTemperature { get; set; }
    }
}
