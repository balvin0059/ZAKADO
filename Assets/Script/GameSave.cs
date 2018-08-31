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
    public bool everTeach;
    public bool[] catBuyYet = new bool[10];
    public int nowUnlockCat;
    public CatAttr.Statestuff[] stateSave = new CatAttr.Statestuff[10];
    public int exp;
    public int gold;
    public int[] catNum = new int[3];
    public bool[] mission = new bool[2];
    public bool[] level = new bool[12];
    public int enegy = 0;
    public DateTime recordTime;
    public int[] item_id = new int[8];
    public bool[] item_Use = new bool[8];
    public int[] item_amount = new int[8];
    public int[] itemOrder = new int[8];
    public int item_all;

    public int[] quest_id = new int[5];
    public bool[] quest_complete = new bool[5];
}