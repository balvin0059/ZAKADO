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
    public CatAttr.Statestuff[] stateSave = new CatAttr.Statestuff[10];
    public int exp;
    public int gold;
    public int[] catNum = new int[3];
    public bool[] level = new bool[6];
    public int enegy = 0;
    public DateTime recordTime;
}