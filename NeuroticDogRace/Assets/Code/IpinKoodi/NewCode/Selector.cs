using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour {

    GameState state;
    Renderer[] renderers;

    void Start()
    {
        state = null;
        renderers = gameObject.transform.GetComponentsInParent<Transform>()[1].GetComponentsInChildren<Renderer>();
    }

    void OnMouseDown(){
        //state.SetTouched(gameObject);
        SetTouchColor(Color.blue);
    }

    void OnMouseUp()
    {
        SetTouchColor(Color.red);
    }

    void SetTouchColor(Color color)
    {
        foreach (Renderer rend in renderers)
        {
            Material[] mats = rend.materials;
            foreach (Material mat in mats)
            {
                mat.color = color;
            }
        }
    }
}
