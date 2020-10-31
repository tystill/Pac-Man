using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Animator PacMan, Blinky, Pinky, Inky, Clyde;

    public GameObject PowerUp;
    private Vector3 startingPosition;


    void Start()
    {
        startingPosition = PacMan.transform.position;

        StartCoroutine(Animation());

    }

    IEnumerator Animation()
    {

        bool facingLeft = true;

        PacMan.transform.rotation = Quaternion.Euler(0, 0, 180);
        Blinky.SetBool("FacingRight", false);
        Blinky.SetBool("FacingLeft", true);
        Blinky.SetBool("FacingUp", false);
        Blinky.SetBool("FacingDown", false);

        Pinky.SetBool("FacingRight", false);
        Pinky.SetBool("FacingLeft", true);
        Pinky.SetBool("FacingUp", false);
        Pinky.SetBool("FacingDown", false);

        Inky.SetBool("FacingRight", false);
        Inky.SetBool("FacingLeft", true);
        Inky.SetBool("FacingUp", false);
        Inky.SetBool("FacingDown", false);

        Clyde.SetBool("FacingRight", false);
        Clyde.SetBool("FacingLeft", true);
        Clyde.SetBool("FacingUp", false);
        Clyde.SetBool("FacingDown", false);

        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            PacMan.transform.position += new Vector3(facingLeft?-1:1, 0, 0);
            Blinky.transform.position += new Vector3(facingLeft ? -1 : 1, 0, 0);
            Pinky.transform.position += new Vector3(facingLeft ? -1 : 1, 0, 0);
            Inky.transform.position += new Vector3(facingLeft ? -1 : 1, 0, 0);
            Clyde.transform.position += new Vector3(facingLeft ? -1 : 1, 0, 0);

            if (PacMan.transform.position == PowerUp.transform.position)
            {
                facingLeft = false;

                PowerUp.GetComponent<SpriteRenderer>().enabled = false;
                PacMan.transform.rotation = Quaternion.Euler(0, 0, 0);
                Blinky.SetBool("FacingRight", true);
                Blinky.SetBool("FacingLeft", false);
                Blinky.SetInteger("Condition", 1);

                Pinky.SetBool("FacingRight", true);
                Pinky.SetBool("FacingLeft", false);
                Pinky.SetInteger("Condition", 1);

                Inky.SetBool("FacingRight", true);
                Inky.SetBool("FacingLeft", false);
                Inky.SetInteger("Condition", 1);

                Clyde.SetBool("FacingRight", true);
                Clyde.SetBool("FacingLeft", false);
                Clyde.SetInteger("Condition", 1);


            }

            if (PacMan.transform.position == startingPosition && !facingLeft)
            {
                facingLeft = true;

                PowerUp.GetComponent<SpriteRenderer>().enabled = true;
                PacMan.transform.rotation = Quaternion.Euler(0, 0, 180);
                Blinky.SetBool("FacingRight", false);
                Blinky.SetBool("FacingLeft", true);
                Blinky.SetInteger("Condition", 0);

                Pinky.SetBool("FacingRight", false);
                Pinky.SetBool("FacingLeft", true);
                Pinky.SetInteger("Condition", 0);

                Inky.SetBool("FacingRight", false);
                Inky.SetBool("FacingLeft", true);
                Inky.SetInteger("Condition", 0);

                Clyde.SetBool("FacingRight", false);
                Clyde.SetBool("FacingLeft", true);
                Clyde.SetInteger("Condition", 0);


            }



        }

    }

    public void Play()
    {

        SceneManager.LoadScene("Game");

    }

    public void Quit()
    {

        Application.Quit();

    }

}
