using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstScene : MonoBehaviour {
    public GameObject loading;
	// Use this for initialization
	void Start () {
        loading.GetComponent<Loading>().GotoScene("MainScene");
	}
}
