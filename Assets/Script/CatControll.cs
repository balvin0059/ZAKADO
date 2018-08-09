using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatControll : MonoBehaviour {
    public static CatControll instance;
    public static CatControll Instance
    {
        get
        {
            return instance;
        }
    }
    public GameObject heart;
    // Use this for initialization
    private void Awake()
    {
        instance = this;
    }
    void Start () {
		
	}
	
    public void CreatHeart()
    {
        GameObject h = Instantiate(heart);
        h.transform.SetParent(gameObject.transform.parent, false);
        h.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y+1f, gameObject.transform.position.z);
    }
    void Attack()
    {
        
    }
}
