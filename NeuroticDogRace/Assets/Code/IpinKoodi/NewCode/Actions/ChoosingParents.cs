using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NewCode;

public class ChoosingParents : Action {

    public Transform currentChild;

    void Start()
    {
            
    }

    public override void Execute()
    {
        if (currentChild == null)
        {
            return;
        }
        this.touched.GetComponentInChildren<IndividualInfo>().isParent = true;
        IndividualInfo childInfo = this.currentChild.GetComponent<IndividualInfo>();
        childInfo.parent = this.touched.transform;
        touched.GetComponentInChildren<Selector>().SetTouchColor(Color.blue);
    }
}

