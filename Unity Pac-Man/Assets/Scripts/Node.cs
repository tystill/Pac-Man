using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{

    public enum Direction {None,Left,Right,Up,Down};
    public enum NodeType {Invisible, Pellet, PowerUp, Portal};
    public NodeType Type;
    public int row;
    public int column;
    public Direction PortalDirection;
    public Node PortalDestination;
    public AudioSource Eat;


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


    public static Vector3 DirectionToVector(Direction d)
    {
        switch (d)
        {

            case Direction.Left:
                return Vector3.left;
            case Direction.Right:
                return Vector3.right;
            case Direction.Up:
                return Vector3.up;
            case Direction.Down:
                return Vector3.down;

        }
        return Vector3.zero;

    }

}
