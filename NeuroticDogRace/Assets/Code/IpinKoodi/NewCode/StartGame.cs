using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {

    public Transform prefab;
    GameObject armature;
	// Use this for initialization
	void Start () {
        FreezePrefabPosition();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void AddSensorsToPrefab()
    {
        armature = GameObject.FindGameObjectWithTag("PrefabArmature");
        GameObject head = GameObject.FindGameObjectWithTag("Head");
        
    }

    private void FreezePrefabPosition()
    {
        armature = GameObject.FindGameObjectWithTag("PrefabArmature");
        armature.SetActive(false);
    }

    public void startTheGame()
    {
        armature.SetActive(true);
        CleanNNStructure();
        SceneManager.LoadScene("TheGame");
    }

    void CleanNNStructure()
    {
        int[] hiddenStructure = Info.Instance.hiddenLayerStructure;
        int length = 0;
        foreach (int i in hiddenStructure)
        {
            if (i > 0)
            {
                length += 1;
            }

        }
        int[] newStructure = new int[length];
        for (int i=0; i<length; i++)
        {
            newStructure[i] = hiddenStructure[i];
        }
        Info.Instance.hiddenLayerStructure = newStructure;
    }
}
