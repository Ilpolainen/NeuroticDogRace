﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NewCode;

public class SetAsChild : Action {

	// Use this for initialization
	void Start () {
		
	}

    public override void Execute()
    {
        IndividualInfo individualInfo = this.touched.GetComponentInChildren<IndividualInfo>();
        individualInfo.ClearStatus();
        individualInfo.status = 2;
        Info.Instance.game.GetComponentInChildren<ChoosingParents>().currentChild = touched.transform;
        touched.GetComponentInChildren<Selector>().SetTouchColor(Color.magenta);
        Info.Instance.game.currentAction = Info.Instance.game.GetComponentInChildren<ChoosingParents>();
        Info.Instance.UpdateStatuses();
    }
}
