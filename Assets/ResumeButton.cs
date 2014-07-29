using UnityEngine;
using System.Collections;

public class ResumeButton : MonoBehaviour
{
    public MenuManager menu;

    public Sprite normal_sprite;
    public Sprite press_sprite;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseUpAsButton()
    {
        menu.Resume();
    }

    void OnMouseUp()
    {
        ((SpriteRenderer)renderer).sprite = normal_sprite;
    }

    void OnMouseDown()
    {
        ((SpriteRenderer)renderer).sprite = press_sprite;
    }

}
