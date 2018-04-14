using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NewCode;

public class MarkDesposable : Action {

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    public override void Execute()
    {
        IndividualInfo individualInfo = this.touched.GetComponentInChildren<IndividualInfo>();
        individualInfo.isParent = false;
        individualInfo.isDisposable = true;
        touched.GetComponentInChildren<Selector>().SetTouchColor(Color.gray);
    }
}
