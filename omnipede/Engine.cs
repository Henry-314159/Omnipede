using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace omnipede;

public class Engine
{
    public static Tuple<Position, int> Normal(Position startingPosition, int depth, int alpha, int beta)
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
        //else
        //{
        //    Console.WriteLine("Depth: "+depth);
        //    Console.WriteLine("Engine Run Time: "+watch4.Elapsed);
        //    Console.WriteLine();
        //}

        testPositionList = MoveLister.ListMoves(startingPosition);
        bestPosition = testPositionList[0];
  
        
        if (startingPosition.whitesTurn)
        {
            bestPositionValue = -2147483648;
            for (int i = 0; i < testPositionList.Count; i++)
            {
                testPosition = testPositionList[i];

                if (PositionEvaluator.KingsExist(testPosition))
                {
                    testPositionValue = Engine.Normal(testPosition, depth-1, alpha, beta).Item2;
                }
                else
                {
                    testPositionValue = PositionEvaluator.PointBased(testPosition);
                }
                
                alpha = Math.Max(alpha, testPositionValue);

                if (testPositionValue > bestPositionValue)
                {
                    bestPosition = testPosition;
                    bestPositionValue = testPositionValue;
                }

                if (beta <= alpha)
                {
                    return new Tuple<Position, int>(bestPosition, bestPositionValue);
                }
            }
        }
        else
        {
            bestPositionValue = 2147483647;
            for (int i = 0; i < testPositionList.Count; i++)
            {
                testPosition = testPositionList[i];

                if (PositionEvaluator.KingsExist(testPosition))
                {
                    testPositionValue = Engine.Normal(testPosition, depth-1, alpha, beta).Item2;
                }
                else
                {
                    testPositionValue = PositionEvaluator.PointBased(testPosition);
                }

                beta = Math.Min(beta, testPositionValue);

                if (testPositionValue < bestPositionValue)
                {
                    bestPosition = testPosition;
                    bestPositionValue = testPositionValue;
                }

                if (beta <= alpha)
                {
                    return new Tuple<Position, int>(bestPosition, bestPositionValue);
                }
            }
        }

        return new Tuple<Position, int>(bestPosition, bestPositionValue);
    }
}