namespace omnipede;
using System.Text.Json;
using System.Text.RegularExpressions;

partial class PositionDecoder
{
    public static int Test(int x)
    {
        x += 10;
        return x;
    }
    public static Position DecodePosition(string filePath)
    {
        string json = File.ReadAllText(filePath);

        string[] positonAray;

        string[] splitJson = Regex.Split(json, @"{|}");
        try
        {
            positonAray = Regex.Split(splitJson[2], @"\n");
        }
        catch(IndexOutOfRangeException)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("IndexOutOfRangeException When Loading Position");
            Console.WriteLine("Paste Game Data Into omnipede-input.json Then Try Again");
            Console.WriteLine("Press Any Key to Close The Program");
            Console.ReadKey();

            Environment.Exit(0);
            positonAray = Regex.Split(splitJson[2], @"\n");
        }
        List<string> positonList = positonAray.ToList();
        positonList.RemoveAt(0);
        positonList.RemoveAt(positonList.Count -1 );

        List<Piece> startingPosition = new();

        for (int i = 0; i < positonList.Count; i++)
        {
            startingPosition.Add(new Piece(positonList[i]));
        }



        json = Regex.Replace(json, @"\n", "");
        List<string> encodedPlys = Regex.Split(json, "}[^{]*").ToList();
        encodedPlys.RemoveAt(encodedPlys.Count-1);
        encodedPlys.RemoveAt(0);
        List<ExternalPly> plys = new();

        for (int i = 0; i < encodedPlys.Count; i++)
        {
            encodedPlys[i] = encodedPlys[i].Replace(" ","");
            plys.Add(new ExternalPly(encodedPlys[i]));
            
        }

        return new(startingPosition, plys);

    }

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

public class Piece{
    public long xCoord { get; set; }
    public long yCoord { get; set; }
    public string type { get; set; }
    public bool hasMoved { get; set; } = false;

    public Piece Clone()
    {
        return new Piece(this);
    }
    public Piece(Piece pieceToClone)
    {
        xCoord = pieceToClone.xCoord;
        yCoord = pieceToClone.yCoord;
        type = pieceToClone.type;
        hasMoved = pieceToClone.hasMoved;
    }


    public Piece(long xCoordNew, long yCoordNew, string typeNew, bool hasMovedNew)
    {
        xCoord = xCoordNew;
        yCoord = yCoordNew;
        type = typeNew;
        hasMoved = hasMovedNew;
    }

    public Piece(string piece)
    {
        piece = Regex.Replace(piece, @" |""|\n|\u000D", "");
        string[] pieceAray = Regex.Split(piece, @",|:");

        xCoord = long.Parse(pieceAray[0]);
        yCoord = long.Parse(pieceAray[1]);
        type = pieceAray[2];
    }
}

public partial class ExternalPly{
    public string type = "";
    public List<long> startCoords = new();
    public List<long> endCoords = new();
    public long enPassentOffset = 0;
    public string? captured;
    public string? promotionPiece;

    public ExternalPly(string encodedPly)
    {
        List<string> encodedPlyList = Regex.Split(encodedPly, @" |{|""|\[|\]|,|\n|:").ToList();

        for (int i = 0; i < encodedPlyList.Count ; i++)
        {
            if (encodedPlyList[i] == ""){
                encodedPlyList.RemoveAt(i);
                i--;
            }
            else if (encodedPlyList[i].ToCharArray().Length == 0){
                encodedPlyList.RemoveAt(i);
                i--;
            }
            else if (Char.GetUnicodeCategory(encodedPlyList[i].ToCharArray()[0]).ToString() == "Control" & !int.TryParse(encodedPlyList[i], out _)){
                encodedPlyList.RemoveAt(i);
                i--;
            }
        }

        for (int i = 0; i < encodedPlyList.Count; i++)
        {
            if (encodedPlyList[i] == "type"){
                i++;
                type = encodedPlyList[i];
                i++; i++;
                startCoords.Add(long.Parse(encodedPlyList[i]));
                i++;
                startCoords.Add(long.Parse(encodedPlyList[i]));
                i++; i++;
                endCoords.Add(long.Parse(encodedPlyList[i]));
                i++;
                endCoords.Add(long.Parse(encodedPlyList[i]));                
            }
            else if (long.TryParse(encodedPlyList[i], out _)){
                enPassentOffset = long.Parse(encodedPlyList[i]);
            }
            else if (encodedPlyList[i] == "captured"){
                i++;
                captured = encodedPlyList[i];
            }
            else{
                promotionPiece = encodedPlyList[i];
            }
        }

    }
}

