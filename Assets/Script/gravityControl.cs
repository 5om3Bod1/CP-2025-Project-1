using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravityControl : MonoBehaviour
{
    void Start()
    {
        Physics.gravity = new Vector3(0f, 2.5f, 0f);

        //Invoke("boom", 4.25f);
    }
    void boom()
    {
        Destroy(gameObject);
    }
}
