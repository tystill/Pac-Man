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


    public override void SetChaseTarget()
    {
        Target = PacMan.instance.transform.position;
    }
}
