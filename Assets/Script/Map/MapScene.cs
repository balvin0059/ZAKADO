using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapScene : MonoBehaviour {
    public GameObject noenegyPanel;
    public GameObject confirmenegyPanel;
    public Text enegyText;
    public GameObject[] levelHolder;
    public GameObject[] clearArea;
    void Start()
    {
        clearArea[0].SetActive(GlobalValue.instance.level[0]);
        for (int i = 1; i < GlobalValue.instance.level.Length; i++)
        {
            if (GlobalValue.instance.level[i - 1])
            {
                levelHolder[i].SetActive(GlobalValue.instance.level[i - 1]);
                clearArea[i].SetActive(GlobalValue.instance.level[i]);
            }
            else
            {
                break;
            }
        }
    }
	public void OnLevel(int l)
    {
        if (GlobalValue.instance.enegy - 5 < 0)
        {
            noenegyPanel.SetActive(true);
        }
        else
        {
            GlobalValue.instance.nowLevel = l;
            string str = GlobalValue.instance.enegy.ToString() + " -> " + (GlobalValue.instance.enegy - 5).ToString();
            enegyText.text = str;
            confirmenegyPanel.SetActive(true);
        }
    }
    public void OnConfirmPanel()
    {
        GlobalValue.instance.enegy -= 5;
        GlobalValue.instance.dateTime_next = GlobalValue.instance.dateTime_next.AddMinutes(25);
        SceneManager.LoadScene("GameScene");
    }
    public void OnNoEnegyPanel()
    {
        noenegyPanel.SetActive(false);
        //or something ads
    }
}
