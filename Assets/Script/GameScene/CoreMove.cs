using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CoreMove : MonoBehaviour
{
    public GameObject tipLine;
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

    #region 玩家回合需要變數
    public RectTransform UGUICanvas;//宣告一個canvas
    public Camera miancamera;//宣告一個camera
    public Vector3 mousePos_1;//紀錄按下去的POS
    public Vector3 mousePos_2;//紀錄移動中的POS
    public Vector2 directionVector;//計算角度DELTA POS
    public float distance;//計算距離來影響速度
    public float z;//角度存入器
    #endregion

    // Update is called once per frame
    void Update()
    {
        bulletText.text = bullet.ToString();
        if (TurnControll.instance.turnState == TurnControll.TurnState.turnPlayer)
        {
            tipLine.transform.localPosition = new Vector3(1, 15*distance, 1);
            tipLine.transform.localScale = new Vector3(1, 2*distance, 1);
            coreImage.sprite = foodPlate;
            Hook();
        }
        else if (TurnControll.instance.turnState == TurnControll.TurnState.turnAttack)
        {
            ResetGun();
        }
        else if (TurnControll.instance.turnState == TurnControll.TurnState.turnPlayerShoot)
        {
            tipLine.transform.localPosition = new Vector3(0, 0, 0);
            tipLine.transform.localScale = new Vector3(0, 0, 0);
        }
        Move();
        Fire();
    }
    void Fire()
    {
        if (TurnControll.instance.turnState == TurnControll.TurnState.turnEnemyAttack)
        {
            coreImage.sprite = shovel;
            if (shoot)
            {
                coreImage.sprite = shovel;
                ShootTime += Time.deltaTime;
                if (ShootTime > 0.3f)
                {
                    GameObject s = Instantiate(shovelPower, new Vector2(gameObject.transform.position.x, -3.8f), Quaternion.identity);
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
        if (TurnControll.instance.turnState == TurnControll.TurnState.turnEnemyAttack)
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

    #region 按鈕
    public void OnPressDown()
    {
        if (TurnControll.instance.turnState == TurnControll.TurnState.turnEnemyAttack)
        {
            move = true;
            shoot = true;
        }
    }

    public void OnPressUp()
    {
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
        z = 0;
        distance = 0;
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

    void Hook()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RectTransformUtility.ScreenPointToWorldPointInRectangle(UGUICanvas, new Vector2(Input.mousePosition.x, Input.mousePosition.y), miancamera, out mousePos_1);
        }
        if (Input.GetMouseButton(0))
        {
            RectTransformUtility.ScreenPointToWorldPointInRectangle(UGUICanvas, new Vector2(Input.mousePosition.x, Input.mousePosition.y), miancamera, out mousePos_2);
        }
        directionVector = mousePos_1 - mousePos_2;
        distance = directionVector.magnitude * 1.7f;
        distance = distance > 10 ? 10 : distance;
        z = GetAngle(mousePos_1, mousePos_2);
        z = z > 80 ? 80 : z;
        z = z < -80 ? 80 : z;
        transform.localRotation = Quaternion.Euler(0, 0, z);
    }

    #region 角度計算器
    float GetAngle(Vector2 a, Vector2 b)
    {
        if (a.x == b.x && a.y >= b.y) return 0;
        float angle = Mathf.Acos(-b.y / b.magnitude) * (180 / Mathf.PI);
        return (b.x < 0 ? -angle : angle);
    }
    #endregion

    void ResetGun()
    {
        z = 0;
        distance = 0;
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        mousePos_1 = new Vector3(0, 0, 0);
        mousePos_2 = new Vector3(0, 0, 0);
        directionVector = new Vector2(0, 0);
    }
}
