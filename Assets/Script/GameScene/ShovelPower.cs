using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShovelPower : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(0.0f, 0.1f, 0.0f);
    }
    public void OnTriggerEnter2D(Collider2D n)
    {
        if (n.tag == "Cat")
        {
            Destroy(gameObject);
        }
        if (n.tag == "EnemyBullet")
        {
            Destroy(n.gameObject);
            Destroy(gameObject);
        }
    }
}
