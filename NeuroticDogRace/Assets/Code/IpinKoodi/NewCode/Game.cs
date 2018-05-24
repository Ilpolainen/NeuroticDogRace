using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Code;

public class Game : MonoBehaviour {


    public Action currentAction;
    public bool winnerFound;
    Text gameOver;
    Text debug;

    // Use this for initialization
    void Start () {
        debug = GameObject.Find("DebugText").GetComponentInChildren<Text>();
        if (Info.Instance == null)
        {
            debug.text= "No Info Created";
        } else
        {
            gameOver = GameObject.Find("Game Over").GetComponentInChildren<Text>();
            gameOver.text = " ";
        }
        ConstructActions();
        Info.Instance.game = this;
        
	}
	
	// Update is called once per frame
	
        
    void ConstructActions()
    {
        gameObject.AddComponent<SetAsChild>();
        gameObject.AddComponent<ChoosingParents>();
        gameObject.AddComponent<SetAsMutated>();
        currentAction =  gameObject.AddComponent<MarkDesposable>();
    }


    public Action GetAction()
    {
        return currentAction;
    }

    void CreateDummyInfo()
    {
        GameObject dummyInfo = new GameObject("DummyInfo");
        Info i = dummyInfo.AddComponent<Info>();
        i.academy = GameObject.Find("SceneCode").GetComponent<Academy>();
        i.prefab = i.academy.prefab;
        i.randomness = 3f;
        i.hiddenLayerStructure = i.prefab.GetComponent<Mind2>().hiddenStructure;
        i.game = this;
        i.targets = new List<GameObject>();
        i.unitCount = 1;
    }

    public void SetChildCommand()
    {
        currentAction = GetComponent<SetAsChild>();
    }

    public void SetWeakCommand()
    {
        currentAction = GetComponent<MarkDesposable>();
    }

    public void SetAsMutatedCommand ()
    {
        currentAction = GetComponent<SetAsMutated>();
    }

    public void SelectTarget()
    {
        foreach (Transform unit in Info.Instance.academy.units)
        {
            
        }
    }

    void GoalReached()
    {
        winnerFound = true;
    }

    void Update()
    {
        if (winnerFound)
        {
            RotateColors();
            ShowText();
        }
    }

    void RotateColors()
    {
        int time = Time.frameCount;
        if (time % 5 ==0)
        {
            Renderer[] renderers = Info.Instance.winner.GetComponentsInChildren<Renderer>();
            foreach (Renderer rend in renderers)
            {
                float red = UnityEngine.Random.value;
                float blue = UnityEngine.Random.value;
                float green = UnityEngine.Random.value;
                if (Mathf.Abs(red-green) < .4)
                {
                    green = green / 4;
                }
                Material[] mats = rend.materials;
                foreach (Material mat in mats)
                {
                    mat.color = new Color(red, green, blue);
                }
            }
        }  
    }
    public void PlaySound()
    {

    }
    void ShowText()
    {
        int time = Time.frameCount;
        if (time % 5 == 0)
        {
            float red = UnityEngine.Random.value;
            float blue = UnityEngine.Random.value;
            float green = UnityEngine.Random.value;
            if (Mathf.Abs(red - green) < .4)
            {
                green = green / 4;
            }
            gameOver.color = new Color(red, green, blue);
        }
        
        gameOver.text = "EVOLVEMENT!!!";
        
    }
}
