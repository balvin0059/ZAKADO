using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletAttr : MonoBehaviour {
    public bool boss = false;
    public int bulletLive;
    public int dmg;
    public float speed;
    public int sid;
    public float rotate;
    // Use this for initialization
    void Start () {
        gameObject.AddComponent<Ef_AutoRotate>().speed = 0.0f;
        if(sid == 3005)
        {
            boss = true;
            bulletLive = 10;
        }
    }
	
	// Update is called once per frame
	void Update () {
        Skill();
    }
    void Skill()
    {
        switch (sid - 3000)
        {
            case 0:
                gameObject.transform.Translate(0.0f, -speed, 0.0f);
                break;
            case 1:
                gameObject.transform.Translate(0.0f, -speed, 0.0f);
                if (gameObject.transform.localPosition.x >= 100f)
                {
                    gameObject.GetComponent<Ef_AutoRotate>().speed = -50f;
                }
                else
                {
                    gameObject.GetComponent<Ef_AutoRotate>().speed = 50f;
                }
                break;
            case 2:
                gameObject.transform.Translate(0.0f, -speed, 0.0f);
                if (gameObject.transform.localPosition.x >= 100f)
                {
                    gameObject.GetComponent<Ef_AutoRotate>().speed = -50f;
                }
                else
                {
                    gameObject.GetComponent<Ef_AutoRotate>().speed = 50f;
                }
                break;
            case 3:
                gameObject.transform.Translate(0.0f, -speed, 0.0f);
                if (gameObject.transform.localPosition.x >= 100f)
                {
                    gameObject.GetComponent<Ef_AutoRotate>().speed = -50f;
                }
                else
                {
                    gameObject.GetComponent<Ef_AutoRotate>().speed = 50f;
                }
                break;
            case 4:
                gameObject.transform.Translate(0.0f, -speed, 0.0f);
                break;
            case 5:
                rotate += Time.deltaTime*30;
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, new Vector3(gameObject.transform.position.x, -600, 0f), speed);
                gameObject.transform.rotation = Quaternion.Euler(0f, 0f, rotate);
                break;
            case 6:
                break;
            default:
                gameObject.transform.Translate(0.0f, -speed, 0.0f);
                break;
        }
    }

    public void OnTriggerEnter2D(Collider2D n)
    {
        if (n.tag == "Wall")
        {
            n.SendMessage("GetDamage", dmg);
            Destroy(gameObject);
        }
    }
    void GetBulletDamage(int i)
    {
        if (boss)
        {
            if (bulletLive > 0)
            {
                bulletLive -= i;
            }
            else
            {
                Destroy(gameObject);
            }
        } else
        {
            Destroy(gameObject);
        }
    }
}
