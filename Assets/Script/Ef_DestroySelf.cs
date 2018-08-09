using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Ef_DestroySelf : MonoBehaviour {
    public float delay = 1.0f;
    public Image image;
    public Color color;
	// Use this for initialization
	void Start () {
        Destroy(gameObject, delay);        
	}
	
	// Update is called once per frame
	void Update () {
        color.a -= Time.deltaTime*2;
        image.color = color;
	}
}
