using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Blinky : Ghost
{





    // Update is called once per frame
    void Update()
    {
        GetTarget();

        timer += Time.deltaTime;
        if(timer >= 1f)
        {
            UpdatePosition();
            Move();
            timer = 0;
        }
    }

    void GetTarget()
    {
        Target = GameBoard.instance.GameObjects[GameBoard.instance.PacCol, GameBoard.instance.PacRow].GetComponent<Node>();
    }
}
