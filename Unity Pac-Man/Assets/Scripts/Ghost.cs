using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ghost : MonoBehaviour
{

    public enum State { Idle, Chase, Scatter, Frightened, Dead };
    public State CurrentState;
    public Node CurrentNode { get; set; } //properties, public getting, private setting
    public Vector3 Target;
    public Node.Direction facing;
    private Node Optimal;

    public int row;
    public int column;
    public Vector3 scatterTarget;
    public Vector3 startingPosition;
    private float frightenedTimer;
    private float chaseTimer;
    private float scatterTimer;
    private int chaseCount, scatterCount;
    private int[,] chaseTimeList = { { 20, 20, 20, int.MaxValue }, { 20, 20, 1033, int.MaxValue }, { 20, 20, 1037, int.MaxValue } };
    private int[,] scatterTimeList = { { 7, 7, 5, 5 }, { 7, 7, 5, 0 }, { 5, 5, 5, 0 } };
    private Animator Animator;
    private float speedPercent = 1;
    private float timer;
    private Vector3 currentPosition;
    private float speed = 4f;


    public abstract void SetChaseTarget();



    public virtual void Start()
    {
        //Debug.Log("Made Ghost");
        Animator = GetComponent<Animator>();
        CurrentNode = GameBoard.instance.GameObjects[10, 13].GetComponent<Node>();
        Move();
        PickDirection();
        startingPosition = transform.position;
        currentPosition = startingPosition;

    }

    void Update()
    {

        if (/*!PacMan.instance.dead && */Game.instance.Lives != 0)
        {
            GetTarget();
            UpdateState();
            UpdateSpeed();
            UpdateAnimation();
            Move();
            if (CurrentState == State.Dead && transform.position == GameBoard.instance.GameObjects[10, 13].transform.position)
            {
                StartCoroutine(Respawn());
            }
        }
    }

    int LevelToIndex()
    {

        if (Game.instance.Level == 0)
        {
            return 0;
        }
        else if (Game.instance.Level >= 1 && Game.instance.Level <= 3)
        {
            return 1;
        }
        else
        {
            return 2;
        }

    }

    public void UpdatePosition()
    {
        if (CurrentState != State.Idle)
        {
            PickDirection();


        }
    }

    public void UpdateState()
    {
        switch (CurrentState)
        {
            case State.Frightened:
                frightenedTimer -= Time.deltaTime;
                if (frightenedTimer <= 0)
                {
                    CurrentState = scatterTimer > 0 ? State.Scatter : State.Chase;

                }
                break;
            case State.Chase:
                chaseTimer -= Time.deltaTime;
                if (chaseTimer <= 0)
                {
                    CurrentState = State.Scatter;
                    chaseCount++;
                    scatterTimer = scatterTimeList[LevelToIndex(), scatterCount];
                }
                break;
            case State.Scatter:
                scatterTimer -= Time.deltaTime;
                if (scatterTimer <= 0)
                {
                    CurrentState = State.Chase;
                    scatterCount++;
                    chaseTimer = chaseTimeList[LevelToIndex(), chaseCount];
                }
                break;
        }


    }

    void UpdateSpeed()
    {
        if (Game.instance.Level == 0)
        {
            speedPercent = CurrentState == State.Frightened ? 0.5f : 0.75f;
        }
        else if (Game.instance.Level >= 1 && Game.instance.Level <= 3)
        {
            speedPercent = CurrentState == State.Frightened ? 0.55f : 0.85f;
        }
        else if (Game.instance.Level >= 4 && Game.instance.Level <= 19)
        {
            speedPercent = CurrentState == State.Frightened ? 0.6f : 0.95f;
        }
        else
        {
            speedPercent = 0.95f;
        }
        if (CurrentState == State.Dead)
        {
            speedPercent = 4;
        }

    }

    void UpdateAnimation()
    {
        switch (CurrentState)
        {
            case State.Chase:
            case State.Scatter:
                Animator.SetBool("TimeUp", false);
                Animator.SetInteger("Condition", 0);
                break;
            case State.Frightened:
                Animator.SetInteger("Condition", 1);
                Animator.SetBool("TimeUp", false);

                if (frightenedTimer < 4)
                {
                    Animator.SetBool("TimeUp", true);

                }
                break;
            case State.Dead:
                Animator.SetBool("TimeUp", false);
                Animator.SetInteger("Condition", 2);
                break;

            case State.Idle:
                Animator.SetBool("TimeUp", false);
                Animator.SetInteger("Condition", -1);
                break;
        }
    }

    void PickDirection()
    {

        float distance = float.MaxValue;
        Node.Direction tempFacing = Node.Direction.None;
        foreach (Node.Direction dir in GameBoard.instance.GetValidDirections(CurrentNode))
        {

            if (dir != Node.OppositeDirection(facing))
            {

                Node testNode = GameBoard.instance.GetNodeInDirection(CurrentNode, dir);
                float testDistance = Vector3.Distance(testNode.transform.position, Target);

                if (testDistance < distance)
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

        switch (facing)
        {
            case Node.Direction.Right:
                Animator.SetBool("FacingRight", true);
                Animator.SetBool("FacingLeft", false);
                Animator.SetBool("FacingUp", false);
                Animator.SetBool("FacingDown", false);

                break;
            case Node.Direction.Left:
                Animator.SetBool("FacingRight", false);
                Animator.SetBool("FacingLeft", true);
                Animator.SetBool("FacingUp", false);
                Animator.SetBool("FacingDown", false);

                break;
            case Node.Direction.Up:
                Animator.SetBool("FacingRight", false);
                Animator.SetBool("FacingLeft", false);
                Animator.SetBool("FacingUp", true);
                Animator.SetBool("FacingDown", false);

                break;
            case Node.Direction.Down:
                Animator.SetBool("FacingRight", false);
                Animator.SetBool("FacingLeft", false);
                Animator.SetBool("FacingUp", false);
                Animator.SetBool("FacingDown", true);

                break;
        }

    }

    public void Move()
    {

        if (CurrentState != State.Idle && CurrentNode != null)
        {
            CurrentNode = Optimal;


            transform.position = Vector3.Lerp(currentPosition, CurrentNode.transform.position, timer * speedPercent * speed);
            timer += Time.deltaTime;

        }
        if (transform.position == CurrentNode.transform.position)
        {
            currentPosition = transform.position;
            timer = 0;
            UpdatePosition();

        }

    }

    public IEnumerator Respawn()
    {
        CurrentState = State.Idle;
        transform.position = startingPosition;
        CurrentNode = GameBoard.instance.GameObjects[10, 13].GetComponent<Node>();
        currentPosition = CurrentNode.transform.position;
        facing = Node.Direction.Up;
        Optimal = CurrentNode;

        yield return new WaitForSeconds(3f);

        CurrentState = scatterTimer > 0 ? State.Scatter : State.Chase;


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

        facing = Node.OppositeDirection(facing);
        if (Game.instance.Level == 0)
        {
            frightenedTimer = 8;
        }
        else if (Game.instance.Level >= 1 && Game.instance.Level <= 3)
        {
            frightenedTimer = 5;
        }
        else if (Game.instance.Level >= 4 && Game.instance.Level <= 19)
        {
            frightenedTimer = 2;
        }
        else
        {
            frightenedTimer = 0;
        }


        CurrentState = State.Frightened;
    }

    public void Die()
    {
        CurrentState = State.Dead;
    }

    public void GetTarget()
    {

        switch (CurrentState)
        {

            case State.Chase:
                SetChaseTarget();
                break;
            case State.Scatter:
                Target = scatterTarget;
                break;
            case State.Frightened:
                List<Node.Direction> directions = GameBoard.instance.GetValidDirections(CurrentNode, facing, false);
                Target = GameBoard.instance.GetNodeInDirection(CurrentNode, directions[Random.Range(0, directions.Count)]).transform.position;
                break;
            case State.Dead:
                Target = startingPosition;
                break;
        }
    }


}
