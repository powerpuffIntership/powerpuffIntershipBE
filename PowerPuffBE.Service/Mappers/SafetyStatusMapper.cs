using PowerPuffBE.Data.Entities;
using PowerPuffBE.Model;
using PowerPuffBE.Model.Enums;
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
        const int temp250 = 250;
        const int temp400 = 400;
        const int temp800 = 800;
        const int temp950 = 950;

        const int prod10 = 10;
        const int prod50 = 50;
        const int prod250 = 250;
        const int prod300 = 300;

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
                StatusPowerProduction = GetProductionStatus(listOfPowerProduction),
                StatusCoreTemperature = GetTemperatureStatus(listOfTemperatures),
                sectionInfo = GetSectionInfoStatus(GetProductionStatus(listOfPowerProduction), GetTemperatureStatus(listOfTemperatures)),
            };
        }
        private string GetSectionInfoStatus(string ProductionStatus, string TemperatureStatus)
        {
            string returnSectionInfo = string.Empty;
            if((ProductionStatus.Equals(ReactorStatusEnum.Critical.ToString()) || ProductionStatus.Equals(ReactorStatusEnum.OutOfRange.ToString())) && 
                (TemperatureStatus.Equals(ReactorStatusEnum.Critical.ToString()) || TemperatureStatus.Equals(ReactorStatusEnum.OutOfRange.ToString())))
            {
                returnSectionInfo = "Core temperature and production power output has exeeded safe levels";
            }
            else if(ProductionStatus.Equals(ReactorStatusEnum.Critical.ToString()) || ProductionStatus.Equals(ReactorStatusEnum.OutOfRange.ToString()))
            {
                returnSectionInfo = "Production power output has exeeded safe levels";
            }
            else if(TemperatureStatus.Equals(ReactorStatusEnum.Critical.ToString()) || TemperatureStatus.Equals(ReactorStatusEnum.OutOfRange.ToString()))
            {
                returnSectionInfo = "Core temperature has exeeded safe levels";
            }
            return returnSectionInfo;
        }
        private string GetTemperatureStatus(List<int> temperature)
        {
            int minTemp, maxTemp;
            string returnMessage = string.Empty;
            minTemp = temperature.Min();
            maxTemp = temperature.Max();
            if (minTemp < temp250 || maxTemp >= temp950) 
            {
                returnMessage = ReactorStatusEnum.Critical.ToString();
            }
            else if ((temp250 <= minTemp && minTemp < temp400) || (temp800 <= maxTemp && maxTemp < temp950))
            {
                returnMessage = ReactorStatusEnum.OutOfRange.ToString();
            }
            else if((temp400 <= minTemp && minTemp < temp800) || (temp400 <= maxTemp && maxTemp < temp800))
            {
                returnMessage = ReactorStatusEnum.InRange.ToString();
            }
            return returnMessage;
        }
        private string GetProductionStatus(List<int> production)
        {
            int minProd, maxProd;
            string returnMessage = string.Empty;
            minProd = production.Min();
            maxProd = production.Max();
            if (minProd < prod10 || maxProd >= prod300)
            {
                returnMessage = ReactorStatusEnum.Critical.ToString();
            }
            else if ((prod10 <= minProd && minProd < prod50) || (prod250 <= maxProd && maxProd < prod300))
            {
                returnMessage = ReactorStatusEnum.OutOfRange.ToString();
            }
            else if ((prod50 <= minProd && minProd < prod250) || (prod50 <= maxProd && maxProd < prod250))
            {
                returnMessage = ReactorStatusEnum.InRange.ToString();
            }
            return returnMessage;

        }

    }
}
