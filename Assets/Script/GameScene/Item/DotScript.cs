using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotScript : MonoBehaviour {
    public int order;
    private void Update()
    {
        if(TurnControll.instance.turnState == TurnControll.TurnState.turnAttack)
        {
            Destroy(gameObject);
        }
    }
}
