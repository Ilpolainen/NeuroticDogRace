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
        
        touched.GetComponentInChildren<IndividualInfo>().ClearStatus();
        touched.GetComponentInChildren<IndividualInfo>().status = 3;
        IndividualInfo childInfo = currentChild.GetComponent<IndividualInfo>();
        childInfo.parents.Add(touched.transform);
        touched.GetComponentInChildren<Selector>().SetTouchColor(Color.blue);
        Info.Instance.UpdateStatuses();
    }
}

