using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pinky : Ghost
{




    // Update is called once per frame
    void Update()
    {
        GetTarget();

        timer += Time.deltaTime;
        if (timer >= 1f)
        {
            UpdatePosition();
            Move();
            timer = 0;
        }
    }

    void GetTarget()
    {
        //use pacmans world space 4 in front of that if I change get distance to take a position instead of a second node
        Target = GameBoard.instance.GameObjects[GameBoard.instance.PacCol, GameBoard.instance.PacRow].GetComponent<Node>();
    }
}
