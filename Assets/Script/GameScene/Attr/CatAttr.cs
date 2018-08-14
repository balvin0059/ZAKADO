using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class CatAttr {
    [Serializable]
    public struct Statestuff
    {
        public int uid;
        public string name;
        public int lv;
        public int hp;
        public int attack;
        public int exp;
        public ElementType.Element type; // 0 = fire, 1 = water, 2 = lighting
        public string passives;
        public int passives_id;
        public string actives;
        public int actives_id;
    }
    // Use this for initialization
    void Start () {
    }
}
