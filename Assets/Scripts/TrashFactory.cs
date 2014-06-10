using UnityEngine;
using System.Collections;

public class TrashFactory : MonoBehaviour
{
    public float countFor100Persent = 10;
    public GameObject[] trashCansInOrder;

    public GameObject[] GetTrash(int count, int type)
    {
        GameObject[] result = new GameObject[count];
        float trashChance = rnd(100 - (150 / count), 100 / count) / 100f;//  Mathf.Pow(2f, count + 5) 
        float rand;

        for (int i = 0; i < count; i++)
        {
            rand = Random.Range(0, 1f);
            if (rand <= trashChance)
            {
                result[i] = (GameObject)GameObject.Instantiate(trashCansInOrder[Random.Range(0, trashCansInOrder.Length - 1)]);
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
}
