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

        int depth = 0;
        int lineLength = 0;
        int movesSearchedFrequency = 0;
        string filePath = @".\omnipede-input.json";
        bool errorDetection = true;

        Dictionary<string, string> config = Config.LoadConfig(ref depth, ref filePath, ref errorDetection, ref lineLength, ref movesSearchedFrequency);

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
        Console.WriteLine("Starting Search For \"Best\" Moves:");
        

        for (int i = 0; i < lineLength; i++)
        {
            int movesSearched = 0;

            if (movesSearchedFrequency != -1)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("     Starting Search For \"Best\" Move:");
            }

            Tuple<Position, int> outputTuple = Engine.Normal(position, depth, -2147483648, 2147483647, errorDetection, ref movesSearched, ref movesSearchedFrequency);

            Position goodPosition = outputTuple.Item1;


            if (movesSearchedFrequency != -1)
            {
            Console.WriteLine("         Total Moves Searched: "+movesSearched);
            } 
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("     Move Found:");
            Console.WriteLine("         Position Value: "+outputTuple.Item2);

            PrintPositionDifrences(position, goodPosition);

            position = goodPosition.Clone();
        }

        watchGlobal.Stop();

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Total Run Time: "+watchGlobal.Elapsed);
        Console.Read();
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
                Console.WriteLine("         Add: "+JsonSerializer.Serialize(goodPosition.pieces[i]));
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
                Console.WriteLine("         Remove: "+JsonSerializer.Serialize(position.pieces[i]));
            }
        }
    }
}
