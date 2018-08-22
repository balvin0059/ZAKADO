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
        public ElementType.Element type; // 0 = fire, 1 = water, 2 = lighting
        public string actives;
        public string actives_info;
        public int actives_id;
        public int actives_cost;
    }
    // Use this for initialization
    void Start () {
    }
}
