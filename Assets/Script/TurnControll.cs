using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnControll : MonoBehaviour {
    public bool turn;//0 = player 1 = enemy
    public RectTransform rect;
    public float move = 0.0f;

    public static TurnControll instance;
    public static TurnControll Instance
    {
        get
        {
            return instance;
        }
    }
    private void Awake()
    {
        instance = this;
    }
    // Use this for initialization
    void Start () {

	}

    // Update is called once per frame
    void Update()
    {
        MoveTopHolder();
    }
    void MoveTopHolder()
    {
        if (!turn)
        {
            if (move < 3.5f)
            {
                move += Time.deltaTime * 4;
            }
            else
            {
                move = 3.5f;
            }
            rect.position = new Vector3(0.0f, move, 0.0f);
        }else if (turn)
        {
            if (move > 0.0f)
            {
                move -= Time.deltaTime * 4;
            }
            else
            {
                move = 0.0f;
            }
            rect.position = new Vector3(0.0f, move, 0.0f);
        }
    }
}
