using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour
{
    private TextMesh score_text;
    private TextMesh lifes_text;

    public GameObject board_go;
    public Color scoreColor;

    public GameObject pause_menu_go;

    private Rect btnInGameMenu;

    private Player player;

    // Use this for initialization
    void Start()
    {
        player = board_go.GetComponent<Player>();
        score_text = GameObject.Find("score_text").GetComponent<TextMesh>();
        lifes_text = GameObject.Find("lifes_text").GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        score_text.text = player.Points + "";
        lifes_text.text = player.Lifes + "";
    }
}
