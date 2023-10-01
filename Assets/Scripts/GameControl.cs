using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class GameControl : MonoBehaviour
{
    [SerializeField] public GameObject[] BlocksGO;
    public static GameObject[] blocksGO;
    public static GameObject lblueGO, pinkGO, yellowGO, redGO, blueGO, orangeGO, purpleGO, greenGO, lgreenGM, dblueGO;
    [SerializeField] public Transform[] Waypoints;
    public static Transform[] waypoints;
    public static Transform[,] waypointsArr;
    public static bool allBlocksUsed;


    // Start is called before the first frame update
    void Start()
    {
        waypoints = Waypoints;
        blocksGO = BlocksGO;
        lblueGO = BlocksGO[0]; pinkGO = BlocksGO[1]; yellowGO = BlocksGO[2]; redGO = BlocksGO[3]; blueGO = BlocksGO[4]; orangeGO = BlocksGO[5]; purpleGO = BlocksGO[6]; greenGO = BlocksGO[7]; lgreenGM = BlocksGO[8]; dblueGO = BlocksGO[9];
        waypointsArr = new Transform[5, 10];
        InsertWaypoints();

        buttons = Buttons;
        AssignButtons();
        buttonObjects = ButtonObjects;
        AssignButtonObjects();
        Array.ForEach(buttonObjects, b => b.SetActive(false));
        
        allBlocksUsed = false;

        InitializeBlocks();
        LvLSetup();

        gameBoard = new GameBoard();
        FillHoles();

        // Bot
        botButton = BotButton;
        botObj = BotObj;
        screenObj = ScreenObj;
        screenObj.SetActive(false);
        isBoardSolved = false;
        blockUsed = false;
        // Smarter bot
        visited = new bool[9, 14];
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private static void InsertWaypoints()
    {
        int i = 10;
        while (i < 60)
        {
            for (int j = 0; j < 5; j++)
            {
                for (int k = 0; k < 10; k++)
                {
                    waypointsArr[j, k] = waypoints[i++];
                }
            }
        }
    }

    private static void LvLSetup()
    {
        if (GameStart.lvl == 1)
        {
            SetTransform(lblue, waypointsArr[0, 5]);
            SetTransform(yellow, waypointsArr[2, 8], new Vector3(0, 180, -90));
            SetTransform(red, waypointsArr[2, 4], new Vector3(0, 90, -90));
            SetTransform(blue, waypointsArr[4, 2], new Vector3(0, 180, 0));
            SetTransform(purple, waypointsArr[3, 6], new Vector3(0, 0, -90));
            SetTransform(green, waypointsArr[1, 8], new Vector3(0, 180, 0));
            SetTransform(dblue, waypointsArr[2, 1], new Vector3(0, -90, -90));
            SetTransform(lgreen, waypointsArr[0, 1]);

            blocksInGame = new int[] {pink.blockID, orange.blockID};
        }
        /*
        else if (GameStart.lvl == -1000000000)
        {
            SetTransform(lblue, waypointsArr[], new Vector3(0, 0, 0));
            SetTransform(pink, waypointsArr[], new Vector3(0, 0, 0));

            blocksInGame = new int[] {lblue.blockID, pink.blockID, yellow.blockID, red.blockID, blue.blockID, orange.blockID, purple.blockID, green.blockID, lgreen.blockID, dblue.blockID};
        }
        */
        else if (GameStart.lvl == 2)
        {
            SetTransform(lblue, waypointsArr[3, 6], new Vector3(0, 0, -90));
            SetTransform(yellow, waypointsArr[4, 5], new Vector3(0, 0, -90));
            SetTransform(red, waypointsArr[0, 6], new Vector3(0, 180, -90));
            SetTransform(orange, waypointsArr[2, 4], new Vector3(0, 0, -90));
            SetTransform(purple, waypointsArr[0, 2]);
            SetTransform(green, waypointsArr[1, 0], new Vector3(0, -90, 0));
            SetTransform(dblue, waypointsArr[3, 9], new Vector3(0, 90, 0));
            SetTransform(lgreen, waypointsArr[3, 1], new Vector3(0, 0, -90));

            blocksInGame = new int[] {pink.blockID, blue.blockID};
        }
        else if (GameStart.lvl == 3)
        {
            SetTransform(lblue, waypointsArr[4, 1], new Vector3(0, 0, -90));
            SetTransform(pink, waypointsArr[0, 7], new Vector3(0, 180, -90));
            SetTransform(yellow, waypointsArr[2, 7], new Vector3(0, 180, 0));
            SetTransform(orange, waypointsArr[2, 9], new Vector3(0, -90, -90));
            SetTransform(purple, waypointsArr[4, 8], new Vector3(0, 180, 0));
            SetTransform(green, waypointsArr[2, 2], new Vector3(0, 180, 0));
            SetTransform(lgreen, waypointsArr[4, 5], new Vector3(0, 0, -90));
            SetTransform(dblue, waypointsArr[3, 4], new Vector3(0, 0, -90));

            blocksInGame = new int[] {red.blockID, blue.blockID};
        }
        else if (GameStart.lvl == 4)
        {
            SetTransform(lblue, waypointsArr[0, 8], new Vector3(0, 180, -90));
            SetTransform(yellow, waypointsArr[0, 2], new Vector3(0, 180, -90));
            SetTransform(red, waypointsArr[2, 0], new Vector3(0, 90, -90));
            SetTransform(blue, waypointsArr[2, 3]);
            SetTransform(purple, waypointsArr[1, 5], new Vector3(0, 180, 0));
            SetTransform(green, waypointsArr[2, 7], new Vector3(0, 180, -90));
            SetTransform(lgreen, waypointsArr[4, 3], new Vector3(0, 0, -90));
            SetTransform(dblue, waypointsArr[2, 1], new Vector3(0, -90, 0));

            blocksInGame = new int[] {pink.blockID, orange.blockID};
        }
        else if (GameStart.lvl == 5)
        {
            SetTransform(lblue, waypointsArr[3, 7], new Vector3(0, 180, 0));
            SetTransform(pink, waypointsArr[1, 8], new Vector3(0, 180, 0));
            SetTransform(yellow, waypointsArr[4, 5], new Vector3(0, 0, -90));
            SetTransform(red, waypointsArr[0, 2], new Vector3(0, 180, -90));
            SetTransform(blue, waypointsArr[4, 1], new Vector3(0, 0, -90));
            SetTransform(orange, waypointsArr[0, 6], new Vector3(0, 180, -90));
            SetTransform(green, waypointsArr[2, 4], new Vector3(0, 0, -90));
            SetTransform(lgreen, waypointsArr[3, 9], new Vector3(0, 90, 0));

            blocksInGame = new int[] {purple.blockID, dblue.blockID};
        }
        else if (GameStart.lvl == 6)
        {
            SetTransform(lblue, waypointsArr[2, 5], new Vector3(0, 90, -90));
            SetTransform(yellow, waypointsArr[3, 4], new Vector3(0, -90, -90));
            SetTransform(red, waypointsArr[0, 2], new Vector3(0, 180, -90));
            SetTransform(orange, waypointsArr[4, 7], new Vector3(0, 0, -90));
            SetTransform(purple, waypointsArr[4, 1], new Vector3(0, 180, 0));
            SetTransform(green, waypointsArr[1, 8], new Vector3(0, 180, 0));
            SetTransform(lgreen, waypointsArr[2, 3], new Vector3(0, 90, 0));
            SetTransform(dblue, waypointsArr[2, 1], new Vector3(0, 0, -90));

            blocksInGame = new int[] {pink.blockID, blue.blockID};
        }
        else if (GameStart.lvl == 25)
        {
            SetTransform(pink, waypointsArr[4, 7], new Vector3(0, 180, 0));
            SetTransform(yellow, waypointsArr[4, 2], new Vector3(0, 180, 0));
            SetTransform(blue, waypointsArr[3, 9], new Vector3(0, -90, -90));
            SetTransform(orange, waypointsArr[3, 5], new Vector3(0, 180, -90));
            SetTransform(green, waypointsArr[1, 0], new Vector3(0, -90, 0));
            SetTransform(lgreen, waypointsArr[1, 5], new Vector3(0, 90, 0));

            blocksInGame = new int[] {lblue.blockID, red.blockID, purple.blockID, dblue.blockID};
        }
        else if (GameStart.lvl == 26)
        {
            SetTransform(lblue, waypointsArr[4, 1], new Vector3(0, 0, -90));
            SetTransform(pink, waypointsArr[3, 9], new Vector3(0, -90, -90));
            SetTransform(red, waypointsArr[1, 0], new Vector3(0, 90, -90));
            SetTransform(purple, waypointsArr[1, 2], new Vector3(0, 180, 0));
            SetTransform(green, waypointsArr[0, 8], new Vector3(0, 180, -90));
            SetTransform(lgreen, waypointsArr[3, 6], new Vector3(0, 90, 0));

            blocksInGame = new int[] {yellow.blockID, blue.blockID, orange.blockID, dblue.blockID};
        }
        else if (GameStart.lvl == 27)
        {
            SetTransform(pink, waypointsArr[0, 1]);
            SetTransform(yellow, waypointsArr[2, 5], new Vector3(0, 90, 0));
            SetTransform(blue, waypointsArr[3, 9], new Vector3(0, -90, -90));
            SetTransform(orange, waypointsArr[2, 6], new Vector3(0, 90, -90));
            SetTransform(green, waypointsArr[4, 1], new Vector3(0, 0, -90));
            SetTransform(dblue, waypointsArr[3, 8], new Vector3(0, 90, 0));

            blocksInGame = new int[] {lblue.blockID, red.blockID, purple.blockID, lgreen.blockID};
        }
        else if (GameStart.lvl == 28)
        {
            SetTransform(pink, waypointsArr[2, 3], new Vector3(0, 180, 0));
            SetTransform(blue, waypointsArr[2, 9], new Vector3(0, 90, 0));
            SetTransform(orange, waypointsArr[3, 4], new Vector3(0, 180, -90));
            SetTransform(purple, waypointsArr[1, 6], new Vector3(0, 90, 0));
            SetTransform(lgreen, waypointsArr[4, 5], new Vector3(0, 0, -90));
            SetTransform(dblue, waypointsArr[4, 1], new Vector3(0, 0, -90));

            blocksInGame = new int[] {lblue.blockID, yellow.blockID, red.blockID, green.blockID};
        }
        else if (GameStart.lvl == 29)
        {
            SetTransform(pink, waypointsArr[2, 3], new Vector3(0, 90, -90));
            SetTransform(yellow, waypointsArr[2, 5], new Vector3(0, 90, 0));
            SetTransform(red, waypointsArr[2, 9], new Vector3(0, -90, -90));
            SetTransform(orange, waypointsArr[0, 5], new Vector3(0, 180, -90));
            SetTransform(purple, waypointsArr[0, 1], new Vector3(0, 180, -90));
            SetTransform(dblue, waypointsArr[1, 7], new Vector3(0, 0, -90));

            blocksInGame = new int[] {lblue.blockID, blue.blockID, green.blockID, lgreen.blockID};
        }
        else if (GameStart.lvl == 30)
        {
            SetTransform(lblue, waypointsArr[0, 2], new Vector3(0, 180, -90));
            SetTransform(pink, waypointsArr[3, 0], new Vector3(0, -90, 0));
            SetTransform(yellow, waypointsArr[0, 7]);
            SetTransform(blue, waypointsArr[1, 4], new Vector3(0, 90, -90));
            SetTransform(green, waypointsArr[4, 8], new Vector3(0, 0, -90));
            SetTransform(dblue, waypointsArr[3, 6], new Vector3(0, 90, 0));

            blocksInGame = new int[] {red.blockID, orange.blockID, purple.blockID, lgreen.blockID};
        }
        else if (GameStart.lvl == 49)
        {
            SetTransform(yellow, waypointsArr[3, 0], new Vector3(0, -90, 0));
            SetTransform(blue, waypointsArr[2, 4], new Vector3(0, 90, -90));
            SetTransform(green, waypointsArr[2, 8], new Vector3(0, 90, -90));

            blocksInGame = new int[] {lblue.blockID, pink.blockID, red.blockID, orange.blockID, purple.blockID, lgreen.blockID, dblue.blockID};
        }
        else if (GameStart.lvl == 50)
        {
            SetTransform(blue, waypointsArr[2, 7], new Vector3(0, 180, -90));
            SetTransform(orange, waypointsArr[0, 8], new Vector3(0, 180, -90));
            SetTransform(lgreen, waypointsArr[1, 0], new Vector3(0, 90, -90));
            SetTransform(dblue, waypointsArr[2, 3]);

            blocksInGame = new int[] {lblue.blockID, pink.blockID, yellow.blockID, red.blockID, purple.blockID, green.blockID};
        }
        else if (GameStart.lvl == 51)
        {
            SetTransform(lblue, waypointsArr[0, 7]);
            SetTransform(blue, waypointsArr[0, 3], new Vector3(0, 180, -90));
            SetTransform(lgreen, waypointsArr[1, 0], new Vector3(0, 90, -90));
            SetTransform(dblue, waypointsArr[2, 4], new Vector3(0, 180, -90));

            blocksInGame = new int[] {pink.blockID, yellow.blockID, red.blockID, orange.blockID, purple.blockID, green.blockID};
        }
        else if (GameStart.lvl == 52)
        {
            SetTransform(yellow, waypointsArr[2, 1], new Vector3(0, 0, -90));
            SetTransform(blue, waypointsArr[1, 6], new Vector3(0, 180, -90));
            SetTransform(green, waypointsArr[3, 6], new Vector3(0, 90, 0));

            blocksInGame = new int[] {lblue.blockID, pink.blockID, red.blockID, orange.blockID, purple.blockID, lgreen.blockID, dblue.blockID};
        }
        else if (GameStart.lvl == 53)
        {
            SetTransform(lblue, waypointsArr[4, 6], new Vector3(0, 180, 0));
            SetTransform(red, waypointsArr[2, 9], new Vector3(0, -90, -90));
            SetTransform(lgreen, waypointsArr[1, 0], new Vector3(0, 90, -90));
            SetTransform(dblue, waypointsArr[0, 4], new Vector3(0, 180, -90));

            blocksInGame = new int[] {pink.blockID, yellow.blockID, blue.blockID, orange.blockID, purple.blockID, green.blockID};
        }
        else if (GameStart.lvl == 54)
        {
            SetTransform(pink, waypointsArr[3, 1]);
            SetTransform(yellow, waypointsArr[3, 7], new Vector3(0, 180, 0));
            SetTransform(red, waypointsArr[4, 7], new Vector3(0, 0, -90));
            SetTransform(orange, waypointsArr[4, 3], new Vector3(0, 0, -90));
            SetTransform(green, waypointsArr[1, 0], new Vector3(0, 90, -90));
            SetTransform(lgreen, waypointsArr[1, 9], new Vector3(0, 90, 0));

            blocksInGame = new int[] {lblue.blockID, blue.blockID, purple.blockID, dblue.blockID};
        }
        else if (GameStart.lvl == 73)
        {
            SetTransform(lblue, waypointsArr[4, 7], new Vector3(0, 180, 0));
            SetTransform(pink, waypointsArr[4, 2], new Vector3(0, 0, -90));
            SetTransform(dblue, waypointsArr[2, 2], new Vector3(0, 0, -90));

            blocksInGame = new int[] {yellow.blockID, red.blockID, blue.blockID, orange.blockID, purple.blockID, green.blockID, lgreen.blockID};
        }
        else if (GameStart.lvl == 74)
        {
            SetTransform(lblue, waypointsArr[4, 6], new Vector3(0, 0, -90));
            SetTransform(blue, waypointsArr[3, 9], new Vector3(0, -90, -90));
            SetTransform(purple, waypointsArr[2, 3], new Vector3(0, 90, 0));

            blocksInGame = new int[] {pink.blockID, yellow.blockID, red.blockID, orange.blockID, green.blockID, lgreen.blockID, dblue.blockID};
        }
        else if (GameStart.lvl == 75)
        {
            SetTransform(pink, waypointsArr[2, 9], new Vector3(0, -90, -90));
            SetTransform(purple, waypointsArr[2, 5], new Vector3(0, 0, -90));
            SetTransform(green, waypointsArr[4, 8], new Vector3(0, 0, -90));
            SetTransform(dblue, waypointsArr[4, 1], new Vector3(0, 0, -90));

            blocksInGame = new int[] {lblue.blockID, yellow.blockID, red.blockID, blue.blockID, orange.blockID, lgreen.blockID};
        }
        else if (GameStart.lvl == 76)
        {
            SetTransform(pink, waypointsArr[3, 1]);
            SetTransform(blue, waypointsArr[2, 9], new Vector3(0, -90, -90));
            SetTransform(green, waypointsArr[2, 5], new Vector3(0, -90, 0));
            SetTransform(lgreen, waypointsArr[1, 0], new Vector3(0, 90, -90));

            blocksInGame = new int[] {lblue.blockID, yellow.blockID, red.blockID, orange.blockID, purple.blockID, dblue.blockID};
        }
        else if (GameStart.lvl == 77)
        {
            SetTransform(blue, waypointsArr[2, 0], new Vector3(0, 90, -90));
            SetTransform(purple, waypointsArr[3, 5], new Vector3(0, -90, -90));
            SetTransform(green, waypointsArr[3, 3]);
            SetTransform(dblue, waypointsArr[2, 7], new Vector3(0, 90, -90));

            blocksInGame = new int[] {lblue.blockID, pink.blockID, yellow.blockID, red.blockID, orange.blockID, lgreen.blockID};
        }
        else if (GameStart.lvl == 78)
        {
            SetTransform(yellow, waypointsArr[1, 6], new Vector3(0, 90, -90));
            SetTransform(red, waypointsArr[2, 9], new Vector3(0, -90, -90));
            SetTransform(orange, waypointsArr[0, 3]);
            SetTransform(green, waypointsArr[1, 1], new Vector3(0, 90, 0));

            blocksInGame = new int[] {lblue.blockID, pink.blockID, blue.blockID, purple.blockID, lgreen.blockID, dblue.blockID};
        }
        else if (GameStart.lvl == 97)
        {
            SetTransform(green, waypointsArr[1, 7], new Vector3(0, 0, -90));
            SetTransform(lgreen, waypointsArr[2, 4], new Vector3(0, 90, -90));

            blocksInGame = new int[] {lblue.blockID, pink.blockID, yellow.blockID, red.blockID, blue.blockID, orange.blockID, purple.blockID, dblue.blockID};
        }
        else if (GameStart.lvl == 98)
        {
            SetTransform(lblue, waypointsArr[1, 5], new Vector3(0, 90, -90));
            SetTransform(purple, waypointsArr[2, 3], new Vector3(0, 180, -90));

            blocksInGame = new int[] {pink.blockID, yellow.blockID, red.blockID, blue.blockID, orange.blockID, green.blockID, lgreen.blockID, dblue.blockID};
        }
        else if (GameStart.lvl == 99)
        {
            SetTransform(lblue, waypointsArr[4, 4], new Vector3(0, 0, -90));
            SetTransform(green, waypointsArr[4, 8], new Vector3(0, 180, 0));
            SetTransform(lgreen, waypointsArr[3, 0], new Vector3(0, 90, -90));

            blocksInGame = new int[] {pink.blockID, yellow.blockID, red.blockID, blue.blockID, orange.blockID, purple.blockID, dblue.blockID};
        }
        else if (GameStart.lvl == 100)
        {
            SetTransform(orange, waypointsArr[2, 0], new Vector3(0, 90, -90));
            SetTransform(green, waypointsArr[4, 2], new Vector3(0, 0, -90));
            SetTransform(dblue, waypointsArr[4, 8], new Vector3(0, 0, -90));

            blocksInGame = new int[] {lblue.blockID, pink.blockID, yellow.blockID, red.blockID, blue.blockID, purple.blockID, lgreen.blockID};
        }
        else if (GameStart.lvl == 101)
        {
            SetTransform(pink, waypointsArr[1, 4], new Vector3(0, 0, -90));
            SetTransform(blue, waypointsArr[4, 4], new Vector3(0, 0, -90));

            blocksInGame = new int[] {lblue.blockID, yellow.blockID, red.blockID, orange.blockID, purple.blockID, green.blockID, lgreen.blockID, dblue.blockID};
        }
        else if (GameStart.lvl == 102)
        {
            SetTransform(green, waypointsArr[2, 6]);
            SetTransform(lgreen, waypointsArr[2, 3], new Vector3(0, 0, -90));

            blocksInGame = new int[] {lblue.blockID, pink.blockID, yellow.blockID, red.blockID, blue.blockID, orange.blockID, purple.blockID, dblue.blockID};
        }
        
        // Activate pick buttons (for every block in game)
        foreach (int i in blocksInGame)
        {
            pickObj[i].SetActive(true);
        }

        Debug.Log("Level set up.");
    }
    private static void SetTransform(Block b, Transform waypoint, Vector3? eulerAngles = null, float scale=1f)
    {
        b.go.transform.position = waypoint.position;
        b.go.transform.rotation = eulerAngles.HasValue ? Quaternion.Euler(eulerAngles.Value) : waypoint.rotation;
        b.go.transform.localScale = Vector3.one * scale;
    }



}
