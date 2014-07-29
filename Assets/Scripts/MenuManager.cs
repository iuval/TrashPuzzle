using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    private enum Menu { None, Pause, Game }

    private Menu openMenu;

    public PauseMenu pauseMenu;
    public MenuOverlay bg;

    public Board board;

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

    public void OpenPauseMenu()
    {
        board.PauseGame();
        bg.enabled = true;
        openMenu = Menu.Pause;
        pauseMenu.enabled = true;
        pauseMenu.GetComponent<Animator>().SetTrigger("show_menu");
        bg.Show();
    }


    public void Resume()
    {
        board.ResumeGame(   );
        Close();
    }

    public void Close()
    {
        switch (openMenu)
        {
            case Menu.Pause:
                {
                    pauseMenu.GetComponent<Animator>().SetTrigger("hide_menu");
                    break;
                }
            case Menu.Game:
                {
                    //pauseMenu.GetComponent<Animator>().SetTrigger("hide_menu");
                    break;
                }
        }
        bg.GetComponent<Animator>().SetTrigger("hide");
        openMenu = Menu.None;
    }

}
