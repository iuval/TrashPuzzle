using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour
{
    public GUISkin skin;
    public GameObject board_go;
    public Color scoreColor;

    private Rect btnInGameMenu;

    private Player player;

    // Use this for initialization
    void Start()
    {

        player = board_go.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnGUI()
    {
        GUI.skin = skin;
        GUI.BeginGroup(new Rect(0, 0, Screen.width, Screen.height));

        GUI.color = scoreColor;
        GUI.Label(new Rect(420, 20, 360, 370), player.Points + "");

        if (GUI.Button(new Rect(250, 0, 50, 50), "Menu"))
        {
            //pause the game
            //    Time.timeScale = 0;
            //show the pause menu
            PauseMenu pauseMenu = GetComponent<PauseMenu>();
            pauseMenu.enabled = true;
        }
        GUI.EndGroup();
    }
}
