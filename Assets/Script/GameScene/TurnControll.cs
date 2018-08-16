using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TurnControll : MonoBehaviour {    
    //單例模式
    public static TurnControll instance;
    public static TurnControll Instance
    {
        get
        {
            return instance;
        }
    }
    private void Awake()
    {
        instance = this;
    }
    //
    [Serializable]
    public struct CatHolder
    {
        public GameObject[] catgenPos;
        public GameObject cat_leader;
        public GameObject cat_teammate_0;
        public GameObject cat_teammate_1;
    }
    [Serializable]
    public struct EnemyHolder
    {
        public GameObject enemyHolder;
        public GameObject[] spawn;
        public GameObject enemyGameobject;
    }
    public enum TurnState
    {
        None = 0,
        turnPlayer,
        turnPlayerShoot,
        turnAttack,
        turnAttacking,
        turnEnemy,
        turnEnemyAttack,
        turnEnd,
        turnFinish
    }
    public enum TurnResult
    {
        None = 0,
        turnWin,
        turnLose
    }

    public GameObject gaveLosePanel;
    public Text expText;
    public Text goldText;
    public GameObject gaveWinPanel;
    public GameObject playerHp;
    public GameObject Core;
    public CatHolder cat;
    public EnemyHolder enemy;
    public TurnState turnState;
    public TurnResult turnResult;
    bool onlyOneTime = true;
    public RectTransform rect;
    public float move = 0.0f;
    public RectTransform top;

    // Use this for initialization
    void Start () {
        turnState = TurnState.turnPlayer;

        BaseSet();
    }

    // Update is called once per frame
    void Update()
    {
        MoveTopHolder();
        if (turnState == TurnState.turnAttack)
        {
            StartCoroutine(TurnAttack());
        }
        if (turnState == TurnState.turnEnemy)
        {
            if (rect.position == top.position)
            {
                StartCoroutine(TurnEnemy());
            }
        }
        if (turnState == TurnState.turnEnemyAttack)
        {
        }
        if(turnState == TurnState.turnEnd)
        {
            if (rect.position == new Vector3(0f, 0f, 0f))
            {
                StartCoroutine(TurnEnd());                
            }
        }
        if(turnState == TurnState.turnFinish)
        {
            StartCoroutine(GameFinish(turnResult));            
        }
        
    }
    void MoveTopHolder()
    {
        if (turnState == TurnState.turnEnemy)
        {
            rect.position = Vector3.MoveTowards(rect.position, top.position, 80 * Time.deltaTime);
        }
        if (turnState == TurnState.turnPlayer|| turnState == TurnState.turnPlayerShoot || turnState == TurnState.turnAttack || turnState == TurnState.turnAttacking || turnState == TurnState.turnEnd)
        {
            rect.position = Vector3.MoveTowards(rect.position, new Vector3(0f,0f,0f), 80 * Time.deltaTime);
        }
    }
    IEnumerator TurnAttack()
    {
        yield return new WaitForSeconds(0.5f);
        cat.cat_leader.SendMessage("Attack");
        yield return new WaitForSeconds(0.5f);
        cat.cat_teammate_0.SendMessage("Attack");
        yield return new WaitForSeconds(0.5f);
        cat.cat_teammate_1.SendMessage("Attack");
        yield return new WaitForSeconds(1.5f);
        if (turnState != TurnState.turnFinish)
        {
            turnState = TurnState.turnEnemy;
        }
    }
    IEnumerator TurnEnemy()
    {
        yield return new WaitForSeconds(2.0f);
        if (onlyOneTime)
        {
            InvokeRepeating("TurnEnemyAttack", 0, 0.6f);
            onlyOneTime = false;
            turnState = TurnState.turnEnemyAttack;
            Core.SendMessage("StartShoot");
        }
        yield return new WaitForSeconds(5.0f);
        CancelInvoke("TurnEnemyAttack");
        yield return new WaitForSeconds(1f);
        Core.SendMessage("StopShoot");
        yield return new WaitForSeconds(1f);
        turnState = TurnState.turnEnd;
    }
    IEnumerator TurnEnd()
    {
        onlyOneTime = true;
        Core.SendMessage("ReloadBullet");
        cat.cat_leader.SendMessage("ResetGP");
        cat.cat_teammate_0.SendMessage("ResetGP");
        cat.cat_teammate_1.SendMessage("ResetGP");
        turnState = TurnState.turnPlayer;
        yield return new WaitForSeconds(2.0f);
    }
    void TurnEnemyAttack()
    {
        enemy.enemyGameobject.SendMessage("Spawn");
    }
    int HPGet()
    {
        int a = cat.cat_leader.GetComponent<CatControll>().state.hp;
        int b = cat.cat_teammate_0.GetComponent<CatControll>().state.hp;
        int c = cat.cat_teammate_1.GetComponent<CatControll>().state.hp;

        return a + b + c;        
    }
    IEnumerator GameFinish(TurnResult t)
    {
        yield return new WaitForSeconds(2.0f);
        Time.timeScale = 0f;
        if(t == TurnResult.turnLose)
        {
            gaveLosePanel.SetActive(true);
        }
        if(t == TurnResult.turnWin)
        {
            expText.text = "獲得經驗值:"+enemy.enemyGameobject.GetComponent<EnemyAttr>().enemy.exp.ToString();
            GlobalValue.instance.exp += enemy.enemyGameobject.GetComponent<EnemyAttr>().enemy.exp;
            goldText.text = "獲得金幣:" + enemy.enemyGameobject.GetComponent<EnemyAttr>().enemy.gold.ToString();
            GlobalValue.instance.gold += enemy.enemyGameobject.GetComponent<EnemyAttr>().enemy.gold;
            gaveWinPanel.SetActive(true);
            GlobalValue.instance.level[GlobalValue.instance.nowLevel] = true;
            GlobalValue.instance.gameSave.level[GlobalValue.instance.nowLevel] = GlobalValue.instance.level[GlobalValue.instance.nowLevel];
            SaveLoadData.SaveData(GlobalValue.instance.gameSave);
        }
    }
    public void OnOver()
    {
        GlobalValue.instance.gameSave.gold = GlobalValue.instance.gold;
        GlobalValue.instance.gameSave.exp = GlobalValue.instance.exp;
        SaveLoadData.SaveData(GlobalValue.instance.gameSave);
        Time.timeScale = 1f;
        turnResult = TurnResult.None;
        turnState = TurnState.None;
        SceneManager.LoadScene("MapScene");
    }
    void BaseSet()
    {
        cat.cat_leader = Instantiate(GlobalValue.instance.catHolder[GlobalValue.instance.catNum[0]-1000]);
        cat.cat_leader.transform.SetParent(cat.catgenPos[0].transform, false);
        cat.cat_leader.GetComponent<CatControll>().LoadData();
        cat.cat_teammate_0 = Instantiate(GlobalValue.instance.catHolder[GlobalValue.instance.catNum[1] - 1000]);
        cat.cat_teammate_0.transform.SetParent(cat.catgenPos[1].transform, false);
        cat.cat_teammate_0.GetComponent<CatControll>().LoadData();
        cat.cat_teammate_1 = Instantiate(GlobalValue.instance.catHolder[GlobalValue.instance.catNum[2] - 1000]);
        cat.cat_teammate_1.transform.SetParent(cat.catgenPos[2].transform, false);
        cat.cat_teammate_1.GetComponent<CatControll>().LoadData();

        playerHp.GetComponent<PlayerControll>().SetMaxHp(HPGet());
        playerHp.GetComponent<PlayerControll>().GetHp(HPGet());

        enemy.enemyGameobject = Instantiate(GlobalValue.instance.enemyHolder[GlobalValue.instance.nowLevel]);
        enemy.enemyGameobject.transform.SetParent(enemy.enemyHolder.transform, false);
    }
}