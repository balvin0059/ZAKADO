using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnControll : MonoBehaviour {
    public bool turn;//0 = player 1 = enemy
    public RectTransform rect;
    public float move = 0.0f;
    //單例模式
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
    //
    [Serializable]
    public struct CatHolder
    {
        public GameObject cat_leader;
        public GameObject cat_teammate_0;
        public GameObject cat_teammate_1;
    }
    public enum TurnState
    {
        None = 0,
        turnPlayer,
        turnPlayerShoot,
        turnAttack,
        turnAttacking,
        turnEnemy,
        turnEnemyAttack
    }
    public CatHolder cat;
    public TurnState turnState;
    // Use this for initialization
    void Start () {
        turnState = TurnState.turnPlayer;

    }

    // Update is called once per frame
    void Update()
    {
        MoveTopHolder();
        if (turnState == TurnState.turnAttack)
        {
            StartCoroutine(TurnAttack());
        }
    }
    void MoveTopHolder()
    {
        if (turnState == TurnState.turnEnemy)
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
        }else if (turnState == TurnState.turnPlayer|| turnState == TurnState.turnPlayerShoot || turnState == TurnState.turnAttack || turnState == TurnState.turnAttacking)
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
    IEnumerator TurnAttack()
    {
            yield return new WaitForSeconds(0.4f);
            cat.cat_leader.SendMessage("Attack");
            yield return new WaitForSeconds(0.3f);
            cat.cat_teammate_0.SendMessage("Attack");
            yield return new WaitForSeconds(0.3f);
            cat.cat_teammate_1.SendMessage("Attack");
            yield return new WaitForSeconds(1.0f);
            turnState = TurnState.turnEnemy;
    }
}
