using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clyde : Ghost
{

    public override void Start()
    {
        base.Start();
        scatterTarget = GameBoard.instance.GameObjects[28, 0].transform.position + new Vector3(-1, -1, 0);
    }





    public override void SetChaseTarget()
    {
        if (Vector3.Distance(transform.position, PacMan.instance.transform.position) > 8)
        {

            Target = PacMan.instance.transform.position;
        }
        else
        {
            Target = scatterTarget;
        }
    }
}
