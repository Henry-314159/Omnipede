namespace omnipede;
using System.Numerics;

public struct GameState
{
    public List<Piece> pieces { get; set; } = new();
    public int? tempPieceIndex { get; set; }    
    public BigInteger? promotionRankWhite { get; set; } 
    public BigInteger? promotionRankBlack { get; set; } 
    public uint[] promotionAllowedPieces { get; set; } = new uint[0];
    public uint currentTurn { get; set; }
    public uint slideLimit { get; set; }

    public GameState(){}

    public string DebugString()
    {
        string returnString = "";

        returnString = returnString+"GameState:";
        returnString = returnString+"\n    pieces:";
        for (int i = 0; i < pieces.Count; i++)
        {
            returnString = returnString+"\n        pieces["+i+"]:";
            returnString = returnString+"\n            binary: "+Convert.ToString(pieces[i].binary, toBase: 2);
            returnString = returnString+"\n            hasMoved: "+pieces[i].hasMoved;
            returnString = returnString+"\n            x: "+pieces[i].x;
            returnString = returnString+"\n            y: "+pieces[i].y;
        }
        returnString = returnString+"\n    tempPieceIndex: "+tempPieceIndex;
        returnString = returnString+"\n    currentTurn: "+Convert.ToString(currentTurn, toBase: 2);
        returnString = returnString+"\n    promotionRankWhite: "+promotionRankWhite.ToString();
        returnString = returnString+"\n    promotionRankBlack: "+promotionRankBlack.ToString();
        
        return returnString;
    }

    public int Hash()
    {
        uint hash = currentTurn + (uint)(tempPieceIndex ?? 0);
        foreach (Piece piece in pieces)
        {
            uint pieceHash = 0;
            pieceHash ^= (uint)(piece.x&0b_1111_1111_1111_1111_1111_1111_1111_1111)<< 16;
            pieceHash ^= (uint)(piece.y&0b_1111_1111_1111_1111_1111_1111_1111_1111);

            pieceHash ^=~(piece.binary&(Piece.Category._MASK|Piece.Color._MASK)) >> 16;
            pieceHash ^= piece.binary << 26;
            pieceHash ^= (uint)Convert.ToInt32(piece.hasMoved) << 15;
            pieceHash ^= currentTurn << 2;

            
            
            //pieceHash <<= piece.x.Sign+2;

            
            hash ^= pieceHash;
            hash ^= hash << 2*(piece.x.Sign+2)+1;
	        hash ^= hash << 13;
	        hash ^= hash >> 17;
	        hash ^= hash << 5;
            hash =  (hash*6257) % 625341599;
            hash ^= hash << 2*(piece.y.Sign+2)+1;
	        hash ^= hash << 13;
	        hash ^= hash >> 17;
	        hash ^= hash << 5;
            hash =  (hash*6257) % 625341599;
            //Console.WriteLine(hash);
        }
        return (int)(hash&0b_0011_1111_1111_1111_1111_1111_1111_1111);
    }

    public int[] Hashints()
    {

        List<int> list = new();

        for (int i = 0; i < pieces.Count; i++)
        {
            list.Add((int)pieces[i].binary);
            list.Add(pieces[i].hasMoved ? 1 : 0);
            list.Add((int)(pieces[i].x & 0b_0111_1111_1111_1111_1111_1111_1111_1111));
            list.Add((int)(pieces[i].y & 0b_0111_1111_1111_1111_1111_1111_1111_1111));
        }
        if (tempPieceIndex != null)
        {
            list.Add((int)tempPieceIndex);
        }
        list.Add((int)currentTurn);

        

        return list.ToArray();
    }

    public static int Evaluate(GameState gameState)
    {
        int evauation = 0;
        for (int i = 0; i < gameState.pieces.Count; i++)
        {
            if ((gameState.pieces[i].binary & Piece.Color._MASK) == Piece.Color.White)
            {
                switch (gameState.pieces[i].binary & Piece.Type._MASK)
                {
                    case Piece.Type.Pawn:
                        evauation += 100;
                        //if (gameState.promotionRankWhite != null)
                        //{
                        //    evauation -= (int)(gameState.promotionRankWhite-gameState.pieces[i].y);
                        //}
                        break;
                    case Piece.Type.Knight:
                        evauation += 300;
                        break;
                    case Piece.Type.Bishop:
                        evauation += 400;
                        break;
                    case Piece.Type.Rook:
                        evauation += 800;
                        break;
                    case Piece.Type.Queen:
                        evauation += 1500;
                        break;
                    case Piece.Type.Guard:
                        evauation += 200;
                        break;
                    case Piece.Type.Hawk:
                        evauation += 1000;
                        break;
                    case Piece.Type.Archbishop:
                        evauation += 1000;
                        break;
                    case Piece.Type.Chancellor:
                        evauation += 1300;
                        break;
                    case Piece.Type.Amazon:
                        evauation += 2000;
                        break;
                }
            }
            else
            {
                switch (gameState.pieces[i].binary & Piece.Type._MASK)
                {
                    case Piece.Type.Pawn:
                        evauation += -100;
                        //if (gameState.promotionRankBlack != null)
                        //{
                        //    evauation -= (int)(gameState.promotionRankBlack-gameState.pieces[i].y);
                        //}
                        break;
                    case Piece.Type.Knight:
                        evauation += -300;
                        break;
                    case Piece.Type.Bishop:
                        evauation += -400;
                        break;
                    case Piece.Type.Rook:
                        evauation += -800;
                        break;
                    case Piece.Type.Queen:
                        evauation += -1500;
                        break;
                    case Piece.Type.Guard:
                        evauation += -200;
                        break;
                    case Piece.Type.Hawk:
                        evauation += -1000;
                        break;
                    case Piece.Type.Archbishop:
                        evauation += -1000;
                        break;
                    case Piece.Type.Chancellor:
                        evauation += -1300;
                        break;
                    case Piece.Type.Amazon:
                        evauation += -2000;
                        break;
                }
            }
        }
        return evauation;
    }
}

