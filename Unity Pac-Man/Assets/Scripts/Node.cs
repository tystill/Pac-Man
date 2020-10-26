using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{

    public enum Direction {None,Left,Right,Up,Down};
    public enum NodeType {Invisible, Pellet, PowerUp};
    public NodeType Type;
    public int row;
    public int column;


    void Start()
    {
        tag = "Node";
       

    }


    public static Direction OppositeDirection(Direction d)
    {
        switch (d){

            case Direction.Left:
                return Direction.Right;
            case Direction.Right:
                return Direction.Left;
            case Direction.Up:
                return Direction.Down;
            case Direction.Down:
                return Direction.Up;

        }
        return Direction.None;
    }

}
