﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorePower : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		gameObject.transform.Translate (0.0f, 0.1f, 0.0f);

	}
	public void OnTriggerEnter2D(Collider2D n){
        if (n.tag == "Cat")
        {
            n.GetComponent<CatControll>().CreatHeart();
            n.SendMessage("GetPower", 1);
            Destroy(gameObject);
        }
	}

}
