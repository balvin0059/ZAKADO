using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatSkill{

    public void Skill(int id, ElementType.Element eType)
    {
        int sid = id - 1000;
        int intEtype = ((int)eType - 1);
        //Debug.Log(element);
        switch (sid)
        {
            case 0:
                TurnControll.instance.SpawnFood(intEtype, 5);
                break;
            case 1:
                TurnControll.instance.SpawnFood(intEtype, 5);
                break;
            case 2:
                TurnControll.instance.SpawnFood(intEtype, 5); 
                break;
            case 3:
                TurnControll.instance.SpawnSpecialFood(0);
                break;
            case 4:
                TurnControll.instance.SpawnSpecialFood(1);
                break;
            case 5:
                TurnControll.instance.SpawnSpecialFood(2);
                break;
            case 6:
                TurnControll.instance.ChangeFoodColor(intEtype);
                break;
            case 7:
                TurnControll.instance.ChangeFoodColor(intEtype);
                break;
            case 8:
                TurnControll.instance.ChangeFoodColor(intEtype);
                break;
        }
    }
}
