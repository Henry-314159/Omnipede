namespace omnipede;

using System.Text.Json;

public class GameStateNaviary
{
    public required Dictionary<string, string> startingPosition { get; set; } 
    public required List<string> patterns { get; set; } 
    public required int[] promotionRanks { get; set; }
    public required List<List<PlyNaviary>> moves { get; set; }
}

public class PlyNaviary
{
    public required string type { get; set; }
    public required List<object> startCoords { get; set; }
    public required List<object> endCoords { get; set; }
    public string? captured { get; set; }
}

public class Position{
    public List<Piece> pieces { get; set; } 
    public bool whitesTurn { get; set; }  = true;
    public Piece? enPassentable { get; set; } 

    public Position Clone()
    {
        return new Position(this);
    }

    public Position(Position positionToClone)
    {
        whitesTurn = positionToClone.whitesTurn;
        if (positionToClone.enPassentable != null)
        {
            enPassentable = new Piece(positionToClone.enPassentable);
        }
        else
        {
            enPassentable = null;
        }
        pieces = new();
        for (int i = 0; i < positionToClone.pieces.Count; i++)
        {
            pieces.Add(new Piece(positionToClone.pieces[i]));
        }
    }

    public Position(List<Piece> startingPosition, List<ExternalPly> plys)
    {
        List<PlyNaviary> plies = new();
        List<piece> startingPosition = new();

        pieces = startingPosition;

        for (int i = 0; i < plys.Count; i++)
        {
            enPassentable = null;
            whitesTurn = !whitesTurn;
            for (int j = 0; j < pieces.Count; j++)
            {
                if (plys[i].endCoords[0] == pieces[j].xCoord & plys[i].endCoords[1] + plys[i].enPassentOffset == pieces[j].yCoord)
                {
                    pieces.RemoveAt(j);
                    if (j == pieces.Count)
                    {
                        j-= 1;
                    }
                }
                if (plys[i].startCoords[0] == pieces[j].xCoord & plys[i].startCoords[1] == pieces[j].yCoord)
                {
                    pieces.RemoveAt(j);
                    if (plys[i].promotionPiece == null)
                    {
                        pieces.Insert(0, new Piece(plys[i].endCoords[0], plys[i].endCoords[1], plys[i].type, true));
                    }
                    else
                    {
                        pieces.Insert(0, new Piece(plys[i].endCoords[0], plys[i].endCoords[1], plys[i].promotionPiece, true));
                    }
                    if ((plys[i].type == "pawnsW" | plys[i].type == "pawnsB") & Math.Abs(plys[i].endCoords[1]-plys[i].startCoords[1]) == 2)
                    {
                        enPassentable = pieces[0];
                    }
                }
            }
        }
    }
}