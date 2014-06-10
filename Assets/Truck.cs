using UnityEngine;
using System.Collections;

public class Truck : MonoBehaviour
{
    private enum State { GoingIn, Playing, StartingMotor, GoingOut, Out }

    public float vel = 10f;

    private State state;

    public Board board;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.GoingIn:
                {
                    if (transform.position.y < 8)
                    {
                        Vector2 pos = transform.position;
                        pos.y += vel * Time.deltaTime;
                        transform.position = pos;
                    }
                    else
                    {
                        state = State.Playing;
                        board.StartGame();
                    }
                } break;
            case State.Playing:
                {

                } break;
            case State.StartingMotor:
                {
                    Debug.Log("StartingMotor");
                    state = State.GoingOut;
                } break;
            case State.GoingOut:
                {
                    Debug.Log("GoingOut");
                    if (transform.position.y < 32)
                    {
                        Vector2 pos = transform.position;
                        pos.y += vel * Time.deltaTime;
                        transform.position = pos;
                    }
                    else
                    {
                        state = State.Out;
                    }
                } break;
            case State.Out:
                {
                    Init();
                    state = State.GoingIn;
                } break;
        }
    }

    public void Init()
    {
        state = State.Out;
        transform.position = new Vector2(-3, -30);
    }

    public void GoIn()
    {
        state = State.GoingIn;
    }

    public void GoOut()
    {
        state = State.StartingMotor;
    }
}
