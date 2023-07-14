namespace omnipede;
using System.Text.Json;


public class MoveLister{
    public static List<Position> ListMoves(Position position, bool errorDetection){

        List<Position> output = new();
        List<Position> testPositionList;
        Position? testPosition;
        Position positionBackup;
        positionBackup = position;
        if (errorDetection)
        {
            positionBackup = position.Clone();
        }

        

        if (position.whitesTurn){
            for (int i = 0; i < position.pieces.Count; i++)
            {
                if (errorDetection && JsonSerializer.Serialize(positionBackup) != JsonSerializer.Serialize(position))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                    Console.WriteLine(JsonSerializer.Serialize(positionBackup, new JsonSerializerOptions { WriteIndented = true }));
                    Console.WriteLine("-----");
                    Console.WriteLine(JsonSerializer.Serialize(position, new JsonSerializerOptions { WriteIndented = true }));
                    return output;
                }
                if (position.pieces[i].type.Last() != 'W')
                {
                    
                }
                else if (position.pieces[i].type == "pawnsW")
                {
                    //Forward
                    testPosition = OneSpaceMovementWithCollisonDetection(position, 0, 1, 'W', i, out bool collision);
                    if (!collision)
                    {
                        if (testPosition!=null) {output.Add(testPosition);}
                        if (!position.pieces[i].hasMoved)
                        {
                            testPosition = OneSpaceMovementWithCollisonDetection(position, 0, 2, 'W', i, out collision);
                            if (!collision && testPosition!=null) 
                            {
                                testPosition.enPassentable = testPosition.pieces[0];
                                output.Add(testPosition);
                            }
                        }
                    }
                    //Capture
                    testPosition = OneSpaceMovementWithCollisonDetection(position, 1, 1, 'W', i, out collision);
                    if (collision)
                    {
                        if (testPosition!=null) {output.Add(testPosition);}
                    }
                    testPosition = OneSpaceMovementWithCollisonDetection(position, -1, 1, 'W', i, out collision);
                    if (collision)
                    {
                        if (testPosition!=null) {output.Add(testPosition);}
                    }
                    //En Passent
                    testPosition = OneSpaceMovementEnPassant(position, 1, 1, 'W', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovementEnPassant(position, -1, 1, 'W', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                }
                else if (position.pieces[i].type == "knightsW")
                {
                    testPosition = OneSpaceMovement(position, 1, 2, 'W', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, 2, 1, 'W', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, 1, -2, 'W', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, 2, -1, 'W', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, -1, -2, 'W', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, -2, -1, 'W', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, -1, 2, 'W', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, -2, 1, 'W', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                }
                else if (position.pieces[i].type == "hawksW")
                {
                    testPosition = OneSpaceMovement(position, 0, 2, 'W', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, 2, 2, 'W', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, 2, 0, 'W', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, 2, -2, 'W', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, 0, -2, 'W', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, -2, -2, 'W', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, -2, 0, 'W', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, -2, 2, 'W', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    
                    testPosition = OneSpaceMovement(position, 0, 3, 'W', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, 3, 3, 'W', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, 3, 0, 'W', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, 3, -3, 'W', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, 0, -3, 'W', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, -3, -3, 'W', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, -3, 0, 'W', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, -3, 1, 'W', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                }
                else if (position.pieces[i].type == "amazonsW")
                {
                    testPositionList = Limit7Movement(position, 0, 1, 'W', i);
                    output.AddRange(testPositionList);
                    testPositionList = Limit7Movement(position, 1, 1, 'W', i);
                    output.AddRange(testPositionList);
                    testPositionList = Limit7Movement(position, 1, 0, 'W', i);
                    output.AddRange(testPositionList);
                    testPositionList = Limit7Movement(position, 1, -1, 'W', i);
                    output.AddRange(testPositionList);
                    testPositionList = Limit7Movement(position, 0, -1, 'W', i);
                    output.AddRange(testPositionList);
                    testPositionList = Limit7Movement(position, -1, -1, 'W', i);
                    output.AddRange(testPositionList);
                    testPositionList = Limit7Movement(position, -1, 0, 'W', i);
                    output.AddRange(testPositionList);
                    testPositionList = Limit7Movement(position, -1, 1, 'W', i);
                    output.AddRange(testPositionList);

                    testPosition = OneSpaceMovement(position, 1, 2, 'W', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, 2, 1, 'W', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, 1, -2, 'W', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, 2, -1, 'W', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, -1, -2, 'W', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, -2, -1, 'W', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, -1, 2, 'W', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, -2, 1, 'W', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                }
                else if (position.pieces[i].type == "queensW")
                {
                    testPositionList = Limit7Movement(position, 0, 1, 'W', i);
                    output.AddRange(testPositionList);
                    testPositionList = Limit7Movement(position, 1, 1, 'W', i);
                    output.AddRange(testPositionList);
                    testPositionList = Limit7Movement(position, 1, 0, 'W', i);
                    output.AddRange(testPositionList);
                    testPositionList = Limit7Movement(position, 1, -1, 'W', i);
                    output.AddRange(testPositionList);
                    testPositionList = Limit7Movement(position, 0, -1, 'W', i);
                    output.AddRange(testPositionList);
                    testPositionList = Limit7Movement(position, -1, -1, 'W', i);
                    output.AddRange(testPositionList);
                    testPositionList = Limit7Movement(position, -1, 0, 'W', i);
                    output.AddRange(testPositionList);
                    testPositionList = Limit7Movement(position, -1, 1, 'W', i);
                    output.AddRange(testPositionList);
                }
                else if (position.pieces[i].type == "chancellorsW")
                {
                    testPositionList = Limit7Movement(position, 0, 1, 'W', i);
                    output.AddRange(testPositionList);
                    testPositionList = Limit7Movement(position, 1, 0, 'W', i);
                    output.AddRange(testPositionList);
                    testPositionList = Limit7Movement(position, 0, -1, 'W', i);
                    output.AddRange(testPositionList);
                    testPositionList = Limit7Movement(position, -1, 0, 'W', i);
                    output.AddRange(testPositionList);

                    testPosition = OneSpaceMovement(position, 1, 2, 'W', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, 2, 1, 'W', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, 1, -2, 'W', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, 2, -1, 'W', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, -1, -2, 'W', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, -2, -1, 'W', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, -1, 2, 'W', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, -2, 1, 'W', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                }
                else if (position.pieces[i].type == "archbishopsW")
                {
                    testPositionList = Limit7Movement(position, 1, 1, 'W', i);
                    output.AddRange(testPositionList);
                    testPositionList = Limit7Movement(position, 1, -1, 'W', i);
                    output.AddRange(testPositionList);
                    testPositionList = Limit7Movement(position, -1, -1, 'W', i);
                    output.AddRange(testPositionList);
                    testPositionList = Limit7Movement(position, -1, 1, 'W', i);
                    output.AddRange(testPositionList);

                    testPosition = OneSpaceMovement(position, 1, 2, 'W', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, 2, 1, 'W', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, 1, -2, 'W', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, 2, -1, 'W', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, -1, -2, 'W', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, -2, -1, 'W', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, -1, 2, 'W', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, -2, 1, 'W', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                }
                else if (position.pieces[i].type == "rooksW")
                {
                    testPositionList = Limit7Movement(position, 0, 1, 'W', i);
                    output.AddRange(testPositionList);
                    testPositionList = Limit7Movement(position, 1, 0, 'W', i);
                    output.AddRange(testPositionList);
                    testPositionList = Limit7Movement(position, 0, -1, 'W', i);
                    output.AddRange(testPositionList);
                    testPositionList = Limit7Movement(position, -1, 0, 'W', i);
                    output.AddRange(testPositionList);
                }
                else if (position.pieces[i].type == "bishopsW")
                {
                    testPositionList = Limit7Movement(position, 1, 1, 'W', i);
                    output.AddRange(testPositionList);
                    testPositionList = Limit7Movement(position, 1, -1, 'W', i);
                    output.AddRange(testPositionList);
                    testPositionList = Limit7Movement(position, -1, -1, 'W', i);
                    output.AddRange(testPositionList);
                    testPositionList = Limit7Movement(position, -1, 1, 'W', i);
                    output.AddRange(testPositionList);
                }
                else if (position.pieces[i].type == "gaurdsW")
                {
                    testPosition = OneSpaceMovement(position, 0, 1, 'W', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, 1, 1, 'W', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, 1, 0, 'W', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, 1, -1, 'W', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, 0, -1, 'W', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, -1, -1, 'W', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, -1, 0, 'W', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, -1, 1, 'W', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                }
                else if (position.pieces[i].type == "kingsW")
                {
                    testPosition = OneSpaceMovement(position, 0, 1, 'W', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, 1, 1, 'W', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, 1, 0, 'W', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, 1, -1, 'W', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, 0, -1, 'W', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, -1, -1, 'W', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, -1, 0, 'W', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, -1, 1, 'W', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                }
            }
        }
        //COLOR CHANGE  COLOR CHANGE  COLOR CHANGE  COLOR CHANGE  COLOR CHANGE  
        //COLOR CHANGE  COLOR CHANGE  COLOR CHANGE  COLOR CHANGE  COLOR CHANGE  
        //COLOR CHANGE  COLOR CHANGE  COLOR CHANGE  COLOR CHANGE  COLOR CHANGE  
        //COLOR CHANGE  COLOR CHANGE  COLOR CHANGE  COLOR CHANGE  COLOR CHANGE  
        //COLOR CHANGE  COLOR CHANGE  COLOR CHANGE  COLOR CHANGE  COLOR CHANGE  
        //COLOR CHANGE  COLOR CHANGE  COLOR CHANGE  COLOR CHANGE  COLOR CHANGE  
        //COLOR CHANGE  COLOR CHANGE  COLOR CHANGE  COLOR CHANGE  COLOR CHANGE  
        //COLOR CHANGE  COLOR CHANGE  COLOR CHANGE  COLOR CHANGE  COLOR CHANGE  
        //COLOR CHANGE  COLOR CHANGE  COLOR CHANGE  COLOR CHANGE  COLOR CHANGE  
        //COLOR CHANGE  COLOR CHANGE  COLOR CHANGE  COLOR CHANGE  COLOR CHANGE  
        else
        {
            for (int i = 0; i < position.pieces.Count; i++)
            {
                if (errorDetection && JsonSerializer.Serialize(positionBackup) != JsonSerializer.Serialize(position))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                    Console.WriteLine(JsonSerializer.Serialize(positionBackup, new JsonSerializerOptions { WriteIndented = true }));
                    Console.WriteLine("-----");
                    Console.WriteLine(JsonSerializer.Serialize(position, new JsonSerializerOptions { WriteIndented = true }));
                    return output;
                }
                if (position.pieces[i].type.Last() != 'B')
                {
                    
                }
                else if (position.pieces[i].type == "pawnsB")
                {
                    //Forward
                    testPosition = OneSpaceMovementWithCollisonDetection(position, 0, -1, 'B', i, out bool collision);
                    if (!collision)
                    {
                        if (testPosition!=null) {output.Add(testPosition);}
                        if (!position.pieces[i].hasMoved)
                        {
                            testPosition = OneSpaceMovementWithCollisonDetection(position, 0, -2, 'B', i, out collision);
                            if (!collision && testPosition!=null) 
                            {
                                testPosition.enPassentable = testPosition.pieces[0];
                                output.Add(testPosition);
                            }
                        }
                    }
                    //Capture
                    testPosition = OneSpaceMovementWithCollisonDetection(position, 1, -1, 'B', i, out collision);
                    if (collision)
                    {
                        if (testPosition!=null) {output.Add(testPosition);}
                    }
                    testPosition = OneSpaceMovementWithCollisonDetection(position, -1, -1, 'B', i, out collision);
                    if (collision)
                    {
                        if (testPosition!=null) {output.Add(testPosition);}
                    }
                    //En Passent
                    testPosition = OneSpaceMovementEnPassant(position, 1, -1, 'B', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovementEnPassant(position, -1, -1, 'B', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                }
                else if (position.pieces[i].type == "knightsB")
                {
                    testPosition = OneSpaceMovement(position, 1, 2, 'B', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, 2, 1, 'B', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, 1, -2, 'B', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, 2, -1, 'B', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, -1, -2, 'B', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, -2, -1, 'B', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, -1, 2, 'B', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, -2, 1, 'B', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                }
                else if (position.pieces[i].type == "hawksB")
                {
                    testPosition = OneSpaceMovement(position, 0, 2, 'B', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, 2, 2, 'B', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, 2, 0, 'B', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, 2, -2, 'B', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, 0, -2, 'B', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, -2, -2, 'B', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, -2, 0, 'B', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, -2, 2, 'B', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    
                    testPosition = OneSpaceMovement(position, 0, 3, 'B', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, 3, 3, 'B', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, 3, 0, 'B', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, 3, -3, 'B', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, 0, -3, 'B', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, -3, -3, 'B', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, -3, 0, 'B', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, -3, 1, 'B', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                }
                else if (position.pieces[i].type == "amazonsB")
                {
                    testPositionList = Limit7Movement(position, 0, 1, 'B', i);
                    output.AddRange(testPositionList);
                    testPositionList = Limit7Movement(position, 1, 1, 'B', i);
                    output.AddRange(testPositionList);
                    testPositionList = Limit7Movement(position, 1, 0, 'B', i);
                    output.AddRange(testPositionList);
                    testPositionList = Limit7Movement(position, 1, -1, 'B', i);
                    output.AddRange(testPositionList);
                    testPositionList = Limit7Movement(position, 0, -1, 'B', i);
                    output.AddRange(testPositionList);
                    testPositionList = Limit7Movement(position, -1, -1, 'B', i);
                    output.AddRange(testPositionList);
                    testPositionList = Limit7Movement(position, -1, 0, 'B', i);
                    output.AddRange(testPositionList);
                    testPositionList = Limit7Movement(position, -1, 1, 'B', i);
                    output.AddRange(testPositionList);

                    testPosition = OneSpaceMovement(position, 1, 2, 'B', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, 2, 1, 'B', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, 1, -2, 'B', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, 2, -1, 'B', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, -1, -2, 'B', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, -2, -1, 'B', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, -1, 2, 'B', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, -2, 1, 'B', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                }
                else if (position.pieces[i].type == "queensB")
                {
                    testPositionList = Limit7Movement(position, 0, 1, 'B', i);
                    output.AddRange(testPositionList);
                    testPositionList = Limit7Movement(position, 1, 1, 'B', i);
                    output.AddRange(testPositionList);
                    testPositionList = Limit7Movement(position, 1, 0, 'B', i);
                    output.AddRange(testPositionList);
                    testPositionList = Limit7Movement(position, 1, -1, 'B', i);
                    output.AddRange(testPositionList);
                    testPositionList = Limit7Movement(position, 0, -1, 'B', i);
                    output.AddRange(testPositionList);
                    testPositionList = Limit7Movement(position, -1, -1, 'B', i);
                    output.AddRange(testPositionList);
                    testPositionList = Limit7Movement(position, -1, 0, 'B', i);
                    output.AddRange(testPositionList);
                    testPositionList = Limit7Movement(position, -1, 1, 'B', i);
                    output.AddRange(testPositionList);
                }
                else if (position.pieces[i].type == "chancellorsB")
                {
                    testPositionList = Limit7Movement(position, 0, 1, 'B', i);
                    output.AddRange(testPositionList);
                    testPositionList = Limit7Movement(position, 1, 0, 'B', i);
                    output.AddRange(testPositionList);
                    testPositionList = Limit7Movement(position, 0, -1, 'B', i);
                    output.AddRange(testPositionList);
                    testPositionList = Limit7Movement(position, -1, 0, 'B', i);
                    output.AddRange(testPositionList);

                    testPosition = OneSpaceMovement(position, 1, 2, 'B', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, 2, 1, 'B', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, 1, -2, 'B', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, 2, -1, 'B', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, -1, -2, 'B', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, -2, -1, 'B', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, -1, 2, 'B', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, -2, 1, 'B', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                }
                else if (position.pieces[i].type == "archbishopsB")
                {
                    testPositionList = Limit7Movement(position, 1, 1, 'B', i);
                    output.AddRange(testPositionList);
                    testPositionList = Limit7Movement(position, 1, -1, 'B', i);
                    output.AddRange(testPositionList);
                    testPositionList = Limit7Movement(position, -1, -1, 'B', i);
                    output.AddRange(testPositionList);
                    testPositionList = Limit7Movement(position, -1, 1, 'B', i);
                    output.AddRange(testPositionList);

                    testPosition = OneSpaceMovement(position, 1, 2, 'B', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, 2, 1, 'B', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, 1, -2, 'B', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, 2, -1, 'B', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, -1, -2, 'B', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, -2, -1, 'B', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, -1, 2, 'B', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, -2, 1, 'B', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                }
                else if (position.pieces[i].type == "rooksB")
                {
                    testPositionList = Limit7Movement(position, 0, 1, 'B', i);
                    output.AddRange(testPositionList);
                    testPositionList = Limit7Movement(position, 1, 0, 'B', i);
                    output.AddRange(testPositionList);
                    testPositionList = Limit7Movement(position, 0, -1, 'B', i);
                    output.AddRange(testPositionList);
                    testPositionList = Limit7Movement(position, -1, 0, 'B', i);
                    output.AddRange(testPositionList);
                }
                else if (position.pieces[i].type == "bishopsB")
                {
                    testPositionList = Limit7Movement(position, 1, 1, 'B', i);
                    output.AddRange(testPositionList);
                    testPositionList = Limit7Movement(position, 1, -1, 'B', i);
                    output.AddRange(testPositionList);
                    testPositionList = Limit7Movement(position, -1, -1, 'B', i);
                    output.AddRange(testPositionList);
                    testPositionList = Limit7Movement(position, -1, 1, 'B', i);
                    output.AddRange(testPositionList);
                }
                else if (position.pieces[i].type == "gaurdsB")
                {
                    testPosition = OneSpaceMovement(position, 0, 1, 'B', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, 1, 1, 'B', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, 1, 0, 'B', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, 1, -1, 'B', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, 0, -1, 'B', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, -1, -1, 'B', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, -1, 0, 'B', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, -1, 1, 'B', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                }
                else if (position.pieces[i].type == "kingsB")
                {
                    testPosition = OneSpaceMovement(position, 0, 1, 'B', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, 1, 1, 'B', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, 1, 0, 'B', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, 1, -1, 'B', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, 0, -1, 'B', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, -1, -1, 'B', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, -1, 0, 'B', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                    testPosition = OneSpaceMovement(position, -1, 1, 'B', i);
                    if (testPosition!=null) {output.Add(testPosition);}
                }
            }
        }

        output.Reverse();
        return output;

    }
    public static Position? OneSpaceMovement(Position position, long xOffset, long yOffset, char friendEnd, int i)
    {
        Position position1 = (Position)position.Clone();

        for (int j = 0; j < position.pieces.Count; j++)
        {
            if (position.pieces[j].xCoord == position.pieces[i].xCoord + xOffset & position.pieces[j].yCoord == position.pieces[i].yCoord + yOffset)
            {
                if (position.pieces[j].type.Last() != friendEnd)
                {
                    position1.pieces.RemoveAt(j);
                    position1.pieces.Insert(j, position.pieces[i].Clone());
                    position1.pieces[j].xCoord = position.pieces[i].xCoord + xOffset;
                    position1.pieces[j].yCoord = position.pieces[i].yCoord + yOffset;
                    position1.pieces[j].hasMoved = true;
                    position1.pieces.RemoveAt(i);
                    position1.enPassentable = null;
                    position1.whitesTurn = !position1.whitesTurn;
                    return(position1);
                }
                else
                {
                    return(null);
                }
            }
        }
        position1.pieces.Add(position1.pieces[i]);
        position1.pieces.RemoveAt(i);
        position1.pieces[^1].xCoord = position.pieces[i].xCoord + xOffset;
        position1.pieces[^1].yCoord = position.pieces[i].yCoord + yOffset;
        position1.pieces[^1].hasMoved = true;
        position1.whitesTurn = !position1.whitesTurn;
        position1.enPassentable = null;
        return(position1);
    }
    public static Position? OneSpaceMovementWithCollisonDetection(Position position, long xOffset, long yOffset, char friendEnd, int i, out bool collision)
    {
        Position position1 = (Position)position.Clone();

        for (int j = 0; j < position.pieces.Count; j++)
        {
            if (position.pieces[j].xCoord == position.pieces[i].xCoord + xOffset & position.pieces[j].yCoord == position.pieces[i].yCoord + yOffset)
            {
                if (position.pieces[j].type.Last() != friendEnd)
                {
                    position1.pieces.RemoveAt(j);
                    position1.pieces.Insert(j, position.pieces[i].Clone());
                    position1.pieces[j].xCoord = position.pieces[i].xCoord + xOffset;
                    position1.pieces[j].yCoord = position.pieces[i].yCoord + yOffset;
                    position1.pieces[j].hasMoved = true;
                    position1.pieces.RemoveAt(i);
                    collision = true;
                    position1.enPassentable = null;
                    position1.whitesTurn = !position1.whitesTurn;
                    return(position1);
                }
                else 
                {
                    collision = true;
                    return(null);
                }
            }
        }
        position1.pieces.Insert(0, position1.pieces[i]);
        position1.pieces.RemoveAt(i+1);
        position1.pieces[0].xCoord = position.pieces[i].xCoord + xOffset;
        position1.pieces[0].yCoord = position.pieces[i].yCoord + yOffset;
        position1.pieces[0].hasMoved = true;
        collision = false;
        position1.enPassentable = null;
        position1.whitesTurn = !position1.whitesTurn;
        return(position1);
    }
    public static Position? OneSpaceMovementEnPassant(Position position, long xOffset, long yOffset, char friendEnd, int i)
    {
        Position position1 = (Position)position.Clone();

        if (position.enPassentable !=null)
        {
            if (position.enPassentable.xCoord == position.pieces[i].xCoord + xOffset && position.enPassentable.yCoord == position.pieces[i].yCoord)
            {
                position1.pieces.RemoveAt(0);
                position1.pieces.Insert(0, position1.pieces[i-1]);
                position1.pieces[0].xCoord = position1.pieces[0].xCoord + xOffset;
                position1.pieces[0].yCoord = position1.pieces[0].yCoord + yOffset;
                position1.pieces[0].hasMoved = true;
                position1.pieces.RemoveAt(i);
                position1.enPassentable = null;
                position1.whitesTurn = !position1.whitesTurn;
                return(position1);
            }
        }
        position1.enPassentable = null;
        return(null);
    }

    public static List<Position> Limit7Movement(Position position, long xOffset, long yOffset, char friendEnd, int i){


        List<Position> outputPosition = new();
        Position? testPosition = null;

        for (int k = 0; k < 7; k++)
        {
            testPosition = OneSpaceMovementWithCollisonDetection(position, xOffset*(k+1), yOffset*(k+1), friendEnd, i, out bool collision);
            
            if (testPosition!=null) 
            {
                outputPosition.Add(testPosition);
            }
            if (collision) {break;}
        }
        return outputPosition;
    }
}