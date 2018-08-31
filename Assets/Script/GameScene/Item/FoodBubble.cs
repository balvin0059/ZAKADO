using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodBubble : MonoBehaviour {
    public GameObject ef_star;
    public int elemenyType;
    public GameObject bubble;
    public bool isEat = false;
    public Vector3 myTransform;
    void Start()
    {
        myTransform = gameObject.transform.position;
    }
	void Update () {
        myTransform = gameObject.transform.position;
        myTransform.z = 0;
        gameObject.transform.position = myTransform;
        isEatFunc();
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

}
