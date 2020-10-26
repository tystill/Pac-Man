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
