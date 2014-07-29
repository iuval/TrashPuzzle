using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour
{
    public Board board;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateData()
    {
        TextMesh score_text = GameObject.Find("score_text").GetComponent<TextMesh>();
        TextMesh menu_score_text = GameObject.Find("menu_score_text").GetComponent<TextMesh>();
        menu_score_text.text = score_text.text;
    }
}
