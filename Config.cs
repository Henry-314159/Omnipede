//namespace omnipede;

using System.Text.Json;

public class Config
{
    public static void LoadConfig(ref int maxDepth, ref int maxTimeMiliseconds)
    {
        //Initialization
        Dictionary<string, JsonElement> config;
        Dictionary<string, JsonElement> defaultConfig = new()
        {
            { "maxDepth", JsonSerializer.SerializeToElement(2) },
            { "maxTimeMiliseconds", JsonSerializer.SerializeToElement(15_000) }
        };
        
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Loading Config:");

        //File Stability
        try
        {
            Dictionary<string, JsonElement>? possibleConfig = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(File.ReadAllText(@".\omnipede-config.json"));
            if (possibleConfig != null)
            {
                config = possibleConfig;
            }
            else
            {
                throw new JsonException();
            }
        }
        catch(JsonException)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("    JsonException When Loading Config:");
            Console.WriteLine("        Delete The Config File To Reset Config");

            Console.ReadKey();
            Environment.Exit(1);
            config = new(){};
        }
        catch(FileNotFoundException)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("    FileNotFoundException When Loading Config:");
            Console.WriteLine("        Creating Config File");

            config = defaultConfig;
            using (var configFile = File.Create(@".\omnipede-config.json")){}
            File.WriteAllText(@".\omnipede-config.json", JsonSerializer.Serialize<Dictionary<string, JsonElement>>(defaultConfig)); 
        }

        if (config["maxDepth"].TryGetInt32(out maxDepth))
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"    maxDepth: {maxDepth}");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"    Error when loading config for maxDepth:");
            maxDepth = defaultConfig["maxDepth"].GetInt32();
            Console.WriteLine($"        Using default value of: {maxDepth}");
        }

        if (config["maxTimeMiliseconds"].TryGetInt32(out maxTimeMiliseconds))
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"    maxTimeMiliseconds: {maxTimeMiliseconds}");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"    Error when loading config for maxTimeMiliseconds:");
            maxTimeMiliseconds = defaultConfig["maxTimeMiliseconds"].GetInt32();
            Console.WriteLine($"        Using default value of: {maxTimeMiliseconds}");
        }
        


        return;
    }
}