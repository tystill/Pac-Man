using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{

    public Ghost Blinky, Pinky, Inky, Clyde;
    public static Game instance;
    public int Lives = 3;
    public byte Level = 0;
    public int Score = 0;
    public Text ScoreText;
    public Text HighScoreText;
    public Sprite[] GhostScores;
    public int[] GhostScoreNums;
    public Sprite[] Fruits;
    public Sprite[] FruitScores;
    public int[] FruitScoreNums;
    public GameObject FruitPrefab;
    public GameObject[] FruitList = new GameObject[5];
    public GameObject FruitScorePrefab;
    public GameObject ScorePrefab;
    private int fruitCount = 0;
    public int HighScore;
    public bool GameStarted = false;

    public TextMesh TopText;
    public TextMesh BottomText;
    public Image[] LiveImages;

    public GameObject[] GhostSounds;



    void Start()
    {
        instance = this;
        TopText.color = Color.cyan;
        BottomText.color = Color.yellow;
        SetFruitList();

        HighScore = PlayerPrefs.GetInt("High Score");
        HighScoreText.text = "" + HighScore;

        Blinky = GameBoard.instance.GameObjects[13, 11].GetComponent<Ghost>();
        Pinky = GameBoard.instance.GameObjects[13, 12].GetComponent<Ghost>();
        Inky = GameBoard.instance.GameObjects[13, 13].GetComponent<Ghost>();
        Clyde = GameBoard.instance.GameObjects[13, 14].GetComponent<Ghost>();

        ((Inky)Inky).BlinkyReference = (Blinky)Blinky;

    }

    public void StartGame()
    {
        GameStarted = true;
        TopText.text = "";
        BottomText.text = "";
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {


        Blinky.Scatter();
        Blinky.gameObject.transform.position = Blinky.CurrentNode.gameObject.transform.position;
        if (Level == 0)
        {
            yield return new WaitForSeconds(3f);
        }
        else
        {
            yield return new WaitForSeconds(0.1f);
        }
        Pinky.Scatter();
        Pinky.gameObject.transform.position = Pinky.CurrentNode.gameObject.transform.position;

        yield return new WaitForSeconds(3f);
        Inky.Scatter();
        Inky.gameObject.transform.position = Inky.CurrentNode.gameObject.transform.position;

        yield return new WaitForSeconds(3f);
        Clyde.Scatter();
        Clyde.gameObject.transform.position = Clyde.CurrentNode.gameObject.transform.position;
    }

    void Update()
    {
        FruitSpawn();
        //PlayGhostSound();
        //can't get list of sounds to work in any way
        ScoreText.text = "" + Score;




        if (Lives == 0)
        {
            StartCoroutine(GameOver());
        }
        if (GameBoard.instance.IsLevelComplete())
        {
            LevelComplete();
        }

        if(Score > HighScore)
        {
            HighScore = Score;
            HighScoreText.text = "" + HighScore;
        }

        if (Blinky.CurrentState != Ghost.State.Frightened && Pinky.CurrentState != Ghost.State.Frightened &&
            Inky.CurrentState != Ghost.State.Frightened && Clyde.CurrentState != Ghost.State.Frightened)
        {
            //Debug.Log("No Ghosts Afraid");
            PacMan.instance.ghostsEaten = 0;
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PlayerPrefs.SetInt("High Score", HighScore);

            SceneManager.LoadScene("Menu");
        }

    }

    void PlayGhostSound()
    {
        if(Blinky.CurrentState == Ghost.State.Dead || Pinky.CurrentState == Ghost.State.Dead ||
            Inky.CurrentState == Ghost.State.Dead || Clyde.CurrentState == Ghost.State.Dead)
        {
            GameObject ghostSound = Instantiate(GhostSounds[0], transform.position, Quaternion.identity);

            GhostSounds[1].GetComponent<AudioSource>().Pause();
            GhostSounds[2].GetComponent<AudioSource>().Pause();

        }
        else if (Blinky.CurrentState == Ghost.State.Frightened || Pinky.CurrentState == Ghost.State.Frightened ||
            Inky.CurrentState == Ghost.State.Frightened || Clyde.CurrentState == Ghost.State.Frightened)
        {
            GhostSounds[0].GetComponent<AudioSource>().Pause();
            GameObject ghostSound = Instantiate(GhostSounds[1], transform.position, Quaternion.identity);
            GhostSounds[2].GetComponent<AudioSource>().Pause();
        }

        else if (Blinky.CurrentState == Ghost.State.Chase || Blinky.CurrentState == Ghost.State.Scatter||
            Pinky.CurrentState == Ghost.State.Chase || Pinky.CurrentState == Ghost.State.Scatter||
            Inky.CurrentState == Ghost.State.Chase || Inky.CurrentState == Ghost.State.Scatter||
            Clyde.CurrentState == Ghost.State.Chase || Clyde.CurrentState == Ghost.State.Scatter)
        {
            GhostSounds[0].GetComponent<AudioSource>().Pause();
            GhostSounds[1].GetComponent<AudioSource>().Pause();
            GameObject ghostSound = Instantiate(GhostSounds[2], transform.position, Quaternion.identity);
        }
    }

    void SetFruitList()
    {
        //called in start and next level
        for(int i = 0; i < FruitList.Length; i++)
        {
            if(LevelToIndex() - i >= 0)
            {
                FruitList[i] = Instantiate(FruitScorePrefab, new Vector3(-(2 * i) + 24, -31.25f, 0), Quaternion.identity);
                FruitList[i].GetComponent<SpriteRenderer>().sprite = Fruits[LevelToIndex() - i];
            }
        }

    }

    void DestroyFruitList()
    {
        foreach(GameObject fruit in FruitList)
        {
            Destroy(fruit);
        }
    }

    void FruitSpawn()
    {
        if((GameBoard.instance.pelletCount == 174 && fruitCount == 0) || (GameBoard.instance.pelletCount == 74 && fruitCount == 1))
        {
            GameObject fruit = Instantiate(FruitPrefab, new Vector3(12.5f, -16, 0), Quaternion.identity);
            fruit.GetComponent<SpriteRenderer>().sprite = Fruits[LevelToIndex()];
            Destroy(fruit, 10);
            fruitCount++;
        }
    }

    public int LevelToIndex()
    {

        switch (Level)
        {
            case 0:
            case 1:
                return Level;
            case 2:
            case 3:
                return 2;
            case 4:
            case 5:
                return 3;
            case 6:
            case 7:
                return 4;
            case 8:
            case 9:
                return 5;
            case 10:
            case 11:
                return 6;
            default:
                return 7;
        }
    }

    IEnumerator GameOver()
    {
        //display game over, stop things
        BottomText.text = "Game Over";
        BottomText.color = Color.red;
        PlayerPrefs.SetInt("High Score", HighScore);
        //display game over


        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene("Menu");

    }

    public void LevelComplete()
    {
        //take to next level

        //Debug.Log("Level Complete!");
        Level++;
        GameBoard.instance.ClearGameObjects();
        Destroy(Blinky);
        Destroy(Pinky);
        Destroy(Inky);
        Destroy(Clyde);
        GameBoard.instance.InstantiatePrefabs();
        GameBoard.instance.pelletCount = 244;
        DestroyFruitList();
        SetFruitList();
        PacMan.instance.Respawn();
        fruitCount = 0;

        Blinky = GameBoard.instance.GameObjects[13, 11].GetComponent<Ghost>();
        Blinky.Start();

        Pinky = GameBoard.instance.GameObjects[13, 12].GetComponent<Ghost>();
        Pinky.Start();

        Inky = GameBoard.instance.GameObjects[13, 13].GetComponent<Ghost>();
        Inky.Start();

        Clyde = GameBoard.instance.GameObjects[13, 14].GetComponent<Ghost>();
        Clyde.Start();


        ((Inky)Inky).BlinkyReference = (Blinky)Blinky;

        StartCoroutine(Spawn());

    }



}
