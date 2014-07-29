using UnityEngine;
using System.Collections;

public class TrashFactory : MonoBehaviour
{
    public float countFor100Persent = 10;
    public Sprite[] green_box_sprites;
    public Sprite[] blue_box_sprites;
    public Sprite[] yellow_box_sprites;
    public Sprite[] orange_box_sprites;

    public Sprite[] GetTrash(int count, int type)
    {
        Sprite[] result = new Sprite[count];
        float trashChance = rnd(100 - (150 / count), 100 / count) / 100f;//  Mathf.Pow(2f, count + 5) 
        float rand;

        for (int i = 0; i < count; i++)
        {
            rand = Random.Range(0, 1f);
            if (rand <= trashChance)
            {
                result[i] = getSpriteByType(type);
            }
            else
            {
                result[i] = null;
            }
        }
        return result;
    }

    private float rnd_snd()
    {
        return (Random.value * 2 - 1) + (Random.value * 2 - 1) + (Random.value * 2 - 1);
    }

    private float rnd(float mean, float stdev)
    {
        return Mathf.Round(rnd_snd() * stdev + mean);
    }

    private Sprite getSpriteByType(int type)
    {
        Sprite[] sprites = null;

        switch (type)
        {
            case 0:
                {
                    sprites = blue_box_sprites;
                    break;
                }
            case 1:
                {
                    sprites = green_box_sprites;
                    break;
                }
            case 2:
                {
                    sprites = yellow_box_sprites;
                    break;
                }
            case 3:
                {
                    sprites = orange_box_sprites;
                    break;
                }
        }

        return sprites[Random.Range(0, sprites.Length - 1)];
    }
}
