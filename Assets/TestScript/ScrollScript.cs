using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollScript : MonoBehaviour {
    public Scrollbar scrollbar;
    public RectTransform rect;
    public float ind = 0;
    public int index = 0;
    // Update is called once per frame
    void Start()
    {

    }
    void Update () {
        if (Input.GetMouseButtonUp(0))
        {
            if (scrollbar.value < 0.08f)
            {
                scrollbar.value = 0;
                index = 0;
            }
            else if(scrollbar.value > 0.9f)
            {
                scrollbar.value = 1;
                index = (int)ind-1;
            }
            else if(scrollbar.value > 1 / (ind - 1) * index + 0.08f)
            {
                scrollbar.value = 1 / (ind-1) * (index + 1);
                index += 1;
            }
            else if (scrollbar.value < 1 / (ind - 1) * index - 0.08f)
            {
                scrollbar.value = 1 / (ind - 1) * (index-1);
                index -= 1;
            }
            else
            {
                scrollbar.value = 1 / (ind - 1) * index;
            }
        }
	}
    //public void ButtonDown()
    //{
    //    w += 540f;
    //    ind += 1;
    //    rect.GetComponent<RectTransform>().sizeDelta = new Vector2(w, 778);
    //}
}
