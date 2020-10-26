using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{

    public enum State {Idle, Chase, Scatter, Frightened, Dead};
    public State CurrentState;
    public Node CurrentNode { get;  private set; } //properties, public getting, private setting
    public Node Target;
    [SerializeField] private Node.Direction facing;
    private Node Optimal;
    public float timer;
    public int row;
    public int column;


    // Start is called before the first frame update
    void Start()
    {

        CurrentNode = GameBoard.instance.GameObjects[10, 13].GetComponent<Node>();
        Move();


    }



    // Update is called once per frame
    public void UpdatePosition()
    {
        if(CurrentState != State.Idle)
        {
            PickDirection();


        }
    }


    void PickDirection()
    {

        float distance = float.MaxValue;
        Node.Direction tempFacing = Node.Direction.None;
        foreach(Node.Direction dir in GameBoard.instance.GetValidDirections(CurrentNode))
        {

            if(dir != Node.OppositeDirection(facing))
            {

                Node testNode = GameBoard.instance.GetNodeInDirection(CurrentNode, dir);
                float testDistance = GameBoard.CalculateDistance(testNode, Target);
                
                if(testDistance < distance)
                {
                    //Debug.Log("facing: " + facing);
                    //Debug.Log("direction: " + dir);
                    distance = testDistance;
                    Optimal = testNode;
                    tempFacing = dir;
                }

            }

        }
        facing = tempFacing;
        


    }


    public void Move()
    {

        if (CurrentState != State.Idle && CurrentNode != null)
        {
            CurrentNode = Optimal;
            transform.position = CurrentNode.transform.position;
        }

    }

    public void Chase()
    {
        CurrentState = State.Chase;
    }

    public void Scatter()
    {
        CurrentState = State.Scatter;
    }

    public void BecomeFrightened()
    {
        CurrentState = State.Frightened;
    }

    public void Die()
    {
        CurrentState = State.Dead;
    }


}
