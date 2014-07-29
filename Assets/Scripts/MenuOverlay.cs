using UnityEngine;
using System.Collections;

public class MenuOverlay : MonoBehaviour
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

    public void Show()
    {
        GetComponent<Animator>().SetTrigger("show");
    }

    public void Hide()
    {
        GetComponent<Animator>().SetTrigger("hide");
    }
}
