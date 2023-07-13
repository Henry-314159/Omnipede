namespace omnipede;

class PositionEvaluator
{
    public static int PointBased(Position position)
    {
        int positionValue = 0;
        for (int i = 0; i < position.pieces.Count; i++)
        {
            if (position.pieces[i].type == "kingsW")
            {
                positionValue += 100000000;
            }
            else if (position.pieces[i].type == "amazonsW")
            {
                positionValue += 2000;
            }
            else if (position.pieces[i].type == "queensW")
            {
                positionValue += 1400;
            }
            else if (position.pieces[i].type == "chancellorsW")
            {
                positionValue += 1100;
            }
            else if (position.pieces[i].type == "archbishopsW")
            {
                positionValue += 1000;
            }
            else if (position.pieces[i].type == "hawksW")
            {
                positionValue += 800;
            }
            else if (position.pieces[i].type == "rooksW")
            {
                positionValue += 700;
            }
            else if (position.pieces[i].type == "bishopsW")
            {
                positionValue += 600;
            }
            else if (position.pieces[i].type == "knightsW")
            {
                positionValue += 255;
            }
            else if (position.pieces[i].type == "guardsW")
            {
                positionValue += 250;
            }
            else if (position.pieces[i].type == "pawnsW")
            {
                positionValue += 80;
            }
            else if (position.pieces[i].type == "kingsB")
            {
                positionValue += -100000000;
            }
            else if (position.pieces[i].type == "amazonsB")
            {
                positionValue += -2000;
            }
            else if (position.pieces[i].type == "queensB")
            {
                positionValue += -1400;
            }
            else if (position.pieces[i].type == "chancellorsB")
            {
                positionValue += -1100;
            }
            else if (position.pieces[i].type == "archbishopsB")
            {
                positionValue += -1000;
            }
            else if (position.pieces[i].type == "hawksB")
            {
                positionValue += -800;
            }
            else if (position.pieces[i].type == "rooksB")
            {
                positionValue += -700;
            }
            else if (position.pieces[i].type == "bishopsB")
            {
                positionValue += -600;
            }
            else if (position.pieces[i].type == "knightsB")
            {
                positionValue += -255;
            }
            else if (position.pieces[i].type == "guardsB")
            {
                positionValue += -250;
            }
            else if (position.pieces[i].type == "pawnsB")
            {
                positionValue += -80;
            }
            //Console.WriteLine(position.pieces[i].type[0]);
            //Console.WriteLine(((int)position.pieces[i].type[^1]).ToString("X4"));
            //Console.WriteLine(position.pieces[i].type +", "+ positionValue);
        }

        return positionValue;
    }

    public static int PointBasedPlusYBonus(Position position)
    {
        int positionValue = 0;
        for (int i = 0; i < position.pieces.Count; i++)
        {
            positionValue += (int)position.pieces[i].yCoord;

            //LOLLOLOLOLOLLOLOL//if (position.pieces[i].type == "kingsW")
            //LOLLOLOLOLOLLOLOL//{
            //LOLLOLOLOLOLLOLOL//    positionValue += 100000000;                
            //LOLLOLOLOLOLLOLOL//}
            //LOLLOLOLOLOLLOLOL//else if (position.pieces[i].type == "amazonsW")
            //LOLLOLOLOLOLLOLOL//{
            //LOLLOLOLOLOLLOLOL//    positionValue += 2000;                
            //LOLLOLOLOLOLLOLOL//}
            //LOLLOLOLOLOLLOLOL//else if (position.pieces[i].type == "queensW")
            //LOLLOLOLOLOLLOLOL//{
            //LOLLOLOLOLOLLOLOL//    positionValue += 1400;               
            //LOLLOLOLOLOLLOLOL//}
            //LOLLOLOLOLOLLOLOL//else if (position.pieces[i].type == "chancellorsW")
            //LOLLOLOLOLOLLOLOL//{
            //LOLLOLOLOLOLLOLOL//    positionValue += 1100;               
            //LOLLOLOLOLOLLOLOL//}
            //LOLLOLOLOLOLLOLOL//else if (position.pieces[i].type == "archbishopsW")
            //LOLLOLOLOLOLLOLOL//{
            //LOLLOLOLOLOLLOLOL//    positionValue += 1000;                
            //LOLLOLOLOLOLLOLOL//}
            //LOLLOLOLOLOLLOLOL//else if (position.pieces[i].type == "hawksW")
            //LOLLOLOLOLOLLOLOL//{
            //LOLLOLOLOLOLLOLOL//    positionValue += 800;
            //LOLLOLOLOLOLLOLOL//}
            //LOLLOLOLOLOLLOLOL//else if (position.pieces[i].type == "rooksW")
            //LOLLOLOLOLOLLOLOL//{
            //LOLLOLOLOLOLLOLOL//    positionValue += 700;
            //LOLLOLOLOLOLLOLOL//}
            //LOLLOLOLOLOLLOLOL//else if (position.pieces[i].type == "bishopsW")
            //LOLLOLOLOLOLLOLOL//{
            //LOLLOLOLOLOLLOLOL//    positionValue += 600;                
            //LOLLOLOLOLOLLOLOL//}
            //LOLLOLOLOLOLLOLOL//else if (position.pieces[i].type == "knightsW")
            //LOLLOLOLOLOLLOLOL//{
            //LOLLOLOLOLOLLOLOL//    positionValue += 255;                
            //LOLLOLOLOLOLLOLOL//}
            //LOLLOLOLOLOLLOLOL//else if (position.pieces[i].type == "guardsW")
            //LOLLOLOLOLOLLOLOL//{
            //LOLLOLOLOLOLLOLOL//    positionValue += 250;                
            //LOLLOLOLOLOLLOLOL//}
            //LOLLOLOLOLOLLOLOL//else if (position.pieces[i].type == "pawnsW")
            //LOLLOLOLOLOLLOLOL//{
            //LOLLOLOLOLOLLOLOL//    positionValue += 80;               
            //LOLLOLOLOLOLLOLOL//}
            //LOLLOLOLOLOLLOLOL//else if (position.pieces[i].type == "kingsB")
            //LOLLOLOLOLOLLOLOL//{
            //LOLLOLOLOLOLLOLOL//    positionValue += -100000000;
            //LOLLOLOLOLOLLOLOL//}
            //LOLLOLOLOLOLLOLOL//else if (position.pieces[i].type == "amazonsB")
            //LOLLOLOLOLOLLOLOL//{
            //LOLLOLOLOLOLLOLOL//    positionValue += -2000;
            //LOLLOLOLOLOLLOLOL//}
            //LOLLOLOLOLOLLOLOL//else if (position.pieces[i].type == "queensB")
            //LOLLOLOLOLOLLOLOL//{
            //LOLLOLOLOLOLLOLOL//    positionValue += -1400;
            //LOLLOLOLOLOLLOLOL//}
            //LOLLOLOLOLOLLOLOL//else if (position.pieces[i].type == "chancellorsB")
            //LOLLOLOLOLOLLOLOL//{
            //LOLLOLOLOLOLLOLOL//    positionValue += -1100;
            //LOLLOLOLOLOLLOLOL//}
            //LOLLOLOLOLOLLOLOL//else if (position.pieces[i].type == "archbishopsB")
            //LOLLOLOLOLOLLOLOL//{
            //LOLLOLOLOLOLLOLOL//    positionValue += -1000;
            //LOLLOLOLOLOLLOLOL//}
            //LOLLOLOLOLOLLOLOL//else if (position.pieces[i].type == "hawksB")
            //LOLLOLOLOLOLLOLOL//{
            //LOLLOLOLOLOLLOLOL//    positionValue += -800;
            //LOLLOLOLOLOLLOLOL//}
            //LOLLOLOLOLOLLOLOL//else if (position.pieces[i].type == "rooksB")
            //LOLLOLOLOLOLLOLOL//{
            //LOLLOLOLOLOLLOLOL//    positionValue += -700;
            //LOLLOLOLOLOLLOLOL//}
            //LOLLOLOLOLOLLOLOL//else if (position.pieces[i].type == "bishopsB")
            //LOLLOLOLOLOLLOLOL//{
            //LOLLOLOLOLOLLOLOL//    positionValue += -600;
            //LOLLOLOLOLOLLOLOL//}
            //LOLLOLOLOLOLLOLOL//else if (position.pieces[i].type == "knightsB")
            //LOLLOLOLOLOLLOLOL//{
            //LOLLOLOLOLOLLOLOL//    positionValue += -255;
            //LOLLOLOLOLOLLOLOL//}
            //LOLLOLOLOLOLLOLOL//else if (position.pieces[i].type == "guardsB")
            //LOLLOLOLOLOLLOLOL//{
            //LOLLOLOLOLOLLOLOL//    positionValue += -250;
            //LOLLOLOLOLOLLOLOL//}
            //LOLLOLOLOLOLLOLOL//else if (position.pieces[i].type == "pawnsB")
            //LOLLOLOLOLOLLOLOL//{
            //LOLLOLOLOLOLLOLOL//    positionValue += -80;
            //LOLLOLOLOLOLLOLOL//}
            //Console.WriteLine(position.pieces[i].type[0]);
            //Console.WriteLine(((int)position.pieces[i].type[^1]).ToString("X4"));
            //Console.WriteLine(position.pieces[i].type +", "+ positionValue);
        }

        return positionValue;
    }
    public static bool KingsExist(Position position)
    {
        bool whiteKing = false;
        bool blackKing = false;
        for (int i = 0; i < position.pieces.Count; i++)
        {
            if (position.pieces[i].type == "kingsW")
            {
                whiteKing = true;
            }
            if (position.pieces[i].type == "kingsB")
            {
                blackKing = true;
            }
        }
        return whiteKing && blackKing;
    }
}