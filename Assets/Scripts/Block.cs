using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Block
{
    public GameObject go;
    public Transform initWp;
    public Button pickB, removeB, discardB;
    public int posID, coorX, coorY;
    public Dictionary<int, bool[,]> positions; // 5x5(4x4) or 3x3; 0=false, 1-8=positions
    public bool used;
    public int blockID;


    public Block(GameObject go, Transform initWp, Button pickB, Button removeB, Button discardB, int posID, int coorX, int coorY, Dictionary<int, bool[,]> positions, bool used, int blockID)
    {
        this.go = go;
        this.initWp = initWp;
        this.pickB = pickB;
        this.removeB = removeB;
        this.discardB = discardB;
        this.posID = posID;
        this.coorX = coorX;
        this.coorY = coorY;
        this.positions = positions;
        this.used = used;
        this.blockID = blockID;
    }
}

public partial class GameControl
{
    public static Block[] blocks;
    public static int currentBlock;
    public static Block lblue, pink, yellow, red, blue, orange, purple, green, lgreen, dblue;
    public static Dictionary<int, bool[,]> lbluePos, pinkPos, yellowPos, redPos, bluePos, orangePos, purplePos, greenPos, lgreenPos, dbluePos;
    public static int[] blocksInGame;

    public static void InitializeBlocks()
    {
        // Define an array of block data.
        var blockData = new []
        {
            new {Color = "lblue", Shape1 = CreateShape("00000", "00000", "01111", "00110", "00000"), Shape2 = CreateShape("00000", "00010", "01111", "00000", "00000")},
            new {Color = "pink", Shape1 = CreateShape("00000", "00000", "01111", "01100", "00000"), Shape2 = CreateShape("00000", "00100", "01111", "00000", "00000")},
            new {Color = "yellow", Shape1 = CreateShape("00000", "00000", "01111", "00011", "00000"), Shape2 = CreateShape("00000", "01000", "01111", "00000", "00000")},
            new {Color = "red", Shape1 = CreateShape("00000", "00000", "01111", "01001", "00000"), Shape2 = CreateShape("00000", "00001", "01111", "00000", "00000")},
            new {Color = "blue", Shape1 = CreateShape("00000", "00000", "01111", "00101", "00000"), Shape2 = CreateShape("00000", "00001", "01111", "00000", "00000")},
            new {Color = "orange", Shape1 = CreateShape("00000", "00000", "01111", "01010", "00000"), Shape2 = CreateShape("00000", "00010", "01111", "00000", "00000")},
            new {Color = "purple", Shape1 = CreateShape("000", "111", "011"), Shape2 = CreateShape("100", "111", "000")},
            new {Color = "green", Shape1 = CreateShape("000", "111", "110"), Shape2 = CreateShape("010", "111", "000")},
            new {Color = "lgreen", Shape1 = CreateShape("000", "111", "101"), Shape2 = CreateShape("001", "111", "000")},
            new {Color = "dblue", Shape1 = CreateShape("000", "111", "101"), Shape2 = CreateShape("010", "111", "000")}
        };

        // Create and populate blocks array
        blocks = new Block[10];
        for (int index = 0; index < 10; index++)
        {
            var data = blockData[index];
            var rotations1 = GenerateRotations(0, data.Shape1);
            var rotations2 = GenerateRotations(4, data.Shape2);
            var allRotations = rotations1.Concat(rotations2).ToDictionary(x => x.Key, x => x.Value);
            
            blocks[index] = new Block(
                blocksGO[index],
                waypoints[index],
                PickButtons[index],
                RemoveButtons[index],
                DiscardButtons[index],
                -1, -1, -1,
                allRotations,
                false,
                index
            );
        }
        lblue = blocks[0]; pink = blocks[1]; yellow = blocks[2]; red = blocks[3]; blue = blocks[4]; orange = blocks[5]; purple = blocks[6]; green = blocks[7]; lgreen = blocks[8]; dblue = blocks[9];

    }

    private static Dictionary<int, bool[,]> GenerateRotations(int start, bool[,] baseShape)
    {
        var rotations = new Dictionary<int, bool[,]> { {start, baseShape} };
        for (int i = start + 1; i <= start + 3; i++) { rotations[i] = Rotate90Clockwise(rotations[i - 1]); }        
        return rotations;
    }


    private static bool[,] CreateShape(params string[] lines)
    {
        int height = lines.Length;
        int width = lines[0].Length;
        bool[,] shape = new bool[height, width];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                shape[y, x] = lines[y][x] == '1';
            }
        }

        return shape;
    }

    public static bool[,] Rotate90Clockwise(bool[,] arr)
    {
        int N = arr.GetLength(0);
        bool[,] rotated = new bool[N, N];

        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                rotated[j, N - 1 - i] = arr[i, j];
            }
        }
        return rotated;
    }
    public static bool[,] Rotate90CounterClockwise(bool[,] arr)
    {
        int N = arr.GetLength(0);
        bool[,] rotated = new bool[N, N];

        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                rotated[N - 1 - j, i] = arr[i, j];
            }
        }
        return rotated;
    }
    public static bool[,] Rotate180(bool[,] arr)
    {
        int N = arr.GetLength(0);
        bool[,] rotated = new bool[N, N];

        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                rotated[N - 1 - i, N - 1 - j] = arr[i, j];
            }
        }

        return rotated;
    }


}
