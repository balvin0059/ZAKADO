using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreMove : MonoBehaviour
{
    public GameObject CorePower;
    bool move = false;
    bool shoot = false;
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
            if (TurnControll.Instance.turn)
            {
                ShootTime += Time.deltaTime;
                if (ShootTime > 0.4f)
                {
                    Instantiate(CorePower, new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, -3.2f), Quaternion.identity);
                    ShootTime = 0.0f;
                }
            }
        }
    }
    void Move()
    {
        //移動飼料罐罐
        if (move)
        {
            Vector2 CorePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, -4.2f);
            gameObject.transform.position = CorePosition;
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
        shoot = false;
    }
}
