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
            int min = 30;
            int max = 90;
            List<ReactorProductionChecksEntity> generatedChecksForReactor = new List<ReactorProductionChecksEntity>();
            for (int i = 1; i <= daysToInsert; i++)
            {
                var date = i == 1 ? (DateTime.Now).Date : (DateTime.Now.AddDays(i-1)).Date;
                var listPerDay = Enumerable.Range(1, 24)
                    .Select(index => new ReactorProductionChecksEntity()
                    {
                        MeasureTime = index > 1 ? date.AddHours(index -1) : date,
                        Temperature = random.Next(min, max),
                        PowerProduction = random.Next(min, max),
                        ReactorId = reactor.Id,
                    }).ToList();
                generatedChecksForReactor.AddRange(listPerDay);
            }
            
            checksGenerated.AddRange(generatedChecksForReactor);
        }

        return checksGenerated;
    }
}