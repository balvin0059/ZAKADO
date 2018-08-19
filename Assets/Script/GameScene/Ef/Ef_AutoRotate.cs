using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ef_AutoRotate : MonoBehaviour {

    public float speed = 10f;
    //public Vector3 dir = Vector3.forward;


    // Update is called once per frame
    void Update () {

        transform.Rotate(Vector3.forward * speed * Time.deltaTime);//移動

    }
}
