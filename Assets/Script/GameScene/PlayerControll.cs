using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControll : MonoBehaviour {
    public string textMix;
    public Text hpText;
    public Image playerHp;
    public int playerMaxHp;
    public int playerNowHp;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        textMix = "HP " + playerNowHp.ToString() + " / " + playerMaxHp.ToString();
        hpText.text = textMix;
        if (playerNowHp > 0)
        {
            playerHp.fillAmount = (float)playerNowHp / (float)playerMaxHp;
        }
        else
        {
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
