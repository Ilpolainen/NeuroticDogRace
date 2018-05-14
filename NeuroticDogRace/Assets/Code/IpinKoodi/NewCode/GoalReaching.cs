using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NewCode;

public class GoalReaching : MonoBehaviour {

    public Game game; 
    // Use this for initialization
    void OnTriggerEnter(Collider other)
    {
        Transform ancestor = other.gameObject.transform;
        while (ancestor.gameObject.GetComponentsInParent<Transform>().Length > 1)
        {
            ancestor = ancestor.GetComponentsInParent<Transform>()[1];
        }
        Info.Instance.winner = ancestor;
        game.winnerFound = true;
        game.PlaySound();
    }
}
