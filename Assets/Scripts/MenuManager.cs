using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKey("escape"))
        {
            //pause the game
            Time.timeScale = 0;
            //show the pause menu
            PauseMenu pauseMenu = GetComponent<PauseMenu>();
            pauseMenu.enabled = true;
        }
    }

    public void ShowEndGameMenu(bool playerWon)
    {
        //PauseMenu pauseMenu = GetComponent<PauseMenu>();
        //pauseMenu.enabled = true;
        //pauseMenu.mustUpdate = true;
        //pauseMenu.showScores = true;
        //pauseMenu.showResume = false;
        //pauseMenu.showMessage = true;
        //pauseMenu.playerWon = playerWon;
    }

    public void ShowStartGameMenu()
    {
        //PauseMenu pauseMenu = GetComponent<PauseMenu>();
        //pauseMenu.enabled = true;
        //pauseMenu.mustUpdate = true;
        //pauseMenu.showScores = false;
        //pauseMenu.showResume = false;
        //pauseMenu.showMessage = false;
    }
}
