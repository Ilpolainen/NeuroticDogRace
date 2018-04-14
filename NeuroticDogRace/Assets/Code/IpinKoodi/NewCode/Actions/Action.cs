using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour  {

    public GameObject touched;

    void Start()
    {

    }
    
    public void SetTouched(GameObject go)
    {
        touched = go;
    }

    public virtual void Execute()
    {

    }
}
