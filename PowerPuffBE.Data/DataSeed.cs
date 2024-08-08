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

    public static int RandomTemperature(bool negative)
    {
        Random random = new Random();
        int defaultTemperature = 600;
        int temperatureMaxOffset = 600;
        int temperature;

        if (negative)
            temperature = random.Next(defaultTemperature - temperatureMaxOffset, defaultTemperature);
        else
            temperature = random.Next(defaultTemperature, defaultTemperature + temperatureMaxOffset);

        return temperature;
    }
    public static int RandomPowerOutput(bool negative)
    {
        Console.WriteLine(negative);
        Random random = new Random();
        int defaultOutput = 200;
        int outputMaxOffset = 200;
        int powerOutput;

        if (negative)
            powerOutput = random.Next(defaultOutput - outputMaxOffset, defaultOutput);
        else
            powerOutput = random.Next(defaultOutput, defaultOutput + outputMaxOffset);
        return powerOutput;
    }


    public static List<ReactorProductionChecksEntity> SeedProductionChecks(List<ReactorEntity> reactors)
    {
        List<ReactorProductionChecksEntity> checksGenerated = new List<ReactorProductionChecksEntity>();
        foreach (var reactor in reactors)
        {
            var daysToInsert = 10;
            List<ReactorProductionChecksEntity> generatedChecksForReactor = new List<ReactorProductionChecksEntity>();
            for (int i = 1; i <= daysToInsert; i++)
            {
                var date = i == 1 ? (DateTime.Now).Date : (DateTime.Now.AddDays(i-1)).Date;
                var listPerDay = Enumerable.Range(1, 24)
                    .Select(index => new ReactorProductionChecksEntity()
                    {}).ToList();
                int n = 1;
                listPerDay = Enumerable.Range(n, 24)
                .Select(index => new ReactorProductionChecksEntity()
                {
                    MeasureTime = index > 1 ? date.AddHours(index - 1) : date,
                    Temperature = RandomTemperature(index % 2 == 0),
                    PowerProduction = RandomPowerOutput( index % 2 == 1),
                    ReactorId = reactor.Id,
                }).ToList();


                generatedChecksForReactor.AddRange(listPerDay);
            }
            
            checksGenerated.AddRange(generatedChecksForReactor);
        }

        return checksGenerated;
    }
}