namespace omnipede;
using System.Numerics;

class ListPlies
{
    public static List<Ply> PseudoLegal(GameState gameState)
    {
        List<Ply> plies = new();
        for (int i = 0; i < gameState.pieces.Count; i++)
        {
            if ((gameState.pieces[i].binary & Piece.Color._MASK) == gameState.currentTurn)
            {
                switch (gameState.pieces[i].binary & Piece.Type._MASK)
                {
                    case Piece.Type.Pawn:
                        if ((gameState.pieces[i].binary & Piece.Color._MASK) == Piece.Color.White)
                        {
                            MovesPawn(ref plies, gameState, gameState.pieces[i], i, 1);
                        }
                        else
                        {
                            MovesPawn(ref plies, gameState, gameState.pieces[i], i, -1);
                        }
                        break;
                    case Piece.Type.Knight:
                        MovesKnight(ref plies, gameState, gameState.pieces[i], i);
                        break;
                    case Piece.Type.Bishop:
                        MovesBishop(ref plies, gameState, gameState.pieces[i], i);
                        break;
                    case Piece.Type.Rook:
                        MovesRook(ref plies, gameState, gameState.pieces[i], i);
                        break;
                    case Piece.Type.Queen:
                        //Console.WriteLine(gameState.currentTurn);
                        MovesBishop(ref plies, gameState, gameState.pieces[i], i);
                        MovesRook(ref plies, gameState, gameState.pieces[i], i);
                        break;
                    case Piece.Type.King:
                        MovesGuard(ref plies, gameState, gameState.pieces[i], i);
                        break;
                    case Piece.Type.Guard:
                        MovesGuard(ref plies, gameState, gameState.pieces[i], i);
                        break;
                    case Piece.Type.Hawk:
                        MovesHawk(ref plies, gameState, gameState.pieces[i], i);
                        break;
                    case Piece.Type.Archbishop:
                        MovesKnight(ref plies, gameState, gameState.pieces[i], i);
                        MovesBishop(ref plies, gameState, gameState.pieces[i], i);
                        break;
                    case Piece.Type.Chancellor:
                        MovesKnight(ref plies, gameState, gameState.pieces[i], i);
                        MovesRook(ref plies, gameState, gameState.pieces[i], i);
                        break;
                    case Piece.Type.Amazon:
                        MovesKnight(ref plies, gameState, gameState.pieces[i], i);
                        MovesBishop(ref plies, gameState, gameState.pieces[i], i);
                        MovesRook(ref plies, gameState, gameState.pieces[i], i);
                        break;
                }
            }
        }
        return plies;
    }





    static void MovesBishop(ref List<Ply> plies, GameState gameState, Piece pieceToMove, int i)
    {
        SlideLimitAddPlysIfPseudoLegal(ref plies, gameState, pieceToMove, i, 1, 1);
        SlideLimitAddPlysIfPseudoLegal(ref plies, gameState, pieceToMove, i,-1, 1);
        SlideLimitAddPlysIfPseudoLegal(ref plies, gameState, pieceToMove, i,-1,-1);
        SlideLimitAddPlysIfPseudoLegal(ref plies, gameState, pieceToMove, i, 1,-1);
    }
    static void MovesRook(ref List<Ply> plies, GameState gameState, Piece pieceToMove, int i)
    {
        SlideLimitAddPlysIfPseudoLegal(ref plies, gameState, pieceToMove, i, 1, 0);
        SlideLimitAddPlysIfPseudoLegal(ref plies, gameState, pieceToMove, i, 0, 1);
        SlideLimitAddPlysIfPseudoLegal(ref plies, gameState, pieceToMove, i,-1, 0);
        SlideLimitAddPlysIfPseudoLegal(ref plies, gameState, pieceToMove, i, 0,-1);
    }
    static void MovesHawk(ref List<Ply> plies, GameState gameState, Piece pieceToMove, int i)
    {
        AddPlyIfPseudoLegal(ref plies, gameState, pieceToMove, i, 2, 0);
        AddPlyIfPseudoLegal(ref plies, gameState, pieceToMove, i, 2, 2);
        AddPlyIfPseudoLegal(ref plies, gameState, pieceToMove, i, 0, 2);
        AddPlyIfPseudoLegal(ref plies, gameState, pieceToMove, i,-2, 2);
        AddPlyIfPseudoLegal(ref plies, gameState, pieceToMove, i,-2, 0);
        AddPlyIfPseudoLegal(ref plies, gameState, pieceToMove, i,-2,-2);
        AddPlyIfPseudoLegal(ref plies, gameState, pieceToMove, i, 0,-2);
        AddPlyIfPseudoLegal(ref plies, gameState, pieceToMove, i, 2,-2);
        
        AddPlyIfPseudoLegal(ref plies, gameState, pieceToMove, i, 3, 0);
        AddPlyIfPseudoLegal(ref plies, gameState, pieceToMove, i, 3, 3);
        AddPlyIfPseudoLegal(ref plies, gameState, pieceToMove, i, 0, 3);
        AddPlyIfPseudoLegal(ref plies, gameState, pieceToMove, i,-3, 3);
        AddPlyIfPseudoLegal(ref plies, gameState, pieceToMove, i,-3, 0);
        AddPlyIfPseudoLegal(ref plies, gameState, pieceToMove, i,-3,-3);
        AddPlyIfPseudoLegal(ref plies, gameState, pieceToMove, i, 0,-3);
        AddPlyIfPseudoLegal(ref plies, gameState, pieceToMove, i, 3,-3);
    }
    static void MovesGuard(ref List<Ply> plies, GameState gameState, Piece pieceToMove, int i)
    {
        AddPlyIfPseudoLegal(ref plies, gameState, pieceToMove, i, 1, 0);
        AddPlyIfPseudoLegal(ref plies, gameState, pieceToMove, i, 1, 1);
        AddPlyIfPseudoLegal(ref plies, gameState, pieceToMove, i, 0, 1);
        AddPlyIfPseudoLegal(ref plies, gameState, pieceToMove, i,-1, 1);
        AddPlyIfPseudoLegal(ref plies, gameState, pieceToMove, i,-1, 0);
        AddPlyIfPseudoLegal(ref plies, gameState, pieceToMove, i,-1,-1);
        AddPlyIfPseudoLegal(ref plies, gameState, pieceToMove, i, 0,-1);
        AddPlyIfPseudoLegal(ref plies, gameState, pieceToMove, i, 1,-1);
    }
    static void MovesKnight(ref List<Ply> plies, GameState gameState, Piece pieceToMove, int i)
    {
        AddPlyIfPseudoLegal(ref plies, gameState, pieceToMove, i, 1, 2);
        AddPlyIfPseudoLegal(ref plies, gameState, pieceToMove, i, 2, 1);
        AddPlyIfPseudoLegal(ref plies, gameState, pieceToMove, i, 1,-2);
        AddPlyIfPseudoLegal(ref plies, gameState, pieceToMove, i, 2,-1);
        AddPlyIfPseudoLegal(ref plies, gameState, pieceToMove, i,-1, 2);
        AddPlyIfPseudoLegal(ref plies, gameState, pieceToMove, i,-2, 1);
        AddPlyIfPseudoLegal(ref plies, gameState, pieceToMove, i,-1,-2);
        AddPlyIfPseudoLegal(ref plies, gameState, pieceToMove, i,-2,-1);
    }
    static void MovesPawn(ref List<Ply> plies, GameState gameState, Piece pieceToMove, int pieceToMoveIndex, BigInteger direction)
    {
        List<Ply> pliesLocal = new();
        for (int i = 0; i < gameState.pieces.Count; i++)
        {
            if (pieceToMove.x + 1 == gameState.pieces[i].x && pieceToMove.y + direction == gameState.pieces[i].y)
            {
                //Capture Right
                if ((gameState.pieces[i].binary & Piece.Color._MASK) != (pieceToMove.binary & Piece.Color._MASK) && (gameState.pieces[i].binary & Piece.Category._MASK) == (pieceToMove.binary & Piece.Category._MASK))
                {
                    pliesLocal.Insert(0, new(gameState, pieceToMoveIndex, 1, direction, new List<Tuple<Piece, int>>{new Tuple<Piece, int>(gameState.pieces[i], i)}, new()));
                    break;
                }
                //En Passant Right
                else if ((gameState.pieces[i].binary & Piece.Color._MASK) != (pieceToMove.binary & Piece.Color._MASK))//Add Extra condition here if you add another piece catagory
                {
                    for (int j = 0; j < gameState.pieces.Count; j++)
                    {
                        if (pieceToMove.x + 1 == gameState.pieces[j].x && pieceToMove.y + direction == gameState.pieces[j].y)
                        {
                            pliesLocal.Insert(0, new Ply(gameState, pieceToMoveIndex, 1, direction, new List<Tuple<Piece, int>>{new Tuple<Piece, int>(gameState.pieces[j], j)}, new()));
                            break;
                        }
                    }
                    break;
                }
                else
                {
                    break;
                }
            }
        }

        for (int i = 0; i < gameState.pieces.Count; i++)
        {
            if (pieceToMove.x - 1 == gameState.pieces[i].x && pieceToMove.y + direction == gameState.pieces[i].y)
            {
                //Capture Left
                if ((gameState.pieces[i].binary & Piece.Color._MASK) != (pieceToMove.binary & Piece.Color._MASK) && (gameState.pieces[i].binary & Piece.Category._MASK) == (pieceToMove.binary & Piece.Category._MASK))
                {
                    pliesLocal.Insert(0, new(gameState, pieceToMoveIndex, -1, direction, new List<Tuple<Piece, int>>{new Tuple<Piece, int>(gameState.pieces[i], i)}, new()));
                    break;
                }
                //En Passant Left
                else if ((gameState.pieces[i].binary & Piece.Color._MASK) != (pieceToMove.binary & Piece.Color._MASK))//Add Extra condition here if you add another piece catagory
                {
                    for (int j = 0; j < gameState.pieces.Count; j++)
                    {
                        if (pieceToMove.x - 1 == gameState.pieces[j].x && pieceToMove.y + direction == gameState.pieces[j].y)
                        {
                            pliesLocal.Insert(0, new Ply(gameState, pieceToMoveIndex, -1, direction, new List<Tuple<Piece, int>>{new Tuple<Piece, int>(gameState.pieces[j], j)}, new()));
                            break;
                        }
                    }
                    break;
                }
                else
                {
                    break;
                }
            }
        }
        
        //forward
        for (int i = 0; i < gameState.pieces.Count; i++)
        {
            if (pieceToMove.x == gameState.pieces[i].x && pieceToMove.y + direction == gameState.pieces[i].y)
            {
                goto PromotionTest;
            }
        }
        pliesLocal.Add(new(gameState, pieceToMoveIndex, 0, direction, new(), new()));
        
        //double
        if (pieceToMove.hasMoved == false)
        {
            for (int i = 0; i < gameState.pieces.Count; i++)
            {
                if (pieceToMove.x == gameState.pieces[i].x && pieceToMove.y + 2*direction == gameState.pieces[i].y)
                {
                    goto PromotionTest;
                }
            }
            pliesLocal.Add(
                new Ply(gameState, pieceToMoveIndex, 0, direction*2, new(), new List<Tuple<Piece, int>>{new(
                    new Piece{
                        x=pieceToMove.x, 
                        y=pieceToMove.y + direction, 
                        binary=Piece.Category.EnPassantCaptureSpace|(pieceToMove.binary&Piece.Color._MASK), 
                        hasMoved=true} 
                    ,gameState.pieces.Count-1)}
                    ));
        }

        PromotionTest:

        if (gameState.promotionRankWhite == null)
        {
            plies.AddRange(pliesLocal);
            return;
        }

        foreach (Ply ply in pliesLocal)
        {
            if (ply.endY == gameState.promotionRankWhite && (gameState.pieces[pieceToMoveIndex].binary & Piece.Color._MASK) == Piece.Color.White)
            {
                foreach (uint binary in gameState.promotionAllowedPieces)
                {
                    ply.endBinary = binary | Piece.Color.White;
                    plies.Insert(0, ply);
                    //Console.WriteLine(ply.DebugString());
                }
            }
            else if (ply.endY == gameState.promotionRankWhite && (gameState.pieces[pieceToMoveIndex].binary & Piece.Color._MASK) == Piece.Color.Black)
            {
                foreach (uint binary in gameState.promotionAllowedPieces)
                {
                    ply.endBinary = binary | Piece.Color.Black;
                    plies.Insert(0, ply);
                    //Console.WriteLine(ply.DebugString());
                }
            }
            else
            {
                plies.Add(ply);
            }
        }
    }





    static void SlideLimitAddPlysIfPseudoLegal(ref List<Ply> plies, GameState gameState, Piece pieceToMove, int pieceToMoveIndex, BigInteger xOffset, BigInteger yOffset)
    { 
        for (int h = 1; h <= gameState.slideLimit; h++)
        {
            for (int i = 0; i < gameState.pieces.Count; i++)
            {
                if (pieceToMove.x + xOffset*h == gameState.pieces[i].x && pieceToMove.y + yOffset*h == gameState.pieces[i].y)
                {
                    if ((gameState.pieces[i].binary & Piece.Color._MASK) != (pieceToMove.binary & Piece.Color._MASK) && (gameState.pieces[i].binary & Piece.Category._MASK) == (pieceToMove.binary & Piece.Category._MASK))
                    {
                        plies.Insert(0, new(gameState, pieceToMoveIndex, xOffset*h, yOffset*h, new List<Tuple<Piece, int>>{new Tuple<Piece, int>(gameState.pieces[i], i)}, new()));
                        return;
                    }
                    else if ((gameState.pieces[i].binary & Piece.Category._MASK) == (pieceToMove.binary & Piece.Category._MASK))
                    {
                        return;
                    }
                }
            }
            plies.Add(new(gameState, pieceToMoveIndex, xOffset*h, yOffset*h, new(), new()));
        }
    }
    static void AddPlyIfPseudoLegal(ref List<Ply> plies, GameState gameState, Piece pieceToMove, int pieceToMoveIndex, BigInteger xOffset, BigInteger yOffset)
    { 
        for (int i = 0; i < gameState.pieces.Count; i++)
        {
            if (pieceToMove.x + xOffset == gameState.pieces[i].x && pieceToMove.y + yOffset == gameState.pieces[i].y)
            {
                if ((gameState.pieces[i].binary & Piece.Color._MASK) != (pieceToMove.binary & Piece.Color._MASK) && (gameState.pieces[i].binary & Piece.Category._MASK) == (pieceToMove.binary & Piece.Category._MASK))
                {
                    plies.Insert(0, new(gameState, pieceToMoveIndex, xOffset, yOffset, new List<Tuple<Piece, int>>{new Tuple<Piece, int>(gameState.pieces[i], i)}, new()));
                    return;
                }
                else
                {
                    return;
                }
            }
        }
        plies.Add(new(gameState, pieceToMoveIndex, xOffset, yOffset, new(), new()));
    }

}