using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace omnipede;

public class Engine
{
    public static Tuple<Ply?, int> Normal(ref int maxTime, ref System.Diagnostics.Stopwatch totalTime, ref GameState gameState, int depth, int alpha, int beta, bool debug)
    {
        List<Ply> testPlyList;
        List<Ply> principleVariation = new();
        Ply bestPly;
        int bestPlyValue;
        Ply testPly;
        int testPlyValue;

        if (depth == 0)
        {
            return new(null, GameState.Evaluate(gameState));
        }


        testPlyList = ListPlies.PseudoLegal(gameState);

        bestPly = testPlyList[0];

        //Console.WriteLine(gameState.currentTurn);
        
        if (gameState.currentTurn == Piece.Color.White)
        {
            bestPlyValue = -2147483648;
            for (int i = 0; i < testPlyList.Count; i++)
            {
                if (totalTime.ElapsedMilliseconds > maxTime)
                {
                    return new(null, GameState.Evaluate(gameState));
                }

                testPly = testPlyList[i];

                Tuple<Ply?, int> testTuple = new(new(), 0);

                gameState = Ply.DoPly(gameState, testPly);

                if (testPly.piecesToRemove.Count > 0 && (testPly.piecesToRemove[0].Item1.binary & Piece.Type._MASK) == Piece.Type.King)
                {
                    testPlyValue = 2147483647;
                }
                else
                {
                    testTuple = Engine.Normal(ref maxTime, ref totalTime, ref gameState, depth-1, alpha, beta, debug);
                    testPlyValue = testTuple.Item2;
                }

                alpha = Math.Max(alpha, testPlyValue);  
                if (beta <= alpha)
                {
                    gameState = Ply.UndoPly(gameState, testPly);
                    return new(null, testPlyValue);
                }

                if (testPlyValue > bestPlyValue)
                {
                    bestPly = testPly;
                    bestPlyValue = testPlyValue;
                }

                gameState = Ply.UndoPly(gameState, testPly);
            }
        }
        else
        {
            bestPlyValue = 2147483647;
            for (int i = 0; i < testPlyList.Count; i++)
            {
                if (totalTime.ElapsedMilliseconds > maxTime)
                {
                    return new(null, GameState.Evaluate(gameState));
                }

                testPly = testPlyList[i];

                Tuple<Ply?, int> testTuple = new(new(), 0);

                gameState = Ply.DoPly(gameState, testPly);

                if (testPly.piecesToRemove.Count > 0 && (testPly.piecesToRemove[0].Item1.binary & Piece.Type._MASK) == Piece.Type.King)
                {
                    testPlyValue = -2147483648;
                }
                else
                {
                    testTuple = Engine.Normal(ref maxTime, ref totalTime, ref gameState, depth-1, alpha, beta, debug);
                    testPlyValue = testTuple.Item2;
                }

                beta = Math.Min(beta, testPlyValue); 
                if (beta <= alpha)
                {
                    gameState = Ply.UndoPly(gameState, testPly);
                    return new(null, testPlyValue);
                }

                if (testPlyValue < bestPlyValue)
                {
                    bestPly = testPly;
                    bestPlyValue = testPlyValue;
                }

                gameState = Ply.UndoPly(gameState, testPly);
                
            }
        }

        return new(bestPly, bestPlyValue);
    }
}