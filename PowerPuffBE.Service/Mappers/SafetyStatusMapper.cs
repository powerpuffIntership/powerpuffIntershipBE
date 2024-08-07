using PowerPuffBE.Data.Entities;
using PowerPuffBE.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PowerPuffBE.Service.Mappers
{
    public interface ISafetyStatusMapper
    {
        SafetyStatusModelDTO MapListToDTO(List<ReactorEntity> entityList);
    }
    public class SafetyStatusMapper : ISafetyStatusMapper
    {
        public SafetyStatusModelDTO MapListToDTO(List<ReactorEntity> entityList)
        {
            List<int> listOfTemperatures = new List<int>();
            List<int> listOfPowerProduction = new List<int>();
            
            foreach (ReactorEntity entity in entityList)
            {
                listOfTemperatures = entity.ProductionChecks.Select(temp => temp.Temperature).ToList();
                listOfPowerProduction = entity.ProductionChecks.Select(temp => temp.PowerProduction).ToList();
            }
            return new SafetyStatusModelDTO()
            {
                sectionInfo = "",
                StatusPowerProduction = GetProductionStatus(listOfTemperatures),
                StatusCoreTemperature = GetTemperatureStatus(listOfPowerProduction),
            };
        }
        private string GetTemperatureStatus(List<int> temperature)
        {
            int minTemp, maxTemp;
            string returnMessage = string.Empty;
            minTemp = temperature.Min();
            maxTemp = temperature.Max();
            if (minTemp < 250 || maxTemp >= 950) 
            { 
                returnMessage = "critical";
            }
            else if ((250 <= minTemp && minTemp < 400) || (800 <= maxTemp && maxTemp < 950))
            {
                returnMessage = "out of range";
            }
            else if((400 <= minTemp && minTemp < 800) || (400 <= maxTemp && maxTemp < 800))
            {
                returnMessage = "in range";
            }
            return returnMessage;
        }
        private string GetProductionStatus(List<int> production)
        {
            int minProd, maxProd;
            string returnMessage = string.Empty;
            minProd = production.Min();
            maxProd = production.Max();
            if (minProd < 10 || maxProd >= 300)
            {
                returnMessage = "critical";
            }
            else if ((10 <= minProd && minProd < 50) || (250 <= maxProd && maxProd < 300))
            {
                returnMessage = "out of range";
            }
            else if ((50 <= minProd && minProd < 250) || (50 <= maxProd && maxProd < 250))
            {
                returnMessage = "in range";
            }
            return returnMessage;

        }

    }
}
