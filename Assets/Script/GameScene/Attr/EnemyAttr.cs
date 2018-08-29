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
        public float speed;
        public float attackDelay;
        public ElementType.Element type;// 1 = fire, 2 = water, 3 = lighting
        public int exp;
        public int gold;
        public string skill_name;
        public int skill_id;
    }
    public Image[] e;
    public Transform[] genPositions;
    public GameObject bullet;
    public Statestuff enemy;
    public Image hpBar;
    public int maxHp;
    public Color c;
    public int limitMax = 10;
    public int limit = 10;
    public int bulletend;
    // Use this for initialization
    void Start () {
        bulletend = limitMax;
        maxHp = enemy.hp;
        e[2].sprite = GlobalValue.instance.elementHolder[(int)enemy.type-1];
    }
	void GetDamage(int dmg)
    {
        Debug.Log("敵人受到了"+dmg+"點傷害");
        enemy.hp -= dmg;
    }
    void GetElement(int e)
    {
        GameObject g = Instantiate(GlobalValue.instance.effectHolder[e]);
        g.transform.SetParent(gameObject.transform.transform, false);
        g.transform.position = gameObject.transform.position;
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
    void TurnEnemyAttack()
    {
        InvokeRepeating("Spawn", 0, enemy.attackDelay);
    }
    void TurnEnemyAttackEnd()
    {
        CancelInvoke("Spawn");
    }
    void Spawn()
    {
        limit -= 1;
        int a = UnityEngine.Random.Range(0, 3);
        GameObject b = Instantiate(bullet);
        b.transform.SetParent(gameObject.transform.parent, false);
        b.transform.localPosition = genPositions[a].localPosition;
        b.GetComponent<EnemyBulletAttr>().dmg = enemy.attack;
        b.GetComponent<EnemyBulletAttr>().speed = enemy.speed;
        b.GetComponent<EnemyBulletAttr>().sid = enemy.skill_id;
    }
    void Reset()
    {
        limit = limitMax;
        bulletend = limitMax;
    }
}
