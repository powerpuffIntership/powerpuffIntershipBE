namespace PowerPuffBE.Data;

using Entities;

public static class DataSeed
{
    public static List<ReactorEntity> SeedReactors()
    {
        return Enumerable.Range(1, 3)
            .Select(index => new ReactorEntity()
            {
                Name = $"Reactor {index}",
                Description =
                    $"TEST {index} | Lorem ipsum dolor sit amet consectetur." +
                    $"Adipiscing non pulvinar placerat lorem ullamcorper magna." +
                    $"Pulvinar bibendum enim.",
                Status = 1
            }).ToList();

    }
    
    public static List<ReactorProductionChecksEntity> SeedProductionChecks(List<ReactorEntity> reactors)
    {
        List<ReactorProductionChecksEntity> checksGenerated = new List<ReactorProductionChecksEntity>();
        foreach (var reactor in reactors)
        {
            var daysToInsert = 10;

            Random random = new Random();
            List<ReactorProductionChecksEntity> generatedChecksForReactor = new List<ReactorProductionChecksEntity>();
            for (int i = 1; i <= daysToInsert; i++)
            {
                var date = i == 1 ? (DateTime.Now).Date : (DateTime.Now.AddDays(i-1)).Date;
                var listPerDay = Enumerable.Range(1, 24)
                    .Select(index => new ReactorProductionChecksEntity()
                    {
                        MeasureTime = index > 1 ? date.AddHours(index -1) : date,
                        Temperature = index % 2 == 0 ?  50 - GetTemperature(random) : 50 + GetTemperature(random) ,
                        PowerProduction = GetPowerProduction(random),
                        ReactorId = reactor.Id,
                    }).ToList();
                generatedChecksForReactor.AddRange(listPerDay);
            }
            
            checksGenerated.AddRange(generatedChecksForReactor);
        }

        return checksGenerated;
    }

    private static int GetTemperature(Random random)
    {
        int min = 5;
        int max = 25;

        return random.Next(min, max);
    }
    
    private static int GetPowerProduction(Random random)
    {
        int min = 100;
        int max = 300;

        return random.Next(min, max);
    }
}