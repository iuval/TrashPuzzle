using UnityEngine;
using System.Collections;

class Player : MonoBehaviour
{
    public int Points { get; private set; }

    public void AddPoints(int p)
    {
        Points += p;
    }

    void Start()
    {
        Points = 0;
    }

    void Update()
    {

    }
}
