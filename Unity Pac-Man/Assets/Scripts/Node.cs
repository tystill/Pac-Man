using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{

    public enum Direction {None,Left,Right,Up,Down};
    public int row;
    public int column;


    void Start()
    {
        tag = "Node";
       

    }


}
