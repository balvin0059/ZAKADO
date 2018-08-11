using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreMove : MonoBehaviour
{
    public GameObject CorePower;
    bool move = false;
    bool shoot = false;
    int bullet = 10;
    float ShootTime = 0.0f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }
    void Fire()
    {
        if (shoot)
        {
            if (TurnControll.instance.turnState == TurnControll.TurnState.turnPlayer || TurnControll.instance.turnState == TurnControll.TurnState.turnPlayerShoot)
            {
                if (bullet > 0)
                {
                    TurnControll.instance.turnState = TurnControll.TurnState.turnPlayerShoot;
                    ShootTime += Time.deltaTime;
                    if (ShootTime > 0.4f)
                    {
                        Instantiate(CorePower, new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, -3.2f), Quaternion.identity);
                        bullet -= 1;
                        ShootTime = 0.0f;
                    }
                }else if (bullet <= 0)
                {
                    TurnControll.instance.turnState = TurnControll.TurnState.turnAttack;
                    shoot = false;
                }
            }
        }
    }
    void Move()
    {
        if (TurnControll.instance.turnState == TurnControll.TurnState.turnPlayer || TurnControll.instance.turnState == TurnControll.TurnState.turnPlayerShoot)
        {
            //移動飼料罐罐
            if (move)
            {
                Vector2 CorePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, -4.2f);
                gameObject.transform.position = CorePosition;
            }
        }
        else
        {
            gameObject.transform.position = new Vector2(0.0f, -4.2f);
        }
    }
    public void OnPressDown()
    {
        move = true;
        shoot = true;
    }
    public void OnPressUp()
    {
        move = false;
    }
}
