using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Creat GameStuff/ Mission Attribute")]
public class MissionSystem : ScriptableObject
{ 

    public string Desciption;

    public string missionName;
    public string missionInfo;

    public Sprite icon;

    public int missionNum;
    public int missionRewardID;
    public bool isComplete;
    public bool isReward;
    public bool isFinish;

    public MissionType missionType;

    public enum MissionType
    {
        None = 0
    }
}
