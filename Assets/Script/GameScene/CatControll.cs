using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatControll : MonoBehaviour {
    public GameObject heart;
    public int getPower;
    public bool attacking = true;
    public CatAttr.Statestuff state;
    public GameObject enemyToDmg;
    public ElementType elementType = new ElementType();
    public ElementType.Element enemyType;
    public bool updown = true; // up = true down = false;

	//吃到飼料創造愛心
    public void CreatHeart()
    {
        GameObject h = Instantiate(heart);
        h.transform.SetParent(gameObject.transform.parent, false);
        h.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y+1.5f, gameObject.transform.position.z);
    }
    //攻擊動作
    public void Attack()
    {        
        if (getPower > 0)
        {            
            if (attacking)
            {
                if (updown)
                {
                    if (gameObject.transform.position.y < 1f)
                    {
                        Debug.Log(gameObject.transform.position.y);
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
                        CauseDamge(getPower);
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
        yield return new WaitForSeconds(1.0f);
        attacking = true;
        updown = true;
    }
    //吃到飼料
    void GetPower(int gp)
    {
        getPower += gp;
    }
    //重設
    void ResetGP()
    {
        getPower = 0;
        attacking = true;
    }

    public void CauseDamge(int gp)
    {
        enemyToDmg = GameObject.FindWithTag("Enemy");
        enemyType = enemyToDmg.GetComponent<EnemyAttr>().enemy.type;
        float m = elementType.TypeResult(state.type, enemyType);
        enemyToDmg.SendMessage("GetDamage", (int)((float)state.attack * m * (float)gp));
    }
    public void LoadData()
    {
        Debug.Log("讀檔");

        GameSave gameSave = new GameSave();
        gameSave = SaveLoadData.LoadData();

        state = gameSave.stateSave[state.uid-1000];        
    }
}
