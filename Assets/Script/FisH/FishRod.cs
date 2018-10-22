using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FishRod : MonoBehaviour {
    public Sprite[] sprites;
    public GameObject[] wave;
    Image image;
	// Use this for initialization
	void Start () {
        image = gameObject.GetComponent<Image>();
        ChangeWave();
    }

    // Update is called once per frame
    void Update()
    {
        wave[(int)FishHolder.instance.fishValue.rodQuality - 1].GetComponent<Animator>().SetBool("Hooking", FishControll.instance.Hooking);
    }
    public void ChangeWave()
    {
        image.sprite = sprites[(int)FishHolder.instance.fishValue.rodQuality - 1];
        for (int i = 0; i < wave.Length; i++)
        {
            if (i == (int)FishHolder.instance.fishValue.rodQuality - 1)
            {
                wave[i].SetActive(true);
            }
            else
            {
                wave[i].SetActive(false);
            }
        }
    }
}
