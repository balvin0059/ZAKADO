using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodBubble : MonoBehaviour {
    public GameObject ef_star;
    public int elemenyType;
    public GameObject bubble;
    public bool isEat = false;
    public Vector3 myTransform;
    float runTime = 0.2f;
    float Xdir = 0.2f;
    float Ydir = 0.2f;
    void Start()
    {
        myTransform = gameObject.transform.position;
        Xdir = UnityEngine.Random.Range(-0.2f, 0.3f);
        Ydir = UnityEngine.Random.Range(-0.2f, 0.3f);
        runTime = UnityEngine.Random.Range(0.1f, 0.3f);
    }
	void Update () {
        myTransform = gameObject.transform.position;
        myTransform.z = 0;
        gameObject.transform.position = myTransform;
        isEatFunc();
        BAF();
        if (gameObject.transform.position == new Vector3(-2f, 3.85f, 0.0f))
        {
            GlobalValue.instance.GetTypePower[elemenyType] += 1;
            Instantiate(ef_star, gameObject.transform.position, Quaternion.identity, gameObject.transform.parent);
            SoundControll.Instance.PlayEffecSound(SoundControll.Instance.getFoodinBot);
            Destroy(gameObject);
        }
        if (TurnControll.instance.turnState == TurnControll.TurnState.turnEnemy)
        {
            Destroy(gameObject);
        }
    }
    void isEatFunc()
    {
        if (isEat)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, new Vector3(-2f, 3.85f,0.0f), 8 * Time.deltaTime);
            Destroy(gameObject.GetComponent<CircleCollider2D>());
            Destroy(bubble.gameObject);
        }
        
    }
    void BAF()
    {
        if (runTime <= 0)
        {
            runTime = UnityEngine.Random.Range(0.1f, 0.3f);
            Xdir = -Xdir;
            Ydir = -Ydir;
        }
        else
        {
            runTime -= Time.deltaTime;
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, new Vector3(gameObject.transform.position.x+ Xdir, gameObject.transform.position.y , 0.0f), 0.0004f);
        }
    }
}
