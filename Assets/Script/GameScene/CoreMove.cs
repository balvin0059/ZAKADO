using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoreMove : MonoBehaviour
{
    public Image coreImage;
    public Sprite shovel;
    public Sprite foodPlate;
    public Text bulletText;
    public GameObject CorePower;
    public GameObject shovelPower;
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
        bulletText.text = bullet.ToString();
        Move();
        Fire();
    }
    void Fire()
    {        
        if (shoot)
        {
            //玩家攻擊模式
            if (TurnControll.instance.turnState == TurnControll.TurnState.turnPlayer || TurnControll.instance.turnState == TurnControll.TurnState.turnPlayerShoot)
            {
                coreImage.sprite = foodPlate;
                if (bullet > 0)
                {
                    TurnControll.instance.turnState = TurnControll.TurnState.turnPlayerShoot;
                    ShootTime += Time.deltaTime;
                    if (ShootTime > 0.4f)
                    {
                        GameObject c = Instantiate(CorePower, new Vector2(gameObject.transform.position.x, -3.2f), Quaternion.identity);
                        c.transform.SetParent(gameObject.transform.parent, false);
                        c.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.5f, gameObject.transform.position.z);
                        bullet -= 1;
                        ShootTime = 0.0f;
                    }
                }else if (bullet == 0)
                {
                    TurnControll.instance.turnState = TurnControll.TurnState.turnAttack;
                    shoot = false;
                }
            }
            //玩家防守模式
            if (TurnControll.instance.turnState == TurnControll.TurnState.turnEnemyAttack)
            {
                coreImage.sprite = shovel;
                ShootTime += Time.deltaTime;
                if (ShootTime > 0.4f)
                {
                    GameObject s = Instantiate(shovelPower, new Vector2(gameObject.transform.position.x, -3.2f), Quaternion.identity);
                    s.transform.SetParent(gameObject.transform.parent, false);
                    s.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.5f, gameObject.transform.position.z);
                    ShootTime = 0.0f;
                }
            }            
        }
    }
    //移動核心
    void Move()
    {
        if (TurnControll.instance.turnState == TurnControll.TurnState.turnPlayer || TurnControll.instance.turnState == TurnControll.TurnState.turnPlayerShoot|| TurnControll.instance.turnState == TurnControll.TurnState.turnEnemyAttack)
        {
            //移動飼料罐罐
            if (move)
            {
                Vector2 CorePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, -3.95f);
                gameObject.transform.position = CorePosition;
            }
        }
        else
        {
            gameObject.transform.position = new Vector2(0.0f, -3.95f);
        }
    }

    #region 按鈕
    public void OnPressDown()
    {
        if (TurnControll.instance.turnState == TurnControll.TurnState.turnPlayer || TurnControll.instance.turnState == TurnControll.TurnState.turnPlayerShoot)
        {
            move = true;
            shoot = true;
        }
        if (TurnControll.instance.turnState == TurnControll.TurnState.turnEnemyAttack)
        {
            move = true;
            shoot = true;
        }
    }

    public void OnPressUp()
    {
        if (TurnControll.instance.turnState == TurnControll.TurnState.turnPlayer || TurnControll.instance.turnState == TurnControll.TurnState.turnPlayerShoot)
        {
            move = false;
        }
        if (TurnControll.instance.turnState == TurnControll.TurnState.turnEnemyAttack)
        {
            move = false;
        }
    }
    #endregion

    #region 控制子彈發射器
    //裝彈
    void ReloadBullet()
    {
        bullet = 10;
        move = false;
        shoot = false;
    }
    //開始
    void StartShoot()
    {
        shoot = true;
    }
    //停止
    void StopShoot()
    {
        shoot = false;
    }
    #endregion
}
