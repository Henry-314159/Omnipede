namespace omnipede;
using System.Numerics;
using System.Text.Json;

public class PlyNaviary
{
    public required string type { get; set; }
    public required List<JsonElement> startCoords { get; set; }
    public required List<JsonElement> endCoords { get; set; }
    public string? captured { get; set; }
}

public class GameRulesNaviary
{
    public required JsonElement slideLimit { get; set; }
    public required string winCondition { get; set; }
}

public class GameStateNaviary
{
    public required Dictionary<string, string> startingPosition { get; set; } 
    public required List<string> patterns { get; set; } 
    public int[]? promotionRanks { get; set; }
    public required List<List<PlyNaviary>> moves { get; set; }
    public GameRulesNaviary? gameRules { get; set; }

    public GameState ToGameState()
    {
        GameState gameState = new GameState{pieces =new(), currentTurn=Piece.Color.White};

        
        if (gameRules != null && uint.TryParse(gameRules.slideLimit.ToString(), out uint slideLimit))
        {
            gameState.slideLimit = slideLimit;
        }
        else
        {
            gameState.slideLimit = 50;
        }

        if (promotionRanks == null)
        {
            gameState.promotionRankWhite=null;
            gameState.promotionRankBlack=null;
        }
        else
        {
            gameState.promotionRankWhite=(BigInteger)promotionRanks[1]; 
            gameState.promotionRankBlack=(BigInteger)promotionRanks[0];
        }

        List<uint> promotionAllowedPieces = new();

        foreach (var item in startingPosition)
        {
            BigInteger x = BigInteger.Parse(item.Key.Split(",")[0]);
            BigInteger y = BigInteger.Parse(item.Key.Split(",")[1]);
            uint binary = Piece.DecodeName(item.Value);
            gameState.pieces.Add(new Piece{x=x, y=y, binary=binary, hasMoved=false});
            if (!promotionAllowedPieces.Contains(binary & Piece.Type._MASK))
            {
                promotionAllowedPieces.Add(binary & Piece.Type._MASK);
            }
        }

        promotionAllowedPieces.Remove(Piece.Type.King);
        promotionAllowedPieces.Remove(Piece.Type.Pawn);

        if (promotionAllowedPieces.Count > 0)
        {
            gameState.promotionAllowedPieces = promotionAllowedPieces.ToArray();
        }

        for (int i = 0; i < moves.Count; i++)
        {
            for (int j = 0; j < moves[i].Count; j++)
            {
                if (moves[i].Count == 1)
                {
                    gameState.currentTurn = Piece.Color.Black;
                }
                if (gameState.tempPieceIndex != null)
                {
                    gameState.pieces.RemoveAt((int)gameState.tempPieceIndex);
                    gameState.tempPieceIndex = null;
                }
                BigInteger startX = BigInteger.Parse(moves[i][j].startCoords[0].ToString());
                BigInteger startY = BigInteger.Parse(moves[i][j].startCoords[1].ToString());
                BigInteger endX = BigInteger.Parse(moves[i][j].endCoords[0].ToString());
                BigInteger endY = BigInteger.Parse(moves[i][j].endCoords[1].ToString());
                bool hasCaptured = moves[i][j].captured != null;
                BigInteger enPassantOffset;
                uint promotionBinary = 0;
                enPassantOffset = 0;
                switch (moves[i][j].endCoords.Count)
                {
                    case 3:
                        enPassantOffset = BigInteger.Parse(moves[i][j].endCoords[2].ToString());
                        break;
                    case 4:
                        if (!(moves[i][j].endCoords[2].ToString() == ""))
                        {
                            enPassantOffset = BigInteger.Parse(moves[i][j].endCoords[2].ToString());
                        }
                        promotionBinary = Piece.DecodeName(moves[i][j].endCoords[3].ToString());
                        break;
                }
                for (int k = 0; k < gameState.pieces.Count; k++)
                {
                    if (hasCaptured && gameState.pieces[k].x == endX && gameState.pieces[k].y == endY + enPassantOffset)
                    {
                        gameState.pieces.RemoveAt(k);
                        k-=2;
                        continue;
                    }
                    if (gameState.pieces[k].x == startX && gameState.pieces[k].y == startY)
                    {
                        gameState.pieces[k].x = endX;
                        gameState.pieces[k].y = endY;
                        gameState.pieces[k].hasMoved = true;
                        if ((gameState.pieces[k].binary & Piece.Type._MASK) == Piece.Type.Pawn && BigInteger.Abs(startY-endY) == 2)
                        {
                            if ((gameState.pieces[k].binary & Piece.Color._MASK) == Piece.Color.White)
                            {
                                gameState.pieces.Add(new Piece{x=gameState.pieces[k].x, y=gameState.pieces[k].y - 1, binary=Piece.Category.EnPassantCaptureSpace & Piece.Color.White, hasMoved=true});
                                gameState.tempPieceIndex = gameState.pieces.Count-1;
                            }
                            else
                            {
                                gameState.pieces.Add(new Piece{x=gameState.pieces[k].x, y=gameState.pieces[k].y + 1, binary=Piece.Category.EnPassantCaptureSpace & Piece.Color.Black, hasMoved=true});
                                gameState.tempPieceIndex = gameState.pieces.Count-1;
                            }
                        }
                        if (promotionBinary != 0)
                        {
                            gameState.pieces[k].binary = promotionBinary;
                        }
                    }
                }
            }
        }

        return gameState;
    }
}