using UnityEngine;
using System.Collections;

public class RecycleCanText : MonoBehaviour
{
    public int TrashCanValue { get; private set; }
    private GUIText text;

    public void AddToTrash(int value)
    {
        TrashCanValue += value;
        text.text = TrashCanValue.ToString();
    }

    // Use this for initialization
    void Start()
    {
        text = GetComponentInChildren<GUIText>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
