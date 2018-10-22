using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Runtime.InteropServices;
[StructLayout(LayoutKind.Sequential)]
public class GameSave
{
    public bool everSave;
    public bool[] everTeach = new bool[3];
    public bool[] catBuyYet = new bool[12];
    public int nowUnlockCat;
    public CatAttr.Statestuff[] stateSave = new CatAttr.Statestuff[12];
    public int exp;
    public int gold;
    public int nowStory;
    public int[] catNum = new int[3];
    public bool[] mission = new bool[3];
    public bool[] level = new bool[18];
    public int enegy = 0;
    public DateTime recordTime;
    public int[] item_id = new int[14];
    public bool[] item_Use = new bool[14];
    public int[] item_amount = new int[14];
    public int[] itemOrder = new int[14];
    public int item_all;

    public int[] quest_id = new int[6];
    public bool[] quest_complete = new bool[6];
    public bool[] quest_reward = new bool[6];

    public bool daliybonus;

    public bool FishAuto;
    public DateTime recordFishingTime;
    public int[] fishAmount = new int[3];
    public int[] fishBaitAmount = new int[3];
    public FishHolder.FishValue.RodQuality rodQuality;
    public FishHolder.FishValue.BaitQuality baitQuality;

    public bool[] storeCount = new bool[4];
}