using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class GameBoard
{
    public bool[,] Board { get; set; } // 5x10 board
    public bool[,] TempBoard { get; set; } // 5x10 temporary board for hovering piece
    public int PieceX { get; set; } // X-coordinate of the top-left corner of the hovering piece
    public int PieceY { get; set; } // Y-coordinate of the top-left corner of the hovering piece
    public bool[,] CurrentPiece { get; set; } // 4x4 or 3x3 piece that is currently hovering

    public GameBoard()
    {
        Board = new bool[9, 14];
        TempBoard = new bool[9, 14];
        Debug.Log("Game board created.");
    }

    public void UpdateTempBoard()
    {
        // Clear TempBoard
        for (int x = 0; x < 9; x++)
        {
            for (int y = 0; y < 14; y++)
            {
                TempBoard[x, y] = false;
            }
        }

        // Fill TempBoard based on CurrentPiece and its position (PieceX, PieceY)
        int pieceWidth = CurrentPiece.GetLength(0);
        int pieceHeight = CurrentPiece.GetLength(1);
        for (int x = 0; x < pieceHeight; x++)
        {
            for (int y = 0; y < pieceWidth; y++)
            {
                if (CurrentPiece[x, y])
                {
                    TempBoard[PieceX + x, PieceY + y] = true;
                }
            }
        }

        // DEBUG
        // Display TempBoard
        StringBuilder row = new StringBuilder();
        Debug.Log("Tempboard: ");
        for (int x = 2; x < 7; x++)
        {
            StringBuilder cellRow = new StringBuilder();  // Initialize a new StringBuilder for each row
            for (int y = 2; y < 12; y++)
            {
                string cell = TempBoard[x, y] ? "1" : "0";  // Use ternary conditional operator correctly
                cellRow.Append(cell);  // Use StringBuilder's Append method
            }
            cellRow.Append("\n");  // Append new line character to each row
            row.Append(cellRow);  // Append this row to the main string
        }
        Debug.Log(row.ToString());  // Convert StringBuilder to string and log it

        // Display Board
        row = new StringBuilder();
        Debug.Log("Board: ");
        for (int x = 2; x < 7; x++)
        {
            StringBuilder cellRow = new StringBuilder();  // Initialize a new StringBuilder for each row
            for (int y = 2; y < 12; y++)
            {
                string cell = Board[x, y] ? "1" : "0";  // Use ternary conditional operator correctly
                cellRow.Append(cell);  // Use StringBuilder's Append method
            }
            cellRow.Append("\n");  // Append new line character to each row
            row.Append(cellRow);  // Append this row to the main string
        }
        Debug.Log(row.ToString());  // Convert StringBuilder to string and log it
    }

    public bool CanInsert()
    {
        for (int x = 2; x < 7; x++)
        {
            for (int y = 2; y < 12; y++)
            {
                if (TempBoard[x, y] && Board[x, y])
                {
                    return false; // Collision detected
                }
            }
        }
        return true; // No collision
    }

    public void InsertPiece()
    {
        for (int x = 2; x < 7; x++)
        {
            for (int y = 2; y < 12; y++)
            {
                Board[x, y] |= TempBoard[x, y]; // Logical OR to insert
            }
        }
    }

    public void RemovePiece()
    {
        for (int x = 2; x < 7; x++)
        {
            for (int y = 2; y < 12; y++)
            {
                Board[x, y] ^= TempBoard[x, y]; // Logical XOR to remove
            }
        }
    }

    public bool CanMove(int dx, int dy)
    {
        // dx, dy are the changes in x and y coordinates
        // UP
        if (dx == -1)
        {
            for (int y = 2; y < 12; y++)
            {
                if (TempBoard[2, y])
                {
                    return false; // Cannot move up
                }
            }
        }
        // DOWN
        else if (dx == 1)
        {
            for (int y = 2; y < 12; y++)
            {
                if (TempBoard[6, y])
                {
                    return false; // Cannot move down
                }
            }
        }
        // LEFT
        else if (dy == -1)
        {
            for (int x = 2; x < 7; x++)
            {
                if (TempBoard[x, 2])
                {
                    return false; // Cannot move left
                }
            }
        }
        // RIGHT
        else
        {
            for (int x = 2; x < 7; x++)
            {
                if (TempBoard[x, 11])
                {
                    return false; // Cannot move left
                }
            }
        }

        return true;
    }

    public void AdjustRotation()
    {
        AdjustVertically();
        AdjustHorizontally();
        UpdateTempBoard();
    }
    public void AdjustVertically()
    {
        for (int y = 2; y < 12; y++)
        {
            if (TempBoard[0, y])
            {
                GameControl.blocks[GameControl.currentBlock].go.transform.position += Vector3.right * 2.5f;
                PieceX += 2;
                GameControl.blocks[GameControl.currentBlock].coorX = PieceX;
                return;
            }
        }
        for (int y = 2; y < 12; y++)
        {
            if (TempBoard[1, y])
            {
                GameControl.blocks[GameControl.currentBlock].go.transform.position += Vector3.right * 1.25f;
                PieceX = ++GameControl.blocks[GameControl.currentBlock].coorX;
                return;
            }
        }
        for (int y = 2; y < 12; y++)
        {
            if (TempBoard[8, y])
            {
                GameControl.blocks[GameControl.currentBlock].go.transform.position += Vector3.left * 2.5f;
                PieceX -= 2;
                GameControl.blocks[GameControl.currentBlock].coorX = PieceX;
                return;
            }
        }
        for (int y = 2; y < 12; y++)
        {
            if (TempBoard[7, y])
            {
                GameControl.blocks[GameControl.currentBlock].go.transform.position += Vector3.left * 1.25f;
                PieceX = --GameControl.blocks[GameControl.currentBlock].coorX;
                return;
            }
        }
    }
    public void AdjustHorizontally()
    {
        for (int x = 2; x < 7; x++)
        {
            if (TempBoard[x, 0])
            {
                GameControl.blocks[GameControl.currentBlock].go.transform.position += Vector3.forward * 2.5f;
                PieceY += 2;
                GameControl.blocks[GameControl.currentBlock].coorY = PieceY;
                return;
            }
        }
        for (int x = 2; x < 7; x++)
        {
            if (TempBoard[x, 1])
            {
                GameControl.blocks[GameControl.currentBlock].go.transform.position += Vector3.forward * 1.25f;
                PieceY = ++GameControl.blocks[GameControl.currentBlock].coorY;
                return;
            }
        }
        for (int x = 2; x < 7; x++)
        {
            if (TempBoard[x, 13])
            {
                GameControl.blocks[GameControl.currentBlock].go.transform.position += Vector3.back * 2.5f;
                PieceY -= 2;
                GameControl.blocks[GameControl.currentBlock].coorY = PieceY;
                return;
            }
        }
        for (int x = 2; x < 7; x++)
        {
            if (TempBoard[x, 12])
            {
                GameControl.blocks[GameControl.currentBlock].go.transform.position += Vector3.back * 1.25f;
                PieceY = --GameControl.blocks[GameControl.currentBlock].coorY;
                return;
            }
        }
    }

    public bool IsLevelCompleted()
    {
        for (int x = 2; x < 7; x++)
        {
            for (int y = 2; y < 12; y++)
            {
                if (Board[x, y] == false) return false;
            }
        }
        return true;
    }
}

public partial class GameControl
{
    public static GameBoard gameBoard;


    public static void FillHoles()
    {
        Debug.Log("Filling holes.");
        string[] realBoard = new string[5];
        string[] extendedBoard;
        if (GameStart.lvl == 1) { realBoard = new string[] {"1110111111", 
                                                            "1110111111", 
                                                            "1100111111", 
                                                            "1110111101", 
                                                            "1111110000"}; }
        else if (GameStart.lvl == 2) { realBoard = new string[] {"1111111100", 
                                                                 "1111110000", 
                                                                 "1111111111", 
                                                                 "1110111111", 
                                                                 "0000111111"}; }
        else if (GameStart.lvl == 3) { realBoard = new string[] {"0000011111", 
                                                                 "0011011111", 
                                                                 "0111111111", 
                                                                 "0011111111", 
                                                                 "1111111111"}; }
        else if (GameStart.lvl == 4) { realBoard = new string[] {"1111111111", 
                                                                 "1111111100", 
                                                                 "1111111110", 
                                                                 "1111110100", 
                                                                 "1111100000"}; }
        else if (GameStart.lvl == 5) { realBoard = new string[] {"1111111111", 
                                                                 "1000111111", 
                                                                 "0001111111", 
                                                                 "0001111111", 
                                                                 "1111111111"}; }
        else if (GameStart.lvl == 6) { realBoard = new string[] {"1111000011", 
                                                                 "1111110111", 
                                                                 "1111110000", 
                                                                 "1111111010", 
                                                                 "1111111111"}; }
        else if (GameStart.lvl == 25) { realBoard = new string[] {"1000110000", 
                                                                  "1100010011", 
                                                                  "1100110001", 
                                                                  "1101111111", 
                                                                  "1111111111"}; }
        else if (GameStart.lvl == 26) { realBoard = new string[] {"1110000111", 
                                                                  "1111000011", 
                                                                  "1000011001", 
                                                                  "1110001011", 
                                                                  "1111011001"}; }
        else if (GameStart.lvl == 27) { realBoard = new string[] {"1111000000", 
                                                                  "1100011011", 
                                                                  "0000011111", 
                                                                  "0100111111", 
                                                                  "1110111111"}; }
        else if (GameStart.lvl == 28) { realBoard = new string[] {"0000001000", 
                                                                  "0001111001", 
                                                                  "0111111011", 
                                                                  "0111111001", 
                                                                  "1111111011"}; }
        else if (GameStart.lvl == 29) { realBoard = new string[] {"1111111111", 
                                                                  "0011111111", 
                                                                  "0001110001", 
                                                                  "0001110001", 
                                                                  "0001110000"}; }
        else if (GameStart.lvl == 30) { realBoard = new string[] {"1111101111", 
                                                                  "1100100011", 
                                                                  "1000111000", 
                                                                  "1100111010", 
                                                                  "1100011111"}; }
        else if (GameStart.lvl == 49) { realBoard = new string[] {"0000000000", 
                                                                  "1100100010", 
                                                                  "1100100011", 
                                                                  "1000100010", 
                                                                  "1000110000"}; }
        else if (GameStart.lvl == 50) { realBoard = new string[] {"1000001111", 
                                                                  "1000000100", 
                                                                  "1111111110", 
                                                                  "0010110000", 
                                                                  "0000000000"}; }
        else if (GameStart.lvl == 51) { realBoard = new string[] {"1111101111", 
                                                                  "1100000110", 
                                                                  "1101110000", 
                                                                  "0000100000", 
                                                                  "0000000000"}; }
        else if (GameStart.lvl == 52) { realBoard = new string[] {"0000000000", 
                                                                  "1000111100", 
                                                                  "1111111000", 
                                                                  "0000011000", 
                                                                  "0000001000"}; }
        else if (GameStart.lvl == 53) { realBoard = new string[] {"1001110011", 
                                                                  "1000100001", 
                                                                  "1100000001", 
                                                                  "0000011001", 
                                                                  "0000111100"}; }
        else if (GameStart.lvl == 54) { realBoard = new string[] {"1000000011", 
                                                                  "1100000001", 
                                                                  "1000011011", 
                                                                  "1111111111", 
                                                                  "1111111111"}; }
        else if (GameStart.lvl == 73) { realBoard = new string[] {"0000000000", 
                                                                  "0010000000", 
                                                                  "0111000000", 
                                                                  "0010001100", 
                                                                  "0111111110"}; }
        else if (GameStart.lvl == 74) { realBoard = new string[] {"0000000000", 
                                                                  "0001000011", 
                                                                  "0011000001", 
                                                                  "0011000101", 
                                                                  "0000011111"}; }
        else if (GameStart.lvl == 75) { realBoard = new string[] {"0000000001", 
                                                                  "0000100001", 
                                                                  "0000111011", 
                                                                  "0100000011", 
                                                                  "1110000111"}; }
        else if (GameStart.lvl == 76) { realBoard = new string[] {"1000000011", 
                                                                  "1000010001", 
                                                                  "1100011001", 
                                                                  "1111011001", 
                                                                  "1100000000"}; }
        else if (GameStart.lvl == 77) { realBoard = new string[] {"0000000000", 
                                                                  "1000000100", 
                                                                  "1000010110", 
                                                                  "1011110100", 
                                                                  "1111110000"}; }
        else if (GameStart.lvl == 78) { realBoard = new string[] {"1111111111", 
                                                                  "1110101001", 
                                                                  "0100001001", 
                                                                  "0000001001", 
                                                                  "0000000000"}; }
        else if (GameStart.lvl == 97) { realBoard = new string[] {"0000000100", 
                                                                  "0000101110", 
                                                                  "0000100000", 
                                                                  "0000110000", 
                                                                  "0000000000"}; }
        else if (GameStart.lvl == 98) { realBoard = new string[] {"0000010000", 
                                                                  "0000010000", 
                                                                  "0011111000", 
                                                                  "0000110000", 
                                                                  "0000000000"}; }
        else if (GameStart.lvl == 99) { realBoard = new string[] {"0000000000", 
                                                                  "0000000000", 
                                                                  "1000000000", 
                                                                  "1000010011", 
                                                                  "1101111111"}; }
        else if (GameStart.lvl == 100) { realBoard = new string[] {"0000000000", 
                                                                   "1000000000", 
                                                                   "1000000000", 
                                                                   "1110000010", 
                                                                   "1111000111"}; }
        else if (GameStart.lvl == 101) { realBoard = new string[] {"0000100000", 
                                                                   "0001111000", 
                                                                   "0000000000", 
                                                                   "0000001000", 
                                                                   "0001111000"}; }
        else if (GameStart.lvl == 102) { realBoard = new string[] {"0000000000", 
                                                                   "0000100000", 
                                                                   "0011111100", 
                                                                   "0000011000", 
                                                                   "0000000000"}; }

        extendedBoard = ExtendShape(realBoard);
        gameBoard.Board = CreateShape(extendedBoard);
        Debug.Log("Holes filled.");
    }

    public static string[] ExtendShape(string[] input)
    {
        string[] output = new string[9];
        
        // Fill the first and last two rows with zeros
        output[0] = "00000000000000";
        output[1] = "00000000000000";
        output[7] = "00000000000000";
        output[8] = "00000000000000";


        // Fill the middle rows based on the input
        for (int i = 0; i < 5; i++)
        {
            output[i + 2] = "00" + input[i] + "00";
        }

        return output;
    }

}

