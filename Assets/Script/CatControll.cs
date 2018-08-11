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
    public int getPower;
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
        h.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y+1.5f, gameObject.transform.position.z);
    }
    public void Attack()
    {
        if (getPower > 0)
        {
            StartCoroutine(AttackMove());
        }
    }
    IEnumerator AttackMove()
    {
        TurnControll.instance.turnState = TurnControll.TurnState.turnAttacking;
        gameObject.transform.position += new Vector3(0.0f, 0.1f, 0.0f);        
        yield return new WaitForSeconds(0.1f);
        gameObject.transform.position -= new Vector3(0.0f, 0.1f, 0.0f);
    }
    void GetPower(int gp)
    {
        getPower += gp;
    }
}
