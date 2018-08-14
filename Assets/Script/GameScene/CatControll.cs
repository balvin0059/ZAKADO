using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatControll : MonoBehaviour {
    public GameObject heart;
    public int getPower;
    bool attacking = true;
    public CatAttr.Statestuff state;
    public GameObject enemyToDmg;
    public ElementType elementType = new ElementType();
    public ElementType.Element enemyType;
    // Use this for initialization
    void Start () {
        enemyToDmg = GameObject.Find("enemy");
    }
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
                StartCoroutine(AttackMove());
                attacking = false;
            }
        }
    }
    //攻擊動作
    IEnumerator AttackMove()
    {
        TurnControll.instance.turnState = TurnControll.TurnState.turnAttacking;
        //gameObject.transform.position += new Vector3(0.0f, 0.1f, 0.0f);
        gameObject.transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 100.0f, 0.0f), 40 * Time.deltaTime);
        CauseDamge(getPower);
        yield return new WaitForSeconds(0.5f);
        //gameObject.transform.position -= new Vector3(0.0f, 0.1f, 0.0f);
        gameObject.transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 0.0f, 0.0f), 40 * Time.deltaTime);
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
        enemyType = enemyToDmg.GetComponent<EnemyAttr>().enemy.type;
        float m = elementType.TypeResult(state.type, enemyType);
        enemyToDmg.SendMessage("GetDamage", (int)((float)state.attack * m * (float)gp));
    }
    public void SaveData()
    {
        Debug.Log("存檔");
        int uid = state.uid;
        PlayerPrefs.SetInt(uid.ToString() + "_uid", state.uid);
        PlayerPrefs.SetString(uid.ToString() + "_name", state.name);
        PlayerPrefs.SetInt(uid.ToString() + "_hp", state.hp);
        PlayerPrefs.SetInt(uid.ToString() + "_attack", state.attack);
        PlayerPrefs.SetInt(uid.ToString() + "_exp", state.exp);
        PlayerPrefs.SetInt(uid.ToString() + "_type", (int)state.type);
        PlayerPrefs.Save();
    }
    public void LoadData()
    {
        Debug.Log("讀檔");
        int uid = state.uid;
        state.uid = PlayerPrefs.GetInt(uid.ToString() + "_uid", state.uid);
        state.name = PlayerPrefs.GetString(uid.ToString() + "_name", state.name);
        state.hp = PlayerPrefs.GetInt(uid.ToString() + "_hp", state.hp);
        state.attack = PlayerPrefs.GetInt(uid.ToString() + "_attack", state.attack);
        state.exp = PlayerPrefs.GetInt(uid.ToString() + "_exp", state.exp);
        state.type = (ElementType.Element)PlayerPrefs.GetInt(uid.ToString() + "_type", (int)state.type);
    }
}
