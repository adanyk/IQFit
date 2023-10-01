using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public partial class GameControl
{
    [SerializeField] public Button BotButton;
    [SerializeField] public GameObject BotObj;
    [SerializeField] public GameObject ScreenObj;
    public static Button botButton;
    public static GameObject botObj, screenObj;
    public static bool isBoardSolved, blockUsed;
    public static float tUse;

    // Smarter bot
    public static bool[,] visited;



    public void ActivateBot()
    {
        Debug.Log("Activate bot");
        StartCoroutine(Bot());
    }
    public IEnumerator Bot()
    {
        Debug.Log("Bot activated");
        // Screen the buttons against user interference
        screenObj.SetActive(true);
        botObj.SetActive(false);

        // Reset level
        Array.ForEach(buttonObjects, b => b.SetActive(false));
        LvLSetup();
        gameBoard = new GameBoard();
        FillHoles();

        // Start solving
        tUse = 0.5f/100000;
        yield return Solve();

        // Remove the screen
        screenObj.SetActive(false);
    }

    public IEnumerator Solve()
    {
        while (!isBoardSolved)
        {
            // Set current block
            foreach (int i in blocksInGame)
            {
                if (pickObj[i].activeSelf)
                {
                    currentBlock = i;
                    break;
                }
            }

            // Pick block
            Pick();
            yield return new WaitForSeconds(tUse/10); // Pause for visualizing
            int pos = 0;
            blockUsed = false;

            // Until a block is inserted, keep trying
            while (!blockUsed)
            {
                // Move over the board trying to insert the piece
                while (moveObj[1].activeSelf || moveObj[3].activeSelf || useObj.activeSelf)
                {
                    // If possible, insert the piece
                    if (useObj.activeSelf)
                    {
                        yield return new WaitForSeconds(tUse);
                        UseBlock();
                        yield return new WaitForSeconds(tUse/2);

                        // If the last piece is used, but level not completed or an impossible shape detected, remove current piece
                        if ((allBlocksUsed && !isBoardSolved) || ContainsImpossibleShapes())
                        {
                            Remove();
                            useObj.SetActive(false); // Unable inserting the same way
                        }
                        else
                        {
                            blockUsed = true;
                            break;
                        }
                    }

                    // If possible, move right
                    if (moveObj[3].activeSelf)
                    {
                        MoveRight();
                    }
                    // otherwise, if possible, return to the left edge and move down
                    else if (moveObj[1].activeSelf)
                    {
                        while (moveObj[2].activeSelf) MoveLeft();
                        MoveDown();
                    }
                }

                // If the piece got inserted, omit the rest
                if (!blockUsed)
                {
                    // If there are still untried positions left, try next one
                    if (pos < 7)
                    {
                        // Set position
                        Rotate();
                        if (++pos == 4) Flip();

                        // Set piece in the upper left corner
                        while (moveObj[0].activeSelf) MoveUp();
                        while (moveObj[2].activeSelf) MoveLeft();
                    }
                    // Otherwise, discard the block if no position is valid, then continue with the previous one
                    else
                    {
                        Discard();

                        // Reset current block
                        for (int j = blocksInGame.Length - 1; j >= 0; j--)
                        {
                            int k = blocksInGame[j];
                            if (removeObj[k].activeSelf)
                            {
                                currentBlock = k;
                                break;
                            }
                        }
                        pos = blocks[currentBlock].posID;
                        Remove();
                        yield return new WaitForSeconds(tUse);

                        // Unable inserting the same way
                        useObj.SetActive(false);
                    }
                }
            }
        }
        yield break;
    }


    public static bool ContainsImpossibleShapes() 
    {
        // Reset visited matrix
        Array.Clear(visited, 0, visited.Length);

        for (int i = 2; i < 7; i++) 
        {
            for (int j = 2; j < 12; j++) 
            {
                if (!gameBoard.Board[i, j] && !visited[i, j]) 
                {
                    // If BFS returns a region size less than 4, we found an impossible shape
                    if (BFS(i, j)) 
                    {
                        return true; // Impossible shape found
                    }
                }
            }
        }

        return false;  // No impossible shapes found
    }

    public static bool BFS(int x, int y) 
    {
        int[] dx = {1, -1, 0, 0}; // These arrays represent the
        int[] dy = {0, 0, 1, -1}; // possible directions to move: down, up, right, left.
        int count = 0; // To keep track of the number of empty holes in the connected region.
		int minX = x, maxX = x, minY = y, maxY = y; // Coordinates of the rectangular embedding the searched region

        var queue = new Queue<Tuple<int, int>>(); // Create a queue to store nodes to explore.
        queue.Enqueue(new Tuple<int, int>(x, y)); // Start with the initial point.
        visited[x, y] = true; // Mark the initial point as visited.

        while (queue.Count > 0) // While there are nodes left to explore.
        {
            var current = queue.Dequeue(); // Take the node from the front of the queue.
            count++; // Increment count because we're visiting a new empty hole.

			// Update the bounding box coordinates
			minX = Math.Min(minX, current.Item1);
			maxX = Math.Max(maxX, current.Item1);
			minY = Math.Min(minY, current.Item2);
			maxY = Math.Max(maxY, current.Item2);

            // Explore the neighbors of the current node.
            for (int i = 0; i < 4; i++) 
            {
                int nx = current.Item1 + dx[i]; // Calculate the x-coordinate of the neighbor.
                int ny = current.Item2 + dy[i]; // Calculate the y-coordinate of the neighbor.

                // Check if the neighbor is within the bounds of the inner game board, is an empty hole, and hasn't been visited.
                if (nx >= 2 && nx < 7 && ny >= 2 && ny < 12 && !gameBoard.Board[nx, ny] && !visited[nx, ny]) 
                {
                    queue.Enqueue(new Tuple<int, int>(nx, ny)); // Add the neighbor to the end of the queue.
                    visited[nx, ny] = true; // Mark the neighbor as visited.
                }
            }
        }

		if (count == (maxX - minX + 1) * (maxY - minY + 1)) return true; // Rectangle found
		if (count < 4) return true; // Region of size less than 4 found

		return false;  // No impossible shapes found
    }


}
