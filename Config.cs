namespace omnipede;

using System.Text.Json;

public class Config
{
    public static Dictionary<string, string> LoadConfig(ref int depth, ref  string filePath, ref bool errorDetection, ref int lineLength, ref int movesSearchedFrequency)
    {
        //Initialization
        Dictionary<string, string> config;
        Dictionary<string, string> defaultConfig = new()
        {
            { "depth", "2" },
            { "lineLength", "1" },
            { "movesSearchedFrequency", "1000" },
            { "filePath", @".\omnipede-input.json" },
            { "errorDetection", "false" },
        };
        
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Loading Config:");

        //File Stability
        try
        {
            Dictionary<string, string>? possibleConfig = JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(@".\omnipede-config.json"));
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
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("     JsonException When Loading Config:");
            Console.WriteLine("         Remaking Config File");

            config = new(){};
            File.WriteAllText(@".\omnipede-config.json", JsonSerializer.Serialize<Dictionary<string, string>>(config));          
        }
        catch(FileNotFoundException)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("     FileNotFoundException When Loading Config:");
            Console.WriteLine("         Creating Config File");

            config = new(){};
            using (var configFile = File.Create(@".\omnipede-config.json")){}
            File.WriteAllText(@".\omnipede-config.json", JsonSerializer.Serialize<Dictionary<string, string>>(config)); 
        }

        //missing and unnessasry options
        bool difrent;
        foreach (var configItem in config)
        {
            difrent = true;
            foreach (var defaultConfigItem in defaultConfig)
            {
                if (defaultConfigItem.Key == configItem.Key)
                {
                    difrent = false;
                }
            }
            if (difrent)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("     Unneccecary Config Option \""+configItem.Key+"\":");
                Console.WriteLine("         Removing Unneccecary Config Option");
                config.Remove(configItem.Key);
                Console.WriteLine("         Updating Config File With Changes");
                File.WriteAllText(@".\omnipede-config.json", JsonSerializer.Serialize<Dictionary<string, string>>(config));    
            }
        }

        foreach (var defaultConfigItem in defaultConfig)
        {
            difrent = true;
            foreach (var configItem in config)
            {
                if (defaultConfigItem.Key == configItem.Key)
                {
                    difrent = false;
                }
            }
            if (difrent)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("     Missing Config Option \""+defaultConfigItem.Key+"\":");
                Console.WriteLine("         Adding Missing Config Option");
                config.Add(defaultConfigItem.Key, defaultConfigItem.Value);
                Console.WriteLine("         Updating Config File With Changes");
                File.WriteAllText(@".\omnipede-config.json", JsonSerializer.Serialize<Dictionary<string, string>>(config));    
            }
        }

        //config parsing
        depthConfigTry:
        if (int.TryParse(config["depth"], out depth) && depth > 0)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("     depth: "+depth);
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("     Invalid config for \"depth\" (must be an integer greater then 0)");
            Console.WriteLine("         Reseting config for \"depth\"");
            config["depth"] = defaultConfig["depth"];
            Console.WriteLine("         Updating Config File With Changes");
            File.WriteAllText(@".\omnipede-config.json", JsonSerializer.Serialize<Dictionary<string, string>>(config));  
            goto depthConfigTry;
        }

        lineLengthConfigTry:
        if (int.TryParse(config["lineLength"], out lineLength) && lineLength > 0)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("     lineLength: "+lineLength);
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("     Invalid config for \"lineLength\" (must be an integer greater then 0)");
            Console.WriteLine("         Reseting config for \"lineLength\"");
            config["lineLength"] = defaultConfig["lineLength"];
            Console.WriteLine("         Updating Config File With Changes");
            File.WriteAllText(@".\omnipede-config.json", JsonSerializer.Serialize<Dictionary<string, string>>(config));  
            goto lineLengthConfigTry;
        }

        movesSearchedFrequencyConfigTry:
        if (int.TryParse(config["movesSearchedFrequency"], out movesSearchedFrequency) && movesSearchedFrequency > -2)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("     movesSearchedFrequency: "+movesSearchedFrequency);
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("     Invalid config for \"movesSearchedFrequency\" (must be an integer greater then -2)");
            Console.WriteLine("         Reseting config for \"movesSearchedFrequency\"");
            config["movesSearchedFrequency"] = defaultConfig["movesSearchedFrequency"];
            Console.WriteLine("         Updating Config File With Changes");
            File.WriteAllText(@".\omnipede-config.json", JsonSerializer.Serialize<Dictionary<string, string>>(config));  
            goto movesSearchedFrequencyConfigTry;
        }

        //filePathConfigTry:
        //if (config["filePath"].GetType == "".GetType)
        //{
            filePath = config["filePath"];
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("     filePath: "+filePath);
        //}
        //else
        //{
        //    Console.ForegroundColor = ConsoleColor.Yellow;
        //    Console.WriteLine("     Invalid config for \"errorDetection\" (must be a valid string");
        //    Console.WriteLine("         Reseting config for \"errorDetection\"");
        //    config["errorDetection"] = defaultConfig["errorDetection"];
        //    Console.WriteLine("         Updating Config File With Changes");
        //    File.WriteAllText(@".\omnipede-config.json", JsonSerializer.Serialize<Dictionary<string, string>>(config));  
        //    goto filePathConfigTry;
        //}

        errorDetectionConfigTry:
        if (bool.TryParse(config["errorDetection"], out errorDetection))
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("     errorDetection: "+errorDetection);
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("     Invalid config for \"errorDetection\" (must be an boolean (\"true\" or \"false\"))");
            Console.WriteLine("         Reseting config for \"errorDetection\"");
            config["errorDetection"] = defaultConfig["errorDetection"];
            Console.WriteLine("         Updating Config File With Changes");
            File.WriteAllText(@".\omnipede-config.json", JsonSerializer.Serialize<Dictionary<string, string>>(config));  
            goto errorDetectionConfigTry;
        }

        return config;
    }
}