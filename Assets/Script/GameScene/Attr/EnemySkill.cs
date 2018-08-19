using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySkill : MonoBehaviour
{
    public int Skill_id;
    public int Damage;
    public int Speed;
    
    void Skill(int id, int d, int s, GameObject gameObject, Transform[] transforms)
    {
        Skill_id = s - 3000;
        switch (Skill_id)
        {
            case 0:

                break;
            case 1:

                break;
            case 2:

                break;
            case 3:

                break;
            case 4:

                break;
            case 5:

                break;

            default:
                break;
        }
    }
}
