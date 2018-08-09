using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAttr : MonoBehaviour {
    [Serializable]
    public struct Statestuff
    {
        public int uid;
        public string name;
        public int hp;
        public int attack;
        public int exp;
        public string passives;
        public int passives_id;
        public string actives;
        public int actives_id;
    }
    public Statestuff state;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
