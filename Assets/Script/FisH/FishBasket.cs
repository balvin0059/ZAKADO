using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishBasket : MonoBehaviour {

    Button button;
    void Awake()
    {
        button = gameObject.GetComponent<Button>();
    }
    void Update()
    {
        if(FishHolder.instance.amount > 0)
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
    }
}
