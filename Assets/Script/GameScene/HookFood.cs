using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookFood : MonoBehaviour {

    public Vector3 dir = Vector3.up;
    public float speed;
    public float backSpeed = 1;
    public float shootSpeed = 1f;
    public RectTransform gameObjectRect;
    public bool takeBake = false;
    public List<Vector3> transRecord;
    public GameObject dot;
    // Use this for initialization
    void Start()
    {
        InvokeRepeating("Creatdot", 0, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (speed * shootSpeed  < 1)
        {
            CancelInvoke("Creatdot");
        }//停止
        if (speed * shootSpeed * Time.deltaTime > 0)
        {
            if (shootSpeed > 0f)
            {
                shootSpeed -= Time.deltaTime * 0.8f;
            }
            else
            {
                shootSpeed = 0;
            }
            speed -= Time.deltaTime * 1.5f;
            transform.Translate(dir * speed * shootSpeed * Time.deltaTime);

        }//射出
        else
        {

            takeBake = true;
            speed = 0;
            if (backSpeed < 3.6f)
            {
                backSpeed += Time.deltaTime * 1.8f;
            }
            else
            {
                backSpeed = 3.6f;
            }
            if (gameObjectRect.position.y != -4)
            {
                if (transRecord.Count > 0)
                {
                    gameObjectRect.position = Vector3.MoveTowards(gameObjectRect.position, transRecord[transRecord.Count - 1], 4 * backSpeed * Time.deltaTime);
                    if (gameObjectRect.position == transRecord[transRecord.Count - 1])
                    {
                        transRecord.Remove(transRecord[transRecord.Count - 1]);
                    }
                }
                else
                {                    
                    gameObjectRect.position = Vector3.MoveTowards(gameObjectRect.position, new Vector2(0, -4f), 4 * backSpeed * Time.deltaTime);
                }
            }
            else
            {
                TurnControll.instance.turnState = TurnControll.TurnState.turnAttack;
                Time.timeScale = 1;
                Destroy(gameObject);
            }
        }//收回

    }
    float angle = 0;
    void flipUD()
    {
        angle = 0 - gameObject.transform.eulerAngles.z;
        transform.eulerAngles = new Vector3(0, 0, angle);
    }
    void flipWD()
    {
        angle = 180 - gameObject.transform.eulerAngles.z;
        transform.eulerAngles = new Vector3(0, 0, angle);
    }
    public void OnTriggerEnter2D(Collider2D n)
    {
        if (n.tag == "up" || n.tag == "down")
        {
            if (!takeBake)
            {
                Vector3 record = gameObjectRect.position;
                transRecord.Add(record);
            }
            flipWD();
        }
        if (n.tag == "left" || n.tag == "right")
        {
            if (!takeBake)
            {
                Vector3 record = gameObjectRect.position;
                transRecord.Add(record);
            }
            flipUD();
        }
        if (n.tag == "dot")
        {
            if (takeBake)
            {
                if (n.GetComponent<DotScript>().order >= transRecord.Count)
                {
                    Destroy(n.gameObject);
                }
            }
        }
        if (n.tag == "food")
        {
            SoundControll.Instance.PlayEffecSound(SoundControll.Instance.eatBubbleClip);
            n.GetComponent<FoodBubble>().isEat = true;
            TurnControll.instance.ComboGet(1);
        }
        if (n.tag == "food_special")
        {
            SoundControll.Instance.PlayEffecSound(SoundControll.Instance.eatBubbleClip);
            n.GetComponent<WebBubble>().isEat = true;
            if(n.GetComponent<WebBubble>().special_Type == 2)
            {
                Vector3 record = gameObjectRect.position;
                transRecord.Add(record);
            }
        }
    }
    public void OnTriggerExit2D(Collider2D n)
    {
        if (n.tag == "dot")
        {
            if (takeBake)
            {
                if (n.GetComponent<DotScript>().order >= transRecord.Count)
                {
                    Destroy(n.gameObject);
                }
            }
        }
    }
    void Creatdot()
    {
        GameObject d = Instantiate(dot, gameObject.transform.position, Quaternion.identity, gameObject.transform.parent);
        d.GetComponent<DotScript>().order = transRecord.Count;
        d.transform.eulerAngles = new Vector3(0, 0, angle);
    }
}
