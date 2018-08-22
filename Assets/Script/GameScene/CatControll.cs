using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatControll : MonoBehaviour {
    public GameObject heart;
    public GameObject eff;
    public int getPower;
    public bool attacking = true;
    public CatAttr.Statestuff state;
    public GameObject enemyToDmg;
    public ElementType elementType = new ElementType();
    public ElementType.Element enemyType;
    public bool updown = true; // up = true down = false;
    public CatSkill catSkill = new CatSkill();
    void Awake()
    {
        LoadData();
        Debug.Log("我是" + state.name + "," + state.uid + "編號" + "/n" + "屬性:" + state.type);
    }
    void Start()
    {
    }
	//吃到飼料創造愛
    public void CreatHeart()
    {
        GameObject h = Instantiate(heart);
        h.transform.SetParent(gameObject.transform.parent, false);
        h.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y+1.5f, gameObject.transform.position.z);
        GameObject ef = Instantiate(eff);
        ef.transform.SetParent(gameObject.transform.parent, false);
        ef.transform.position = gameObject.transform.position;
    }
    //攻擊動作
    public void Attack()
    {        
        if (GlobalValue.instance.GetTypePower[(int)state.type-1] > 0)
        {            
            if (attacking)
            {
                if (updown)
                {
                    if (gameObject.transform.position.y < 1f)
                    {
                        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, new Vector3(transform.position.x, 1f, 0.0f), 5 * Time.deltaTime);
                    }
                    else if (gameObject.transform.position.y == 1f)
                    {
                        updown = false;
                    }
                }
                else if (!updown)
                {
                    if (gameObject.transform.position.y > 0.3f)
                    {
                        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, new Vector3(transform.position.x, 0.3f, 0.0f), 5 * Time.deltaTime);
                    }
                    else if (gameObject.transform.position.y == 0.3f)
                    {
                        TurnControll.instance.turnState = TurnControll.TurnState.turnAttacking;
                        CauseDamge(GlobalValue.instance.GetTypePower[(int)state.type - 1]);
                        StartCoroutine(AttackMove());
                        attacking = false;
                    }
                }
            }
        }
    }
    //攻擊動作
    IEnumerator AttackMove()
    {
        yield return new WaitForSeconds(3.0f);
        attacking = true;
        updown = true;
    }
    //重設
    void ResetGP()
    {
        attacking = true;
    }

    public void CauseDamge(int gp)
    {
        enemyToDmg = GameObject.FindWithTag("Enemy");
        enemyType = enemyToDmg.GetComponent<EnemyAttr>().enemy.type;
        float m = elementType.TypeResult(state.type, enemyType);
        enemyToDmg.SendMessage("GetDamage", (int)(state.attack * m * gp));
        enemyToDmg.SendMessage("GetElement", ((int)state.type-1));
    }
    public void LoadData()
    {
        Debug.Log("讀檔");

        GameSave gameSave = new GameSave();
        gameSave = SaveLoadData.LoadData();

        state = gameSave.stateSave[state.uid-1000];        
    }
    public void Skill(int id)
    {
        if (TurnControll.instance.turnState == TurnControll.TurnState.turnPlayer)
        {
            if (!TurnControll.instance.skillUseIng)
            {
                TurnControll.instance.SkillUse(id);
            }
        }
    }
}
