using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NewCode;

public class SetAsMutated : Action {

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    public override void Execute()
    {
        IndividualInfo indInfo = touched.GetComponentInChildren<IndividualInfo>();
        indInfo.ClearStatus();
        indInfo.status = 4;
        touched.GetComponentInChildren<Selector>().SetTouchColor(Color.cyan);
        Info.Instance.UpdateStatuses();
    }
}
