using UnityEngine;
using System.Collections;

public class PauseButton : MonoBehaviour
{
    public MenuManager menu;

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
        menu.OpenPauseMenu();
    }
}
