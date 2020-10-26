using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pinky : Ghost
{

    public override void Start()
    {
        base.Start();
        scatterTarget = GameBoard.instance.GameObjects[0, 0].transform.position + new Vector3(-1, 1, 0);
    }


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


    public override void SetChaseTarget()
    {

        Target = PacMan.instance.transform.position + 4 * Node.DirectionToVector(PacMan.instance.facing);
    }
}
