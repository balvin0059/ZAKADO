using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Afterimage : MonoBehaviour {
    public GameObject self;
    int howmany = 0;
	// Use this for initialization
	void Start () {
        InvokeRepeating("CreateClone", 0.1f, 0.1f);
	}
	
	// Update is called once per frame
	void Update () {
        if (howmany > 2)
        {
            CancelInvoke();
        }
	}
    public void CreateClone()
    {
        Instantiate(self, new Vector3(0,0,0), Quaternion.identity, gameObject.transform.parent);
        ++howmany;
    }
    void OnDestroy()
    {
        self = null;
        Destroy(gameObject);
        Debug.Log("釋放記憶體");
    }
}
