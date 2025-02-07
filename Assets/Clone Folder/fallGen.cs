using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallGen : MonoBehaviour
{
    int randPick;
    public GameObject[] item;

    public Vector3[] spawn;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp("1"))
        {
            //randPick = Random.Range(0, 3 + 1);
            Instantiate(item[0], spawn[0], Quaternion.identity);
            Debug.Log(randPick);
        }
        else if (Input.GetKeyUp("2"))
        {
            //randPick = Random.Range(4, 6 + 1);
            Instantiate(item[1], spawn[1], Quaternion.identity);
            Debug.Log(randPick);
        }
        else if (Input.GetKeyUp("3"))
        {
            //randPick = Random.Range(7, 9 + 1);
            Instantiate(item[2], spawn[2], Quaternion.identity);
            Debug.Log(randPick);
        }

    }
}
