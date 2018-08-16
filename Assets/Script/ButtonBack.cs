using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBack : MonoBehaviour {

    public void OnBack()
    {
        SaveLoadData.SaveData(GlobalValue.instance.gameSave);
        SceneManager.LoadScene("MainScene");
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GlobalValue.instance.gameSave.recordTime = DateTime.Now;
            SaveLoadData.SaveData(GlobalValue.instance.gameSave);
            SceneManager.LoadScene("MainScene");
        }
    }
}
