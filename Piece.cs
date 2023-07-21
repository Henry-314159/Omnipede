namespace omnipede;
using System.Numerics;

public class Piece
{
    public BigInteger x { get; set; }
    public BigInteger y { get; set; }
    public uint binary { get; set; }
    public bool hasMoved { get; set; } = false;

    public class Type                                                  
    {
        public const uint      _MASK = 0b_0000_0000_0000_0000_1111_1111_1111_1111;
        public const uint      _NULL = 0b_0000_0000_0000_0000_0000_0000_0000_0000;
        public const uint       Pawn = 0b_0000_0000_0000_0000_0000_0000_0000_0001;
        public const uint     Knight = 0b_0000_0000_0000_0000_0000_0000_0000_0010;
        public const uint     Bishop = 0b_0000_0000_0000_0000_0000_0000_0000_0011;
        public const uint       Rook = 0b_0000_0000_0000_0000_0000_0000_0000_0100;
        public const uint      Queen = 0b_0000_0000_0000_0000_0000_0000_0000_0101;
        public const uint       King = 0b_0000_0000_0000_0000_0000_0000_0000_0110;
        public const uint      Guard = 0b_0000_0000_0000_0000_0000_0000_0000_0111;
        public const uint       Hawk = 0b_0000_0000_0000_0000_0000_0000_0000_1000;
        public const uint Archbishop = 0b_0000_0000_0000_0000_0000_0000_0000_1001;
        public const uint Chancellor = 0b_0000_0000_0000_0000_0000_0000_0000_1010;
        public const uint     Amazon = 0b_0000_0000_0000_0000_0000_0000_0000_1011;
    }
    public class Color
    //                                                     1_0000_0000_0000_0000
    {//                                                    1_0000_0000_0000_0000
        public const uint    _MASK = 0b_0000_0000_1111_1111_0000_0000_0000_0000;
        public const uint _NUETRAL = 0b_0000_0000_0000_0000_0000_0000_0000_0000;
        public const uint    White = 0b_0000_0000_0000_0001_0000_0000_0000_0000;
        public const uint    Black = 0b_0000_0000_0000_0010_0000_0000_0000_0000;
    }
    public class Category
    {
        public const uint                 _MASK = 0b_1111_1111_0000_0000_0000_0000_0000_0000;
        public const uint               _NORMAL = 0b_0000_0000_0000_0000_0000_0000_0000_0000;
        public const uint EnPassantCaptureSpace = 0b_0000_0001_0000_0000_0000_0000_0000_0000;
    }
    

    public static uint DecodeName(string name)
    {
        uint binary = 0;
        switch (name)
        {
            case "pawnsW":
                binary = Piece.Type.Pawn | Piece.Color.White;
                break;
            case "knightsW":
                binary = Piece.Type.Knight | Piece.Color.White;
                break;
            case "bishopsW":
                binary = Piece.Type.Bishop | Piece.Color.White;
                break;
            case "rooksW":
                binary = Piece.Type.Rook | Piece.Color.White;
                break;
            case "queensW":
                binary = Piece.Type.Queen | Piece.Color.White;
                break;
            case "kingsW":
                binary = Piece.Type.King | Piece.Color.White;
                break;
            case "guardsW":
                binary = Piece.Type.Guard | Piece.Color.White;
                break;
            case "hawksW":
                binary = Piece.Type.Hawk | Piece.Color.White;
                break;
            case "archbishopW":
                binary = Piece.Type.Archbishop | Piece.Color.White;
                break;
            case "chancellorsW":
                binary = Piece.Type.Chancellor | Piece.Color.White;
                break;
            case "amazonsW":
                binary = Piece.Type.Amazon | Piece.Color.White;
                break;
            case "pawnsB":
                binary = Piece.Type.Pawn | Piece.Color.Black;
                break;
            case "knightsB":
                binary = Piece.Type.Knight | Piece.Color.Black;
                break;
            case "bishopsB":
                binary = Piece.Type.Bishop | Piece.Color.Black;
                break;
            case "rooksB":
                binary = Piece.Type.Rook | Piece.Color.Black;
                break;
            case "queensB":
                binary = Piece.Type.Queen | Piece.Color.Black;
                break;
            case "kingsB":
                binary = Piece.Type.King | Piece.Color.Black;
                break;
            case "guardsB":
                binary = Piece.Type.Guard | Piece.Color.Black;
                break;
            case "hawksB":
                binary = Piece.Type.Hawk | Piece.Color.Black;
                break;
            case "archbishopB":
                binary = Piece.Type.Archbishop | Piece.Color.Black;
                break;
            case "chancellorsB":
                binary = Piece.Type.Chancellor | Piece.Color.Black;
                break;
            case "amazonsB":
                binary = Piece.Type.Amazon | Piece.Color.Black;
                break;
        }
        return binary;
    }
}
