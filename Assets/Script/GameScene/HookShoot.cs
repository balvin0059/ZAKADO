using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HookShoot : MonoBehaviour {
    public GameObject hooker;
    public Transform hookHolder;
    public GameObject coreBase;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (TurnControll.instance.turnState == TurnControll.TurnState.turnPlayer)
        {
            if (!TurnControll.instance.skillUseIng)
            {
                FireHook();
            }
        }
    }
    void FireHook()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (!TurnControll.instance.skillUseIng)
            {
                if (GlobalValue.instance.everTeach[0])
                {
                    if (coreBase.transform.GetComponent<CoreMove>().distance > 2.0f)
                    {
                        GameObject b = Instantiate(hooker);
                        b.transform.SetParent(hookHolder.transform, false);
                        b.transform.position = coreBase.transform.Find("CorePos").position;
                        b.transform.rotation = coreBase.transform.Find("CorePos").rotation;
                        b.transform.GetComponent<HookFood>().speed = coreBase.transform.GetComponent<CoreMove>().distance;
                        TurnControll.instance.turnState = TurnControll.TurnState.turnPlayerShoot;
                    }
                }
            }
        }
    }
}
