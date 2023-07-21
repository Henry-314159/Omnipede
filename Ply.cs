namespace omnipede;
using System.Numerics;
using System.Text.Json;

public class Ply
{
    public BigInteger startX { get; set; }
    public BigInteger startY { get; set; }
    public BigInteger endX { get; set; }
    public BigInteger endY { get; set; }
    public int pieceToMoveIndex { get; set; }
    public uint startBinary { get; set; }
    public uint endBinary { get; set; }
    public int? tempPieceIndex { get; set; } = null;
    public bool hasMoved { get; set; }
    public List<Tuple<Piece, int>> piecesToRemove { get; set; } = new();
    public List<Tuple<Piece, int>> piecesToAdd { get; set; } = new();

    public string HashString()
    {

        List<int> list = new();

        list.Add((int)(startX & 0b_0111_1111_1111_1111_1111_1111_1111_1111));
        list.Add((int)(startY & 0b_0111_1111_1111_1111_1111_1111_1111_1111));
        list.Add((int)(endX & 0b_0111_1111_1111_1111_1111_1111_1111_1111));
        list.Add((int)(endY & 0b_0111_1111_1111_1111_1111_1111_1111_1111));

        list.Add(pieceToMoveIndex);
        list.Add((int)startBinary);
        list.Add((int)endBinary);
        list.Add(hasMoved ? 1 : 0);

        if (tempPieceIndex != null)
        {
            list.Add((int)tempPieceIndex);
        }

        return JsonSerializer.Serialize(list)+JsonSerializer.Serialize(piecesToRemove)+JsonSerializer.Serialize(piecesToAdd);
    }

    public Ply(){}

    public Ply(GameState gameState, int pieceToMoveIndexNew, BigInteger xOffset, BigInteger yOffset, List<Tuple<Piece, int>> piecesToRemoveNew , List<Tuple<Piece, int>> piecesToAddNew)
    {
        pieceToMoveIndex = pieceToMoveIndexNew;
        Piece pieceToMove = gameState.pieces[pieceToMoveIndex];
        startX = pieceToMove.x;
        startY = pieceToMove.y;
        endX = pieceToMove.x + xOffset;
        endY = pieceToMove.y + yOffset;
        startBinary = pieceToMove.binary;
        endBinary = pieceToMove.binary;
        hasMoved = pieceToMove.hasMoved;
        piecesToRemove = piecesToRemoveNew;
        piecesToAdd = piecesToAddNew;
        if (gameState.tempPieceIndex is not null)
        {
            piecesToRemove.Add(new Tuple<Piece, int>(gameState.pieces[(int)gameState.tempPieceIndex-piecesToRemove.Count], (int)gameState.tempPieceIndex-piecesToRemove.Count));
            tempPieceIndex = gameState.tempPieceIndex;
        }
    }

    public static GameState DoPly(GameState gameState, Ply ply)
    {
        gameState.currentTurn = NextTurn(gameState.currentTurn);
        gameState.pieces[ply.pieceToMoveIndex].x = ply.endX;
        gameState.pieces[ply.pieceToMoveIndex].y = ply.endY;
        gameState.pieces[ply.pieceToMoveIndex].hasMoved = true;
        gameState.pieces[ply.pieceToMoveIndex].binary = ply.endBinary;
        gameState.tempPieceIndex = null;
        foreach (var piece in ply.piecesToRemove)
        {
            gameState.pieces.RemoveAt(piece.Item2);
        }
        foreach (var piece in ply.piecesToAdd)
        {
            gameState.pieces.Insert(piece.Item2, piece.Item1);
            gameState.tempPieceIndex = piece.Item2;
        }
        return gameState;
    }
    public static GameState UndoPly(GameState gameState, Ply ply)
    {
        ply.piecesToAdd.Reverse();
        ply.piecesToRemove.Reverse();
        foreach (var piece in ply.piecesToAdd)
        {
            gameState.pieces.RemoveAt(piece.Item2);
        }
        foreach (var piece in ply.piecesToRemove)
        {
            gameState.pieces.Insert(piece.Item2, piece.Item1);
        }
        gameState.pieces[ply.pieceToMoveIndex].binary = ply.startBinary;
        gameState.tempPieceIndex = ply.tempPieceIndex;
        gameState.pieces[ply.pieceToMoveIndex].hasMoved = ply.hasMoved;
        gameState.pieces[ply.pieceToMoveIndex].y = ply.startY;
        gameState.pieces[ply.pieceToMoveIndex].x = ply.startX;
        gameState.currentTurn = PreviousTurn(gameState.currentTurn);
        //ply.piecesToAdd.Reverse();
        //ply.piecesToRemove.Reverse();
        return gameState;
    }

    static uint NextTurn(uint currentTurn)
    {
        switch (currentTurn)
        {
            case Piece.Color.White:
                currentTurn = Piece.Color.Black;
                break;
            case Piece.Color.Black:
                currentTurn = Piece.Color.White;
                break;
        }
        return currentTurn;
    }
    static uint PreviousTurn(uint currentTurn)
    {
        switch (currentTurn)
        {
            case Piece.Color.White:
                currentTurn = Piece.Color.Black;
                break;
            case Piece.Color.Black:
                currentTurn = Piece.Color.White;
                break;
        }
        return currentTurn;
    }

    public string DebugString()
    {
        string returnString = "";

        returnString = returnString+"Ply:";
        returnString = returnString+"\n    startX: "+startX;
        returnString = returnString+"\n    startY: "+startY;
        returnString = returnString+"\n    endX: "+endX;
        returnString = returnString+"\n    endY: "+endY;
        returnString = returnString+"\n    pieceToMoveIndex: "+pieceToMoveIndex;
        returnString = returnString+"\n    hasMoved: "+hasMoved;
        returnString = returnString+"\n    piecesToAdd: "+piecesToAdd.Count;
        returnString = returnString+"\n    piecesToRemove: "+piecesToRemove.Count;
        returnString = returnString+"\n    startBinary: "+Convert.ToString(startBinary, toBase: 2);
        returnString = returnString+"\n    endBinary: "+Convert.ToString(endBinary, toBase: 2);
        
        return returnString;
    }
}