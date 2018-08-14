using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIDrop : MonoBehaviour {
    public GameObject bullet;
	// Use this for initialization
	void Start () {        
	}
    void Spawn()
    {
        GameObject b = Instantiate(bullet);
        b.transform.SetParent(gameObject.transform.parent, false);
        b.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
    }

}
