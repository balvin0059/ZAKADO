using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapScene : MonoBehaviour {
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
        GlobalValue.instance.nowLevel = l;
        GlobalValue.instance.enegy -= 5;
        GlobalValue.instance.dateTime_next = GlobalValue.instance.dateTime_next.AddMinutes(25);
        Debug.Log("剩餘"+ GlobalValue.instance.dateTime_next.Minute+ "分鐘回滿體力");
        SceneManager.LoadScene("GameScene");
    }
}
