using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControll : MonoBehaviour {
    #region 單例模式
    public static PlayerControll instance;
    public static PlayerControll Instance
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

    public string textMix;
    public Text hpText;
    public Image playerHp;
    public int playerMaxHp;
    public int playerNowHp;

    public string textEPMix;
    public Text epText;
    public Image playerEp;
    public int playerMaxEP;
    public int playerNowEP;
    // Use this for initialization
    void Start () {
        playerMaxEP = 20;
        playerNowEP = GlobalValue.instance.playerEnegyPower;
    }
	
	// Update is called once per frame
	void Update ()
    {
        textMix = "HP " + playerNowHp.ToString() + "/" + playerMaxHp.ToString();
        textEPMix = "EP " + playerNowEP.ToString() + "/" + playerMaxEP.ToString();
        hpText.text = textMix;
        epText.text = textEPMix;
        playerEp.fillAmount = (float)playerNowEP / (float)playerMaxEP;
        if (playerNowEP > playerMaxEP)
        {
            playerNowEP = playerMaxEP;
        }
            if (playerNowHp > 0)
        {
            playerHp.fillAmount = (float)playerNowHp / (float)playerMaxHp;
        }
        else
        {
            playerNowHp = 0;
            playerHp.fillAmount = 0f / (float)playerMaxHp;
            TurnControll.instance.turnState = TurnControll.TurnState.turnFinish;
            TurnControll.instance.turnResult = TurnControll.TurnResult.turnLose;
        }
	}
    public void SetMaxHp(int a)
    {
        playerMaxHp = a;
    }
    public void GetHp(int a)
    {
        playerNowHp = a;
    }
    public void GetDamage(int value)
    {
        playerNowHp -= value;
    }
}
