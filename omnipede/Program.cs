namespace omnipede;

using System.Collections;
using System.Dynamic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

class Program
{
    static void Main(string[] args)
    {
        var watchGlobal = System.Diagnostics.Stopwatch.StartNew();
        Dictionary<string, string> config;
        try
        {
            config = JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(@".\omnipede-config.json"));
        }
        catch(JsonException)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("JsonException When Loading Config, Remaking Config File");

            config = new(){};
            File.WriteAllText(@".\omnipede-config.json", JsonSerializer.Serialize<Dictionary<string, string>>(config));          
        }
        catch(FileNotFoundException)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("FileNotFoundException When Loading Config, Creating Config File");

            config = new(){};
            using (var configFile = File.Create(@".\omnipede-config.json")){}
            File.WriteAllText(@".\omnipede-config.json", JsonSerializer.Serialize<Dictionary<string, string>>(config)); 
        }
        int depth;
        string filePath;

        ConfigTry:
        try
        {
            depth = int.Parse(config["depth"]);
            filePath = (string)config["filePath"];
        }
        catch (KeyNotFoundException)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("KeyNotFoundException When Loading Config, Adding Missing Keys");

            if (!config.ContainsKey("depth")) {config.Add("depth", "2"); }
            if (!config.ContainsKey("filePath")) {config.Add("filePath", @".\omnipede-input.json");}

            File.WriteAllText(@".\omnipede-config.json", JsonSerializer.Serialize<Dictionary<string, string>>(config));

            goto ConfigTry;
        }
        catch (FormatException)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("FormatException When Loading Config, Reseting Depth Config");

            config["depth"] = "2";
            File.WriteAllText(@".\omnipede-config.json", JsonSerializer.Serialize<Dictionary<string, string>>(config));

            goto ConfigTry;
        }
        
        

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Config Loaded:");
        Console.WriteLine("     Depth: "+depth);
        Console.WriteLine("     File Path: "+filePath);

        Position position;


        try
        {
            position = PositionDecoder.DecodePosition(filePath);
        }
        catch(FileNotFoundException)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("FileNotFoundException When Loading Position, Reseting File Path Config and Creating Input File");
            config["filePath"] = @".\omnipede-input.json";
            using (var inputFile = File.Create(config["filePath"])){}
            File.WriteAllText(@".\omnipede-config.json", JsonSerializer.Serialize<Dictionary<string, string>>(config));
            Console.WriteLine("Paste Game Data Into omnipede-input.json Then Try Again");
            Console.WriteLine("Press Any Key to Close The Program");
            Console.ReadKey();

            Environment.Exit(0);
            position = PositionDecoder.DecodePosition(filePath);
        }

        
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Position Decoded");
        Console.WriteLine("Starting Search For \"Best\" Move");

        Tuple<Position, int> outputTuple = Engine.Normal(position, depth, -2147483648, 2147483647);


        Position goodPosition = outputTuple.Item1;

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Move Found:");
        Console.WriteLine("     Position Value: "+outputTuple.Item2);
        

        PrintPositionDifrences(position, goodPosition);

        watchGlobal.Stop();

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Total Run Time: "+watchGlobal.Elapsed);
        Console.ReadKey();
    }

    static void PrintPositionDifrences(Position position, Position goodPosition)
    {
        bool difrent;
        for (int i = 0; i < goodPosition.pieces.Count; i++)
        {
            difrent = true;
            for (int j = 0; j < position.pieces.Count; j++)
            {
                if (JsonSerializer.Serialize(goodPosition.pieces[i]) == JsonSerializer.Serialize(position.pieces[j]))
                {
                    difrent = false;
                }
            }
            if (difrent)
            {
                Console.WriteLine("     Add: "+JsonSerializer.Serialize(goodPosition.pieces[i]));
            }
        }


        for (int i = 0; i < position.pieces.Count; i++)
        {
            difrent = true;
            for (int j = 0; j < goodPosition.pieces.Count; j++)
            {
                if (JsonSerializer.Serialize(position.pieces[i]) == JsonSerializer.Serialize(goodPosition.pieces[j]))
                {
                    difrent = false;
                }
            }
            if (difrent)
            {
                Console.WriteLine("     Remove: "+JsonSerializer.Serialize(position.pieces[i]));
            }
        }
    }
}
