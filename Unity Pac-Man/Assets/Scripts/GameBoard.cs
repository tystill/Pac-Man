using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{

    public enum TileContents { Empty, Pellet, PowerUp, InvisibleNode };
    [SerializeField] private TileContents[,] board;
    private int rows = 29;
    private int cols = 26;
    public GameObject PelletPrefab;
    public GameObject PowerUpPrefab;
    public GameObject InvisibleNodePrefab;
    public static GameBoard instance;
    public GameObject[,] GameObjects;
    public int PacRow;
    public int PacCol;
    public GameObject BlinkyPrefab;
    public GameObject PinkyPrefab;
    public GameObject InkyPrefab;
    public GameObject ClydePrefab;


    private void Start()
    {
        instance = this;
        GameObjects = new GameObject[rows, cols];
        InitializeBoard();
        InstantiatePrefabs();
    }

    public void InitializeBoard()
    {
        board = new TileContents[rows, cols];
        for (int j = 0; j < cols; j++)
        {
            for (int i = 0; i < rows; i++)
            {
                switch (i)
                {

                    case 0:
                    case 19:
                        if (j != 12 && j != 13)
                        {
                            board[i, j] = TileContents.Pellet;
                        }
                        break;
                    case 1:
                    case 2:
                    case 3:
                    case 20:
                    case 21:
                        if (j == 0 || j == 5 || j == 11 || j == 14 || j == 20 || j == 25)
                        {
                            board[i, j] = TileContents.Pellet;
                        }
                        break;
                    case 4:
                    case 28:
                        board[i, j] = TileContents.Pellet;
                        break;
                    case 5:
                    case 6:
                        if (j == 0 || j == 5 || j == 8 || j == 17 || j == 20 || j == 25)
                        {
                            board[i, j] = TileContents.Pellet;
                        }
                        break;
                    case 7:
                    case 25:
                        if (j != 6 && j != 7 && j != 12 && j != 13 && j != 18 && j != 19)
                        {
                            board[i, j] = TileContents.Pellet;
                        }
                        break;
                    case 8:
                    case 9:
                    case 10:
                    case 11:
                    case 12:
                    case 13:
                    case 14:
                    case 15:
                    case 16:
                    case 17:
                    case 18:
                        if (j == 5 || j == 20)
                        {
                            board[i, j] = TileContents.Pellet;
                        }
                        break;
                    case 22:
                        if (j != 3 && j != 4 && j != 12 && j != 13 && j != 21 && j != 22)
                        {
                            board[i, j] = TileContents.Pellet;
                        }
                        break;
                    case 23:
                    case 24:
                        if (j == 2 || j == 5 || j == 8 || j == 17 || j == 20 || j == 23)
                        {
                            board[i, j] = TileContents.Pellet;
                        }
                        break;
                    case 26:
                    case 27:
                        if (j == 0 || j == 11 || j == 14 || j == 25)
                        {
                            board[i, j] = TileContents.Pellet;
                        }
                        break;
                }
                switch (i)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                    case 19:
                    case 20:
                    case 21:
                    case 23:
                    case 24:
                    case 25:
                    case 26:
                    case 27:
                    case 28:
                        break;
                    case 8:
                    case 9:
                        if (j == 11 || j == 14)
                        {
                            board[i, j] = TileContents.InvisibleNode;
                        }
                        break;
                    case 10:
                    case 16:
                        if (j >= 8 && j <= 17)
                        {
                            board[i, j] = TileContents.InvisibleNode;
                        }
                        break;
                    case 11:
                    case 12:
                    case 14:
                    case 15:
                    case 17:
                    case 18:
                        if (j == 8 || j == 17)
                        {
                            board[i, j] = TileContents.InvisibleNode;
                        }
                        break;
                    case 13:
                        if ((j <= 8 && j != 5) || (j >= 17 && j != 20))
                        {
                            board[i, j] = TileContents.InvisibleNode;
                        }
                        break;

                    case 22:
                        if (j == 12 || j == 13)
                        {
                            board[i, j] = TileContents.InvisibleNode;
                        }
                        break;

                }



            }
        }

        board[2, 0] = TileContents.PowerUp;
        board[2, 25] = TileContents.PowerUp;
        board[22, 0] = TileContents.PowerUp;
        board[22, 25] = TileContents.PowerUp;
    }


    void InstantiatePrefabs()
    {
        for (int j = 0; j < cols; j++)
        {

            GameObject row = new GameObject("row: " + j);
            row.transform.parent = transform;

            for (int i = 0; i < rows; i++)
            {
                switch (board[i, j])
                {


                    case TileContents.Pellet:
                        GameObject pelletGO = Instantiate(PelletPrefab, new Vector3(j, -i, 0), Quaternion.identity);
                        Node temp = pelletGO.GetComponent<Node>();
                        temp.row = j;
                        temp.column = i;

                        pelletGO.transform.parent = row.transform;
                        pelletGO.name = "Pellet[" + j + ", " + i + "]";


                        GameObjects[i, j] = pelletGO;
                        break;
                    case TileContents.PowerUp:
                        GameObject powerUpGO = Instantiate(PowerUpPrefab, new Vector3(j, -i, 0), Quaternion.identity);
                        temp = powerUpGO.GetComponent<Node>();
                        temp.row = j;
                        temp.column = i;
                        powerUpGO.transform.parent = row.transform;
                        powerUpGO.name = "PowerUp[" + j + ", " + i + "]";


                        GameObjects[i, j] = powerUpGO;
                        break;
                    case TileContents.InvisibleNode:
                        GameObject invisibleNodeGO = Instantiate(InvisibleNodePrefab, new Vector3(j, -i, 0), Quaternion.identity);
                        temp = invisibleNodeGO.GetComponent<Node>();
                        temp.row = j;
                        temp.column = i;
                        invisibleNodeGO.transform.parent = row.transform;
                        invisibleNodeGO.name = "InvisibleNode[" + j + ", " + i + "]";


                        GameObjects[i, j] = invisibleNodeGO;
                        break;


                }


            }

        }
        int tempCol = 13;
        int tempRow = 11;

        GameObject blinkyGO = Instantiate(BlinkyPrefab, new Vector3(tempRow, -tempCol, 0), Quaternion.identity);
        Blinky tempBlinky = blinkyGO.GetComponent<Blinky>();
        tempBlinky.row = tempRow;
        tempBlinky.column = tempCol;

        blinkyGO.transform.parent = transform;

        GameObjects[tempCol, tempRow] = blinkyGO;


        tempRow = 12;
        GameObject pinkyGO = Instantiate(PinkyPrefab, new Vector3(tempRow, -tempCol, 0), Quaternion.identity);
        Pinky tempPinky = pinkyGO.GetComponent<Pinky>();
        tempPinky.row = tempRow;
        tempPinky.column = tempCol;

        pinkyGO.transform.parent = transform;


        GameObjects[tempCol, tempRow] = pinkyGO;

        tempRow = 13;
        GameObject inkyGO = Instantiate(InkyPrefab, new Vector3(tempRow, -tempCol, 0), Quaternion.identity);
        Inky tempInky = inkyGO.GetComponent<Inky>();
        tempInky.row = tempRow;
        tempInky.column = tempCol;

        inkyGO.transform.parent = transform;


        GameObjects[tempCol, tempRow] = inkyGO;


        tempRow = 14;
        GameObject clydeGO = Instantiate(ClydePrefab, new Vector3(tempRow, -tempCol, 0), Quaternion.identity);
        Clyde tempClyde = clydeGO.GetComponent<Clyde>();
        tempClyde.row = tempRow;
        tempClyde.column = tempCol;

        clydeGO.transform.parent = transform;


        GameObjects[tempCol, tempRow] = clydeGO;

    }


    public List<Node.Direction> GetValidDirections(Node currentNode)
    {
        List<Node.Direction> validDirections = new List<Node.Direction>();
        if (currentNode.row - 1 >= 0)
        {
            if (board[currentNode.column, currentNode.row - 1] != TileContents.Empty)
            {
                validDirections.Add(Node.Direction.Left);
            }
        }
        if (currentNode.row + 1 < rows)
        {
            if (board[currentNode.column, currentNode.row + 1] != TileContents.Empty)
            {
                validDirections.Add(Node.Direction.Right);
            }
        }
        if (currentNode.column - 1 >= 0)
        {
            if (board[currentNode.column - 1, currentNode.row] != TileContents.Empty)
            {
                validDirections.Add(Node.Direction.Up);
            }
        }
        if (currentNode.column + 1 < cols)
        {
            if (board[currentNode.column + 1, currentNode.row] != TileContents.Empty)
            {
                validDirections.Add(Node.Direction.Down);
            }
        }

        return validDirections;
    }


    public float CalculateDistance(Node node1, Node node2)
    {

        int rowDelt = node1.row - node2.row;
        int colDelt = node1.column - node2.column;
        return (float)Math.Sqrt(rowDelt * rowDelt + colDelt * colDelt);

    }

}
