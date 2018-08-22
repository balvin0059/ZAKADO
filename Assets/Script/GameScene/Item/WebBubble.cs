using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebBubble : MonoBehaviour {
    public int special_Type; // web = 0, live = 1, angle = 2, multiple = 3
    public Animator anim;
    public GameObject bubble;
    public bool isEat = false;
    public Vector3 myTransform;
    void Start()
    {
        myTransform = gameObject.transform.position;
    }
    void Update()
    {
        if (TurnControll.instance.turnState == TurnControll.TurnState.turnEnemy)
        {
            Destroy(gameObject);
        }
        myTransform = gameObject.transform.position;
        myTransform.z = 0;
        gameObject.transform.position = myTransform;
        isEatFunc();
    }
    void isEatFunc()
    {
        switch (special_Type)
        {
            case 0:
                Web_Bubble();
                break;
            case 1:
                Live_Bubble();
                break;
            case 2:
                Turn_Bubble();
                break;
            case 3:
                Turn_Bubble();
                break;
            default:
                break;
        }
    }
    public void Web_Bubble()
    {
        if (isEat)
        {
            if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("end"))
            {
                Destroy(gameObject);
            }
            anim.speed = 1;
            if (gameObject.GetComponent<CircleCollider2D>().radius < 1.4f)
            {
                gameObject.GetComponent<CircleCollider2D>().radius += Time.deltaTime * 1.7f;
            }
            Destroy(bubble.gameObject);
        }
        else
        {
            anim.speed = 0;
        }
    }
    public void Live_Bubble()
    {
        if (isEat)
        {
            if (gameObject.transform.position == new Vector3(-1.62f, -4.55f, 0.0f))
            {
                int sum = PlayerControll.instance.playerNowHp += PlayerControll.instance.playerMaxHp / 10;
                PlayerControll.instance.playerNowHp = (sum >= PlayerControll.instance.playerMaxHp) ? PlayerControll.instance.playerMaxHp : sum;
                Destroy(gameObject);
            }
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, new Vector3(-1.62f, -4.55f, 0.0f), 8 * Time.deltaTime);
            Destroy(bubble.gameObject);
        }
    }
    public void Turn_Bubble()
    {
        if (isEat)
        {
            gameObject.GetComponent<Ef_AutoRotate>().speed = 0;
            GameObject h = GameObject.FindWithTag("hook");
            h.transform.GetComponent<HookFood>().speed += 3.0f;
            h.transform.rotation = gameObject.transform.rotation;
            Destroy(gameObject);
        }
    }
    public void OnTriggerEnter2D(Collider2D n)
    {
        if (isEat)
        {
            if (n.tag == "food")
            {
                n.GetComponent<FoodBubble>().isEat = true;
            }
        }
    }
    public void OnTriggerStay2D(Collider2D n)
    {
        if (isEat)
        {
            if (n.tag == "food")
            {
                n.GetComponent<FoodBubble>().isEat = true;
            }
        }
    }
}
