using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour  {

    public GameObject touched;

    void Start()
    {

    }
    
    public void SetTouched(GameObject go)
    {
        touched = go;
    }
}
