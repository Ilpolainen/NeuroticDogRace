﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewCode
{

    public class Selector : MonoBehaviour
    {

        Transform parent;
        Renderer[] renderers;

        void Start()
        {
            
            parent = gameObject.transform.GetComponentsInParent<Transform>()[1];
            renderers = parent.GetComponentsInChildren<Renderer>();
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hitInfo = new RaycastHit();
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo) && hitInfo.transform.tag == "PrefabArmature")
                {
                    print("It's working");
                }
            }
        }

        void OnMouseDown()
        {
            SetTouchColor(Color.white);
            Debug.Log("Click!");
        }

        void OnMouseUp()
        {
            SetTouchColor(Color.red);
            Action action = Info.Instance.game.GetAction();
            action.SetTouched(parent.gameObject);
            action.Execute();
            
        }

        public void SetTouchColor(Color color)
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
}