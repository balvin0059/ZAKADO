using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBack : MonoBehaviour {

    public void OnBack()
    {
        GlobalValue.instance.gameSave = SaveLoadData.LoadData();
        for (int i = 0; i < 3; i++)
        {
            GlobalValue.instance.catNum[i] = GlobalValue.instance.gameSave.catNum[i];
        }
        GlobalValue.instance.SaveAllData();
        SceneManager.LoadScene("MainScene");
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GlobalValue.instance.SaveAllData();
            Application.Quit();
        }
    }
    public void TurnOffPanel(GameObject g)
    {
        g.SetActive(false);
    }
}
