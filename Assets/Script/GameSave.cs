using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.Internal;
using UnityEngine.Scripting;
public class GameSave : MonoBehaviour
{    //單例模式
    public static GameSave instance;
    public static GameSave Instance
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
    public void SaveState(CatAttr.Statestuff c)
    {
        int uid = c.uid;
        PlayerPrefs.SetInt(uid.ToString() + "_uid", c.uid);
        PlayerPrefs.SetString(uid.ToString() + "_name", c.name);
        PlayerPrefs.SetInt(uid.ToString() + "_hp", c.hp);
        PlayerPrefs.SetInt(uid.ToString() + "_attack", c.attack);
        PlayerPrefs.SetInt(uid.ToString() + "_exp", c.exp);
        PlayerPrefs.SetInt(uid.ToString() + "_type", (int)c.type);
        PlayerPrefs.Save();
    }
    public CatAttr.Statestuff LoadState(CatAttr.Statestuff c)
    {
        int uid = c.uid;
        PlayerPrefs.GetInt(uid.ToString() + "_uid", c.uid);
        PlayerPrefs.GetString(uid.ToString() + "_name", c.name);
        PlayerPrefs.GetInt(uid.ToString() + "_hp", c.hp);
        PlayerPrefs.GetInt(uid.ToString() + "_attack", c.attack);
        PlayerPrefs.GetInt(uid.ToString() + "_exp", c.exp);
        PlayerPrefs.GetInt(uid.ToString() + "_type", (int)c.type);
        return c;
    }
}