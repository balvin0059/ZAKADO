using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ef_Dself : MonoBehaviour {
    public float delay = 1.0f;
    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, delay);
    }
}
