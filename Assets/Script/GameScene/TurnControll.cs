using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TurnControll : MonoBehaviour {
    #region 單例模式
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
    #endregion

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

    public GameObject[] food;
    public GameObject[] food_special;
    public GameObject foodHolder;
    [Header("面板存取")]
    public GameObject enemyTurnPanel;
    public GameObject enemyTurnTextPanel;
    public GameObject playerTurnPanel;
    public GameObject playerTurnTextPanel;
    public GameObject gaveLosePanel;
    public GameObject gaveWinPanel;
    public GameObject SkillPanel;
    public GameObject SkillnoEpPanel;
    public GameObject TeachPanel;
    public Image PanelTopBg;
    public Image PanelBotBg;
    public Image PanelBg;

    public Text[] skillText;//0 腳色名稱 1 技能名稱 2 技能說明 3 技能消耗EP
    public Text expText;
    public Text goldText;
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
    public bool attacking = false;
    private bool isSpawnFood = false;
    public CatSkill catskill = new CatSkill();
    public int skillIdUse;
    public bool skillUseIng = false;

    public GameObject comboPanel;
    public int comboNum;
    public Text comboText;
    public int clearAmount;
    public GameObject clearPanel;
    // Use this for initialization
    void Start () {
        SoundControll.Instance.ChangeBgSound(SoundControll.Instance.Bg_02);
        turnState = TurnState.turnPlayer;
        SpawnFood();
        playerTurnPanel.SetActive(true);
        BaseSet();
        if (GlobalValue.instance.everTeach)
        {
            Destroy(TeachPanel.gameObject);
        }
        else
        {
            TeachPanel.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        MoveTopHolder();
        if (turnState == TurnState.turnAttack)
        {
            comboPanel.SetActive(false);
            clearPanel.SetActive(false);
            StartCoroutine(TurnAttack());
            enemy.enemyGameobject.SendMessage("Reset");
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
            if(enemy.enemyGameobject.GetComponent<EnemyAttr>().limit <= 0)
            {
                enemy.enemyGameobject.SendMessage("TurnEnemyAttackEnd");
                enemy.enemyGameobject.GetComponent<EnemyAttr>().limit = 0;
            }
            if (enemy.enemyGameobject.GetComponent<EnemyAttr>().bulletend <= 0)
            {
                enemy.enemyGameobject.GetComponent<EnemyAttr>().bulletend = 0;
                turnState = TurnState.turnEnd;
            }
            isSpawnFood = false;
        }
        if(turnState == TurnState.turnEnd)
        {
            if (rect.position == new Vector3(0f, 0f, 0f))
            {
                if (!isSpawnFood)
                {                    
                    SpawnFood();                    
                }
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
            PanelTopBg.gameObject.SetActive(false);
            rect.position = Vector3.MoveTowards(rect.position, top.position, 80 * Time.deltaTime);
        }
        if (turnState == TurnState.turnPlayer|| turnState == TurnState.turnPlayerShoot || turnState == TurnState.turnAttack || turnState == TurnState.turnAttacking || turnState == TurnState.turnEnd)
        {
            if (rect.position == new Vector3(0f, 0f, 0f))
            { PanelTopBg.gameObject.SetActive(true); }
            else
            { rect.position = Vector3.MoveTowards(rect.position, new Vector3(0f, 0f, 0f), 80 * Time.deltaTime); }
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
            enemyTurnPanel.SetActive(true);
            enemyTurnTextPanel.SetActive(true);
            enemyTurnPanel.SendMessage("AlphaReset");
            enemyTurnTextPanel.SendMessage("AlphaReset");
        }
    }
    IEnumerator TurnEnemy()
    {
        yield return new WaitForSeconds(2.0f);
        if (onlyOneTime)
        {
            enemy.enemyGameobject.SendMessage("TurnEnemyAttack");
            onlyOneTime = false;
            turnState = TurnState.turnEnemyAttack;
        }
    }
    IEnumerator TurnEnd()
    {
        onlyOneTime = true;
        Core.SendMessage("ReloadBullet");
        cat.cat_leader.SendMessage("ResetGP");
        cat.cat_teammate_0.SendMessage("ResetGP");
        cat.cat_teammate_1.SendMessage("ResetGP");
        turnState = TurnState.turnPlayer;
        playerTurnPanel.SetActive(true);
        playerTurnTextPanel.SetActive(true);
        playerTurnPanel.SendMessage("AlphaReset");
        playerTurnTextPanel.SendMessage("AlphaReset");
        cat.cat_leader.GetComponent<CatControll>().clear = 1f;
        cat.cat_teammate_0.GetComponent<CatControll>().clear = 1f;
        cat.cat_teammate_1.GetComponent<CatControll>().clear = 1f;
        for (int i = 0; i < GlobalValue.instance.GetTypePower.Length; i++)
        {
            GlobalValue.instance.GetTypePower[i] = 0;
        }
        yield return new WaitForSeconds(2.0f);
    }

    void TurnEnemyAttack()
    {
        enemy.enemyGameobject.SendMessage("Spawn");
    }

    public void SkillUse(int id)
    {
        if (GlobalValue.instance.everTeach)
        {
            SoundControll.Instance.PlayEffecSound(SoundControll.Instance.buttonClip);
            skillUseIng = true;
            skillIdUse = id;
            skillText[0].text = GlobalValue.instance.catHolder[skillIdUse].GetComponent<CatControll>().state.name;
            skillText[1].text = GlobalValue.instance.catHolder[skillIdUse].GetComponent<CatControll>().state.actives;
            skillText[2].text = GlobalValue.instance.catHolder[skillIdUse].GetComponent<CatControll>().state.actives_info;
            skillText[3].text = GlobalValue.instance.catHolder[skillIdUse].GetComponent<CatControll>().state.actives_cost.ToString();
            SkillPanel.SetActive(true);
        }
    }
    public void ConfirmSkillUse()
    {
        if (PlayerControll.instance.playerNowEP >= GlobalValue.instance.catHolder[skillIdUse].GetComponent<CatControll>().state.actives_cost)
        {
            SoundControll.Instance.PlayEffecSound(SoundControll.Instance.buttonClip);
            catskill.Skill(GlobalValue.instance.catHolder[skillIdUse].GetComponent<CatControll>().state.uid, GlobalValue.instance.catHolder[skillIdUse].GetComponent<CatControll>().state.type);
            PlayerControll.instance.playerNowEP -= GlobalValue.instance.catHolder[skillIdUse].GetComponent<CatControll>().state.actives_cost;
            SkillPanel.SetActive(false);
            skillIdUse = 0;
            skillUseIng = false;
        }
        else
        {
            SoundControll.Instance.PlayEffecSound(SoundControll.Instance.cantdoClip);
            SkillPanel.SetActive(false);
            SkillnoEpPanel.SetActive(true);
        }
    }

    public void TurnOffSkillPanel()
    {
        SoundControll.Instance.PlayEffecSound(SoundControll.Instance.buttonClip);
        skillIdUse = 0;
        skillUseIng = false;
        SkillPanel.SetActive(false);
        SkillnoEpPanel.SetActive(false);
    }

    #region 製造飼料相關
    public void SpawnFood()
    {
        isSpawnFood = true;
        comboNum = 0;
        clearAmount = 0;
        int rForFood = UnityEngine.Random.Range(15, 20);
        int rForSFood = UnityEngine.Random.Range(1, 3);
        int index = UnityEngine.Random.Range(0, 3);
        float area_h = UnityEngine.Random.Range(-2.5f, 2.5f);
        float area_w = UnityEngine.Random.Range(-4.0f, -0.5f);
        for (int i = 0; i < rForSFood + GlobalValue.instance.itemSBubbleBufferAmount; i++)
        {
            index = UnityEngine.Random.Range(0, 3);
            area_h = UnityEngine.Random.Range(-2.5f, 2.5f);
            area_w = UnityEngine.Random.Range(-4.0f, -0.5f);
            Instantiate(food_special[index], new Vector3(area_h, area_w, 0.0f), Quaternion.identity, foodHolder.transform);
        }
        
        for (int i = 0; i < rForFood + GlobalValue.instance.itemNBubbleBufferAmount; i++)
        {
            index = UnityEngine.Random.Range(0, 3);
            area_h = UnityEngine.Random.Range(-2.5f, 2.5f);
            area_w = UnityEngine.Random.Range(-4.0f, -0.5f);
            clearAmount += 1;
            Instantiate(food[index], new Vector3(area_h, area_w, 0.0f), Quaternion.identity, foodHolder.transform);
        }
    }
    public void SpawnFood(int eType, int num)
    {
        for (int i = 0; i < num; i++)
        {
            float area_h = UnityEngine.Random.Range(-2.5f, 2.5f);
            float area_w = UnityEngine.Random.Range(-4.0f, -0.5f);
            clearAmount += 1;
            Instantiate(food[eType], new Vector3(area_h, area_w, 0.0f), Quaternion.identity, foodHolder.transform);
        }
    }
    public void SpawnSpecialFood()
    {
        int index = UnityEngine.Random.Range(0, 3);
        float area_h = UnityEngine.Random.Range(-2.5f, 2.5f);
        float area_w = UnityEngine.Random.Range(-4.0f, -0.5f);
        Instantiate(food_special[index], new Vector3(area_h, area_w, 0.0f), Quaternion.identity, foodHolder.transform);

    }
    public void SpawnSpecialFood(int sType)
    {
        float area_h = UnityEngine.Random.Range(-2.5f, 2.5f);
        float area_w = UnityEngine.Random.Range(-4.0f, -0.5f);
        Instantiate(food_special[sType], new Vector3(area_h, area_w, 0.0f), Quaternion.identity, foodHolder.transform);
    }
    public void ChangeFoodColor(int eType)
    {
        GameObject[] f = GameObject.FindGameObjectsWithTag("food");
        for(int i = 0; i < f.Length; i++)
        {
            Instantiate(food[eType], f[i].transform.position, Quaternion.identity, foodHolder.transform);
            Destroy(f[i].gameObject);
        }
    }
    #endregion

    IEnumerator GameFinish(TurnResult t)
    {
        yield return new WaitForSeconds(2.0f);
        Time.timeScale = 0f;
        if(t == TurnResult.turnLose)
        {
            SoundControll.Instance.PlayEffecSound(SoundControll.Instance.youLose);
            gaveLosePanel.SetActive(true);
        }
        if(t == TurnResult.turnWin)
        {
            SoundControll.Instance.PlayEffecSound(SoundControll.Instance.youWin);
            expText.text = enemy.enemyGameobject.GetComponent<EnemyAttr>().enemy.exp.ToString();
            GlobalValue.instance.exp += enemy.enemyGameobject.GetComponent<EnemyAttr>().enemy.exp;
            goldText.text = enemy.enemyGameobject.GetComponent<EnemyAttr>().enemy.gold.ToString();
            GlobalValue.instance.gold += enemy.enemyGameobject.GetComponent<EnemyAttr>().enemy.gold;
            gaveWinPanel.SetActive(true);
            GlobalValue.instance.level[GlobalValue.instance.nowLevel] = true;
            if(GlobalValue.instance.nowLevel == 5)
            {
                GlobalValue.instance.mission[GlobalValue.instance.nowMission] = true;
                GlobalValue.instance.gameSave.mission[GlobalValue.instance.nowMission] = GlobalValue.instance.mission[GlobalValue.instance.nowMission];
            }
            GlobalValue.instance.gameSave.level[GlobalValue.instance.nowLevel] = GlobalValue.instance.level[GlobalValue.instance.nowLevel];
            CheckQuest();
            GlobalValue.instance.SaveAllData();
            
        }
    }
    public void OnOver()
    {
        SoundControll.Instance.PlayEffecSound(SoundControll.Instance.buttonClip);
        for (int i = 0; i < GlobalValue.instance.GetTypePower.Length; i++)
        {
            GlobalValue.instance.GetTypePower[i] = 0;
        }
        GlobalValue.instance.gameSave.gold = GlobalValue.instance.gold;
        GlobalValue.instance.gameSave.exp = GlobalValue.instance.exp;
        GlobalValue.instance.SaveAllData();
        Time.timeScale = 1f;
        turnResult = TurnResult.None;
        turnState = TurnState.None;
        SceneManager.LoadScene("MapScene");
    }
    public void OnTeachPanel(GameObject g)
    {
        SoundControll.Instance.PlayEffecSound(SoundControll.Instance.buttonClip);
        GlobalValue.instance.everTeach = true;
        Destroy(g.gameObject);
    }

    public void ComboGet(int a)
    {
        comboNum += a;
        comboText.text = comboNum.ToString();
        comboPanel.SetActive(true);
        ClearCombo();
    }
    void ClearCombo()
    {
        if(comboNum == clearAmount)
        {
            SoundControll.Instance.PlayEffecSound(SoundControll.Instance.clearClip);
            comboPanel.SetActive(false);
            clearPanel.SetActive(true);
            cat.cat_leader.GetComponent<CatControll>().clear = 1.5f;
            cat.cat_teammate_0.GetComponent<CatControll>().clear = 1.5f;
            cat.cat_teammate_1.GetComponent<CatControll>().clear = 1.5f;
            Instantiate(GlobalValue.instance.effectHolder[3], clearPanel.transform.position, Quaternion.identity, clearPanel.transform);
        }
    }
    void CheckQuest()
    {
        if(cat.cat_leader.GetComponent<CatControll>().state.type == ElementType.Element.Fire &&  cat.cat_teammate_0.GetComponent<CatControll>().state.type == ElementType.Element.Fire &&  cat.cat_teammate_1.GetComponent<CatControll>().state.type == ElementType.Element.Fire)
        {
            QuestHolder.instance.quest[0].questAttr.isComplete = true;
        }
        if (cat.cat_leader.GetComponent<CatControll>().state.type == ElementType.Element.Water && cat.cat_teammate_0.GetComponent<CatControll>().state.type == ElementType.Element.Water && cat.cat_teammate_1.GetComponent<CatControll>().state.type == ElementType.Element.Water)
        {
            QuestHolder.instance.quest[1].questAttr.isComplete = true;
        }
        if (cat.cat_leader.GetComponent<CatControll>().state.type == ElementType.Element.Lighting && cat.cat_teammate_0.GetComponent<CatControll>().state.type == ElementType.Element.Lighting && cat.cat_teammate_1.GetComponent<CatControll>().state.type == ElementType.Element.Lighting)
        {
            QuestHolder.instance.quest[2].questAttr.isComplete = true;
        }
        if(GlobalValue.instance.nowLevel == 5)
        {
            QuestHolder.instance.quest[3].questAttr.isComplete = true;
        }
        if (GlobalValue.instance.nowLevel == 11)
        {
            QuestHolder.instance.quest[4].questAttr.isComplete = true;
        }
    }
    #region 初始化
    void BaseSet()
    {
        PanelBg.sprite = GlobalValue.instance.missionMainBg[GlobalValue.instance.nowMission];
        PanelBotBg.sprite = GlobalValue.instance.missionBotBg[GlobalValue.instance.nowMission];
        PanelTopBg.sprite = GlobalValue.instance.missionTopBg[GlobalValue.instance.nowMission];

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
    int HPGet()
    {
        int a = cat.cat_leader.GetComponent<CatControll>().state.hp;
        int b = cat.cat_teammate_0.GetComponent<CatControll>().state.hp;
        int c = cat.cat_teammate_1.GetComponent<CatControll>().state.hp;

        return a + b + c;
    }
    #endregion


}