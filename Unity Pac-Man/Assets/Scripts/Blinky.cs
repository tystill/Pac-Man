using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Blinky : Ghost
{

    public override void Start()
    {
        base.Start();
        scatterTarget = GameBoard.instance.GameObjects[0, 25].transform.position + new Vector3(1, 1, 0);
    }




    public override void SetChaseTarget()
    {
        Target = PacMan.instance.transform.position;
    }
}
