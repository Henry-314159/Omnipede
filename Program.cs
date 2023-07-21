namespace omnipede;

using System.Collections;
using System.Dynamic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

    

class Program
{
    public static Dictionary<string, int> transpositionTab = new();
    

    static void Main(string[] args)
    {
            var watchGlobal = System.Diagnostics.Stopwatch.StartNew();

            var watchConfig = System.Diagnostics.Stopwatch.StartNew();

        int maxDepth = 2;
        int maxTimeMiliseconds = 10;

        Config.LoadConfig(ref maxDepth, ref maxTimeMiliseconds);

            watchConfig.Stop();

            var watchDecode = System.Diagnostics.Stopwatch.StartNew();

        if (!File.Exists(@".\omnipede-input.json"))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Input file not found:");
            Console.WriteLine("    Creating input file");
            File.Create(@".\omnipede-input.json");
            Console.WriteLine("    Paste game into input file and try again");
            Console.ReadKey();
        }
        GameStateNaviary gameStateNaviary = JsonSerializer.Deserialize<GameStateNaviary>(File.ReadAllText(@".\omnipede-input.json"))!;
        GameState gameState = gameStateNaviary.ToGameState();

            watchDecode.Stop();

            var watchEngine = System.Diagnostics.Stopwatch.StartNew();

        //int currentDepth = MaxDepth;

        //Console.WriteLine(gameState.DebugString());

        
        Tuple<Ply?, int> output = new Tuple<Ply?, int>(new(), 0);

        for (int currentDepth = 1; currentDepth <= maxDepth; currentDepth++)
        {
            int depth = currentDepth;
            output = Engine.Normal(ref maxTimeMiliseconds, ref watchGlobal, ref gameState, currentDepth, -2147483648, 2147483647, false);

            if (output.Item1 != null)
            {
                Console.WriteLine(output.Item1.DebugString());
                Console.WriteLine("Ply Value: "+output.Item2);
                Console.WriteLine("Depth: "+currentDepth);
            }
            else
            {
                Console.WriteLine($"Depth {currentDepth} search stoped before completion");
            }
        }

            watchEngine.Stop();

            watchGlobal.Stop();
        
        Console.WriteLine(watchConfig.Elapsed+" - Config Loading Time");
        Console.WriteLine(watchDecode.Elapsed+" - Game State Decoding Time");
        Console.WriteLine(watchEngine.Elapsed+" - Search Time");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(watchGlobal.Elapsed+" - Total Run Time");
        Console.ReadKey();

        
    }

}
