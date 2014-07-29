using UnityEngine;
using System.Collections;

class Player : MonoBehaviour
{
    public int Points { get; private set; }
    public int Lifes { get; private set; }

    public void AddPoints(int p)
    {
        Points += p;
    }

    public void SubLife()
    {
        Lifes--;
    }

    void Start()
    {
        Points = 0;
        Lifes = 5;
    }

    void Update()
    {

    }
}
