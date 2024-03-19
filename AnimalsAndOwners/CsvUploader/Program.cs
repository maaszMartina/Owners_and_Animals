// See https://aka.ms/new-console-template for more information
using DataAccess;

Console.WriteLine("Upload started!");

using (var context = new AnimalsAndOwnersDbContext())
{
    UploadOwnersFromCsv(context, @"Data\Gazda.csv");
    UploadAnimalsFromCsv(context, @"Data\HaziAllatok.csv");
}

Console.WriteLine("Press any key to exit...");
Console.ReadKey();

static void UploadOwnersFromCsv(AnimalsAndOwnersDbContext context, string filePath)
{
    try
    {
        using (var reader = new StreamReader(filePath))
        {
            //cut the header
            reader.ReadLine();

            int id = 0;

            while (!reader.EndOfStream)
            {
                id++;

                var line = reader.ReadLine();
                var values = line.Split(';');

                var owner = new Owner
                {
                    Id = id,
                    Name = values[0]
                };

                context.Owners.Add(owner);
            }

            context.SaveChanges();
            Console.WriteLine("Owners data uploaded successfully.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred while uploading owners data: {ex.Message}");
    }
}

static void UploadAnimalsFromCsv(AnimalsAndOwnersDbContext context, string filePath)
{
    try
    {
        using (var reader = new StreamReader(filePath))
        {
            //cut the header
            reader.ReadLine();

            int id = 0;

            while (!reader.EndOfStream)
            {
                id++;

                var line = reader.ReadLine();
                var values = line.Split(';');

                var animal = new Animal
                {
                    Id = id,
                    Name = values[0],
                    Species = values[1],
                    Age = int.Parse(values[2]),
                    OwnerId = int.Parse(values[3])
                };

                context.Animals.Add(animal);
            }

            context.SaveChanges();
            Console.WriteLine("Animals data uploaded successfully.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred while uploading animals data: {ex.Message}");
    }
}