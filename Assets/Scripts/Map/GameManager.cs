using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Variables
    private float time_scale_factor = 1f;
    public static int score = 0;
    private string scoreMessage = "Score: ";
    public Text scoreObject;
    public GameObject loseMenu;
    public GameObject startMenu;

    // On start of game
    private void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Keep time updated with time_scale_factor
        Time.timeScale = time_scale_factor;

        // Keep score updated on screen
        scoreObject.text = makeScore(score);

        // Check if game is still running
        if(MC_Controller.MCamALIVE == false)
        {
            time_scale_factor = 0f;
            loseMenu.SetActive(true);
        }
    }

    // Update score
    private string makeScore(int score)
    {
        return scoreMessage + score.ToString();
    }

    // Replay level
    public void replayLevel()
    {
        SceneManager.LoadScene(0);
    }
}
