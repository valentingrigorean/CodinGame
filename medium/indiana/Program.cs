using System;
using System.Collections.Generic;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class Player
{

    static IDictionary<int, List<int>> dict = new Dictionary<int, List<int>>();

    public static string GetDirection(int x, int y, string from)
    {
        List<int> currentRow = dict[y];
        switch (currentRow[x])
        {
            case 0:
                break;
            case 1:
            case 3:
            case 7:
            case 8:
            case 9:
            case 12:
            case 13:
                y++;
                break;
            case 2:
            case 6:
                switch (from)
                {
                    case "LEFT":
                        x++;
                        break;
                    case "RIGHT":
                        x--;
                        break;
                }
                break;
            case 4:
                switch (from)
                {
                    case "TOP":
                        x--;
                        break;
                    case "RIGHT":
                        y++;
                        break;
                }
                break;
            case 5:
                switch (from)
                {
                    case "LEFT":
                        y++;
                        break;
                    case "TOP":
                        x++;
                        break;
                }
                break;
            case 10:
                x--;
                break;
            case 11:
                x++;
                break;
        }
        return x + " " + y;
    }
    static void Main(string[] args)
    {
        string[] inputs;
        inputs = Console.ReadLine().Split(' ');
        int W = int.Parse(inputs[0]); // number of columns.
        int H = int.Parse(inputs[1]); // number of rows.      
        for (int i = 0; i < H; i++)
        {
            string LINE = Console.ReadLine(); // represents a line in the grid and contains W integers. Each integer represents one room of a given type.
            List<int> currentRow = new List<int>();
            foreach (string s in LINE.Split(' '))
                currentRow.Add(int.Parse(s));
            dict.Add(i, currentRow);
        }
        int EX = int.Parse(Console.ReadLine()); // the coordinate along the X axis of the exit (not useful for this first mission, but must be read).

        // game loop
        while (true)
        {
            inputs = Console.ReadLine().Split(' ');
            int XI = int.Parse(inputs[0]);
            int YI = int.Parse(inputs[1]);
            string POS = inputs[2];

            // Write an action using Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");

            Console.WriteLine(GetDirection(XI, YI, POS)); // One line containing the X Y coordinates of the room in which you believe Indy will be on the next turn.
        }
    }
}