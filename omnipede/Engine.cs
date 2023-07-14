using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace omnipede;

public class Engine
{
    public static Tuple<Position, int> Normal(Position startingPosition, int depth, int alpha, int beta, bool errorDetection, ref int movesSearched, ref int movesSearchedFrequency)
    {
        List<Position> testPositionList;
        Position bestPosition;
        int bestPositionValue;
        Position testPosition;
        int testPositionValue;

        

        if (depth == 0)
        {
            return new Tuple<Position, int>(startingPosition, PositionEvaluator.PointBased(startingPosition));
        }

        testPositionList = MoveLister.ListMoves(startingPosition, errorDetection);
        bestPosition = testPositionList[0];
  
        
        if (startingPosition.whitesTurn)
        {
            bestPositionValue = -2147483648;
            for (int i = 0; i < testPositionList.Count; i++)
            {   
                if (movesSearchedFrequency > -1)
                {
                    if (movesSearchedFrequency != 0 && movesSearched % movesSearchedFrequency == 0)
                    {
                        Console.WriteLine("         Moves Searched: "+movesSearched);
                    }
                    movesSearched++;
                }
                

                testPosition = testPositionList[i];

                if (PositionEvaluator.KingsExist(testPosition))
                {
                  testPositionValue = Engine.Normal(testPosition, depth-1, alpha, beta, errorDetection, ref movesSearched, ref movesSearchedFrequency).Item2;
                }
                else
                {
                    testPositionValue = PositionEvaluator.PointBased(testPosition);
                }
                
                alpha = Math.Max(alpha, testPositionValue);                

                if (testPositionValue >= beta)
                {
                    return new Tuple<Position, int>(testPosition, testPositionValue);
                }

                if (testPositionValue > bestPositionValue)
                {
                    bestPosition = testPosition;
                    bestPositionValue = testPositionValue;
                }
            }
        }
        else
        {
            bestPositionValue = 2147483647;
            for (int i = 0; i < testPositionList.Count; i++)
            { 
                if (movesSearchedFrequency > -1)
                {
                    if (movesSearchedFrequency != 0 && movesSearched % movesSearchedFrequency == 0)
                    {
                        Console.WriteLine("         Moves Searched: "+movesSearched);
                    }
                    movesSearched++;
                }

                testPosition = testPositionList[i];

                if (PositionEvaluator.KingsExist(testPosition))
                {
                  testPositionValue = Engine.Normal(testPosition, depth-1, alpha, beta, errorDetection, ref movesSearched, ref movesSearchedFrequency).Item2;
                }
                else
                {
                    testPositionValue = PositionEvaluator.PointBased(testPosition);
                }

                beta = Math.Min(beta, testPositionValue);     

                if (testPositionValue <= alpha)
                {
                    return new Tuple<Position, int>(testPosition, testPositionValue);
                }

                if (testPositionValue < bestPositionValue)
                {
                    bestPosition = testPosition;
                    bestPositionValue = testPositionValue;
                }
            }
        }

        return new Tuple<Position, int>(bestPosition, bestPositionValue);
    }
}