using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Ef_fade : MonoBehaviour {
    public Color color;
    public Image image;
    public Text text;
	// Use this for initialization
	void Start () {
        if (image != null)
        {
            color = image.color;
        }
        if (text != null)
        {
            color = text.color;
        }
    }
	
	// Update is called once per frame
	void Update () {
        color.a -= Time.deltaTime*0.5f;
        if (image != null)
        {
            image.color = color;
        }
        if(text != null)
        {
            text.color = color;
        }
        if(color.a <= 0)
        {
            this.gameObject.SetActive(false);
        }
	}
    public void AlphaReset()
    {
        color.a = 1;
    }
}
