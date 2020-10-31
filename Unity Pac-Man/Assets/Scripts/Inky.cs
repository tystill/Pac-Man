using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inky : Ghost
{


    public Blinky BlinkyReference;


    public override void Start()
    {
        base.Start();
        scatterTarget = GameBoard.instance.GameObjects[28, 25].transform.position + new Vector3(1, -1, 0);
    }

  


    public override void SetChaseTarget()
    {
        Vector3 intermediary = PacMan.instance.transform.position + 2 * Node.DirectionToVector(PacMan.instance.facing);

        Vector3 inverse = BlinkyReference.transform.position - intermediary;

        Target = inverse;
    }
}
