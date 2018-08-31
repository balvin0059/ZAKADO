using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class QuestAttr {

    public MissionSystem questAttr;
    public int id;
    public bool isComplete;

    public QuestAttr(MissionSystem questAttr, int id)
    {
        this.questAttr = questAttr;
        this.id = id;
    }
}
