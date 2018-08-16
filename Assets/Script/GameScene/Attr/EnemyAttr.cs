using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAttr : MonoBehaviour {
    [Serializable]
    public struct Statestuff
    {
        public int uid;
        public string name;
        public int hp;
        public int attack;
        public ElementType.Element type;// 0 = fire, 1 = water, 2 = lighting
        public int exp;
        public int gold;
        public string Ai_name;
        public int Ai_id;
    }
    public Image[] e;
    public Transform[] genPositions;
    public GameObject bullet;
    public Statestuff enemy;
    public Image hpBar;
    public int maxHp;
    public Color c;
    // Use this for initialization
    void Start () {
        maxHp = enemy.hp;
    }
	void GetDamage(int dmg)
    {
        Debug.Log("敵人受到了"+dmg+"點傷害");
        enemy.hp -= dmg;
    }
    void Update()
    {
        if (enemy.hp > 0)
        {
            hpBar.fillAmount = (float)enemy.hp / (float)maxHp;
        }
        else
        {
            c.a -= Time.deltaTime;
            for (int n = 0; n < e.Length; n++)
            {
                e[n].color = c;
            }
            hpBar.fillAmount = 0f / (float)maxHp;
            TurnControll.instance.turnState = TurnControll.TurnState.turnFinish;
            TurnControll.instance.turnResult = TurnControll.TurnResult.turnWin;
        }
    }
    void Spawn()
    {
        int a = UnityEngine.Random.Range(0, 3);
        GameObject b = Instantiate(bullet);
        b.transform.SetParent(gameObject.transform.parent, false);
        b.transform.localPosition = genPositions[a].localPosition;
        b.GetComponent<EnemyBulletMove>().dmg = enemy.attack;
    }
}
