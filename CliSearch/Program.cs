using CsvExtracter;
using System.CommandLine;


var nameOption = new Option<string>("--name",
    description: "Name of the result")
{
    IsRequired = true
};


var rootCommand = new RootCommand("Search for a result");
rootCommand.AddOption(nameOption);


rootCommand.SetHandler(async (name) =>
{
    var cameraData = await CsvDataExtractor.ExtractCameraData();
    //Todo?: add fuzzy search?
    var camerasFound = cameraData.FindAll(x => x.Name.Contains(name));

    if (!camerasFound.Any())
    {
        Console.WriteLine("Camera not found");
    }
    else
    {
        Console.WriteLine($"Camera('s) found: ");
        foreach (var camera in camerasFound)
        {
            Console.WriteLine($"{camera.Id} | {camera.Name} | {camera.Latitude} | {camera.Longtitude}");
        }
    }
}, nameOption);


await rootCommand.InvokeAsync(args);
