using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DotAdd : MonoBehaviour {
    int dotAmount = 0;
    Text text;

    void Start()
    {
        text = gameObject.GetComponent<Text>();
        InvokeRepeating("Doting", 0, 1f);
    }
	// Update is called once per frame
	void Update () {

	}
    void Doting()
    {
        if (dotAmount < 2)
        {
            text.text += " .";
            dotAmount++;
        }
        else
        {
            text.text = ".";
            dotAmount = 0;
        }
    }
}
