using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public partial class GameControl
{
    [SerializeField] public Button[] Buttons;
    public static Button[] buttons, MoveButtons, PickButtons, RemoveButtons, DiscardButtons; public static Button useButton, lvlCompletedButton;
    [SerializeField] public GameObject[] ButtonObjects;
    public static GameObject[] buttonObjects, moveObj, pickObj, removeObj, discardObj; public static GameObject useObj, lvlCompletedObj;
    //public static bool setCurrentCompletd;



    public static void MoveUp()
    {
        Block cb = blocks[currentBlock];
        cb.go.transform.position += Vector3.left * 1.25f;
        gameBoard.PieceX = --cb.coorX;
        gameBoard.UpdateTempBoard();
        moveObj[1].SetActive(true);
        moveObj[0].SetActive(gameBoard.CanMove(-1, 0));
        useObj.SetActive(gameBoard.CanInsert());

        Debug.Log("X, Y = (" + gameBoard.PieceX + ", " + gameBoard.PieceY + ") (" + cb.coorX + ", " + cb.coorY + ")");
    }
    public static void MoveDown()
    {
        Block cb = blocks[currentBlock];
        cb.go.transform.position += Vector3.right * 1.25f;
        gameBoard.PieceX = ++cb.coorX;
        gameBoard.UpdateTempBoard();
        moveObj[0].SetActive(true);
        moveObj[1].SetActive(gameBoard.CanMove(1, 0));
        useObj.SetActive(gameBoard.CanInsert());

        Debug.Log("X, Y = (" + gameBoard.PieceX + ", " + gameBoard.PieceY + ") (" + cb.coorX + ", " + cb.coorY + ")");
    }
    public static void MoveLeft()
    {
        Block cb = blocks[currentBlock];
        cb.go.transform.position += Vector3.back * 1.25f;
        gameBoard.PieceY = --cb.coorY;
        gameBoard.UpdateTempBoard();
        moveObj[3].SetActive(true);
        moveObj[2].SetActive(gameBoard.CanMove(0, -1));
        useObj.SetActive(gameBoard.CanInsert());

        Debug.Log("X, Y = (" + gameBoard.PieceX + ", " + gameBoard.PieceY + ") (" + cb.coorX + ", " + cb.coorY + ")");
    }
    public static void MoveRight()
    {
        Block cb = blocks[currentBlock];
        cb.go.transform.position += Vector3.forward * 1.25f;
        gameBoard.PieceY = ++cb.coorY;
        gameBoard.UpdateTempBoard();
        moveObj[2].SetActive(true);
        moveObj[3].SetActive(gameBoard.CanMove(0, 1));
        useObj.SetActive(gameBoard.CanInsert());

        Debug.Log("X, Y = (" + gameBoard.PieceX + ", " + gameBoard.PieceY + ") (" + cb.coorX + ", " + cb.coorY + ")");
    }
    public static void Rotate()
    {
        Block cb = blocks[currentBlock];
        // Rotate
        if (cb.posID < 4)
        {
            cb.go.transform.rotation *= Quaternion.Euler(0, 90, 0);
        }
        else
        {
            cb.go.transform.rotation *= Quaternion.Euler(0, 0, 90);
            cb.go.transform.rotation *= Quaternion.Euler(0, 90, 0);
            cb.go.transform.rotation *= Quaternion.Euler(0, 0, -90);
        }
        
        // Uptade posID
        if (cb.posID == 3) { cb.posID = 0; }
        else if (cb.posID == 7) { cb.posID = 4; }
        else { cb.posID++; }

        // Update gameBoard
        gameBoard.CurrentPiece = cb.positions[cb.posID];
        gameBoard.UpdateTempBoard();
        gameBoard.AdjustRotation();

        // Enable/disable buttons
        useObj.SetActive(gameBoard.CanInsert());
        CanMoveCheckAllDirections();

        Debug.Log("X, Y = (" + gameBoard.PieceX + ", " + gameBoard.PieceY + ") (" + cb.coorX + ", " + cb.coorY + ")");
    }
    public static void Flip()
    {
        // Prepare variables
        Block cb = blocks[currentBlock];
        Vector3[] directions = { Vector3.right, Vector3.back, Vector3.left, Vector3.forward, Vector3.left, Vector3.forward, Vector3.right, Vector3.back };
        Vector3 moveDirection = directions[cb.posID] * 1.25f;
        Quaternion flipRotation = Quaternion.Euler(0, 0, cb.posID < 4 ? -90 : 90);

        // (Move and) Flip
        cb.go.transform.position += moveDirection;
        cb.go.transform.rotation *= flipRotation;

        // Update coordinates
        if      (cb.posID == 0 || cb.posID == 6) gameBoard.PieceX = ++cb.coorX;
        else if (cb.posID == 2 || cb.posID == 4) gameBoard.PieceX = --cb.coorX;
        else if (cb.posID == 3 || cb.posID == 5) gameBoard.PieceY = ++cb.coorY;
        else                                     gameBoard.PieceY = --cb.coorY;


        // Update posID
        cb.posID = (cb.posID < 4) ? cb.posID + 4 : cb.posID - 4;

        // Update gameBoard
        gameBoard.CurrentPiece = cb.positions[cb.posID];
        gameBoard.UpdateTempBoard();

        // Enable/disable buttons
        useObj.SetActive(gameBoard.CanInsert());
        CanMoveCheckAllDirections();

        Debug.Log("X, Y = (" + gameBoard.PieceX + ", " + gameBoard.PieceY + ") (" + cb.coorX + ", " + cb.coorY + ")");
    }

    public static void UseBlock()
    {
        Block cb = blocks[currentBlock];
        cb.go.transform.position += Vector3.down * 2.3f;
        cb.used = true;
        
        // Disable buttons
        foreach(GameObject g in moveObj) { g.SetActive(false); }
        useObj.SetActive(false);
        discardObj[currentBlock].SetActive(false);

        // Insert piece
        gameBoard.InsertPiece();

        // Check if level completed
        allBlocksUsed = true;
        foreach (int i in blocksInGame)
        {
            if (!blocks[i].used)
            {
                Debug.Log("All blocks used.");
                allBlocksUsed = false;
                break;
            }
        }
        if (allBlocksUsed && gameBoard.IsLevelCompleted())
        {
            Debug.Log("Board solved.");
            isBoardSolved = true; // To deactivate the bot
            lvlCompletedObj.SetActive(true);
            return;
        }
        // If level not completed enable remove and pick buttons
        foreach (int i in blocksInGame)
        {
            if (blocks[i].used) { removeObj[i].SetActive(true); }
            else { pickObj[i].SetActive(true); }
        }
    }

    public static void Pick()
    {
        Block cb = blocks[currentBlock];
        // Pick
        cb.go.transform.position = waypointsArr[0, 1].position + Vector3.up * 2.3f;
        cb.go.transform.rotation = waypointsArr[0, 0].rotation;
        cb.go.transform.localScale = Vector3.one;
        cb.posID = 0;

        // Asign coordinates
        if (cb.blockID < 6)
        {
            cb.coorX = 0;
            cb.coorY = 1;
            gameBoard.PieceX = 0;
            gameBoard.PieceY = 1;
        }
        else
        {
            cb.coorX = 1;
            cb.coorY = 2;
            gameBoard.PieceX = 1;
            gameBoard.PieceY = 2;
        }

        // Update gameBoard
        gameBoard.CurrentPiece = cb.positions[0];
        gameBoard.UpdateTempBoard();

        // Enable/disable buttons
        foreach (int i in blocksInGame)
        {
            pickObj[i].SetActive(false);
            removeObj[i].SetActive(false);
        }
        discardObj[currentBlock].SetActive(true);
        moveObj[1].SetActive(true);
        moveObj[3].SetActive(true);
        moveObj[4].SetActive(true);
        moveObj[5].SetActive(true);
        useObj.SetActive(gameBoard.CanInsert());
    }
    public static void Remove()
    {
        Debug.Log("Remove");
        Debug.Log("Current block: " + currentBlock);
        Block cb = blocks[currentBlock];
        // Remove
        cb.go.transform.position += Vector3.up * 2.3f;
        cb.used = false;

        // Update gameBoard
        gameBoard.CurrentPiece = cb.positions[cb.posID];
        gameBoard.PieceX = cb.coorX;
        gameBoard.PieceY = cb.coorY;
        gameBoard.UpdateTempBoard();
        gameBoard.RemovePiece();
        
        // Enable/disable buttons
        foreach (int i in blocksInGame)
        {
            pickObj[i].SetActive(false);
            removeObj[i].SetActive(false);
        }
        useObj.SetActive(true);
        CanMoveCheckAllDirections();
        moveObj[4].SetActive(true);
        moveObj[5].SetActive(true);
        discardObj[currentBlock].SetActive(true);
    }
    public static void Discard()
    {
        Block cb = blocks[currentBlock];
        // Discard
        cb.go.transform.position = waypoints[currentBlock].position;
        cb.go.transform.rotation = waypoints[currentBlock].rotation;
        cb.go.transform.localScale = Vector3.one * 0.4f;

        // Enable/disable buttons
        discardObj[currentBlock].SetActive(false);
        foreach (int i in blocksInGame)
        {
            if (blocks[i].used) { removeObj[i].SetActive(true); }
            else { pickObj[i].SetActive(true); }
        }
        foreach(GameObject g in moveObj) { g.SetActive(false); }
        useObj.SetActive(false);
    }


    private static void AssignButtons()
    {        
        MoveButtons = new Button[6]; Array.Copy(buttons, 0, MoveButtons, 0, 6);
        useButton = buttons[6];
        PickButtons = new Button[10]; Array.Copy(buttons, 7, PickButtons, 0, 10);
        RemoveButtons = new Button[10]; Array.Copy(buttons, 17, RemoveButtons, 0, 10);
        DiscardButtons = new Button[10]; Array.Copy(buttons, 27, DiscardButtons, 0, 10);
        lvlCompletedButton = buttons[37];
    }
    private static void AssignButtonObjects()
    {
        moveObj = new GameObject[6]; Array.Copy(buttonObjects, 0, moveObj, 0, 6);
        useObj = buttonObjects[6];
        pickObj = new GameObject[10]; Array.Copy(buttonObjects, 7, pickObj, 0, 10);
        removeObj = new GameObject[10]; Array.Copy(buttonObjects, 17, removeObj, 0, 10);
        discardObj = new GameObject[10]; Array.Copy(buttonObjects, 27, discardObj, 0, 10);
        lvlCompletedObj = buttonObjects[37];
    }



    public static void LvlCompleted()
    {
        SceneManager.LoadScene(0);
    }

    // Change currentBlock
    public static void LBlueSetCurrnt()
    {
        currentBlock = 0;
        Debug.Log("LBlueSetCurrnt");
    }
    public static void PinkSetCurrent()
    {
        currentBlock = 1;
    }
    public static void YellowSetCurrent()
    {
        currentBlock = 2;
    }
    public static void RedSetCurrent()
    {
        currentBlock = 3;
    }
    public static void BlueSetCurrent()
    {
        currentBlock = 4;
    }
    public static void OrangeSetCurrent()
    {
        currentBlock = 5;
    }
    public static void PurpleSetCurrent()
    {
        currentBlock = 6;
    }
    public static void GreenSetCurrent()
    {
        currentBlock = 7;
    }
    public static void LGreenSetCurrent()
    {
        currentBlock = 8;
    }
    public static void DBlueSetCurrent()
    {
        currentBlock = 9;
    }

    
    public static void CanMoveCheckAllDirections()
    {        
        moveObj[0].SetActive(gameBoard.CanMove(-1, 0));
        moveObj[1].SetActive(gameBoard.CanMove(1, 0));
        moveObj[2].SetActive(gameBoard.CanMove(0, -1));
        moveObj[3].SetActive(gameBoard.CanMove(0, 1));
    }


}
