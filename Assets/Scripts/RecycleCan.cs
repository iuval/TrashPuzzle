using UnityEngine;
using System.Collections;

public class RecycleCan : MonoBehaviour
{
    public int TrashCanValue { get; private set; }

    public Sprite[] trash_sprites;
    public int[] steps;
    private int sprite_index = 0;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddToTrash(int value)
    {
        GetComponent<Animator>().SetTrigger("shake");
        TrashCanValue += value;
        sprite_index++;

        if (sprite_index < trash_sprites.Length && steps[sprite_index] >= TrashCanValue)
        {
            ((SpriteRenderer)renderer).sprite = trash_sprites[sprite_index];
        }
    }
}
