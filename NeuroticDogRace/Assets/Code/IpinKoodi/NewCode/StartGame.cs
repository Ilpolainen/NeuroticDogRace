using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NewCode;

public class StartGame : MonoBehaviour {

    public Transform prefab;
    GameObject armature;
    GameObject[] eyes;
    // Use this for initialization
    void Start () {
        GetEyes();
        prefab.GetComponentInChildren<MuscledBody>().ReRunStart();
        Info.Instance.inputSize = prefab.GetComponentInChildren<MuscledBody>().GetSensorInfo().Length;
        Info.Instance.outputSize = prefab.GetComponentInChildren<MuscledBody>().GetJoints().Length;
        FreezePrefabPosition();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public GameObject[] GetEyes()
    {
        eyes = GameObject.FindGameObjectsWithTag("Eye");
        if (eyes[0].name == "EyeR")
        {
            GameObject r = eyes[0];
            eyes[0] = eyes[1];
            eyes[1] = r;
        }

        return eyes;
    }

    private void AddSensorsToPrefab()
    {
        armature = GameObject.FindGameObjectWithTag("PrefabArmature");
    }

    private void FreezePrefabPosition()
    {
        armature = GameObject.FindGameObjectWithTag("PrefabArmature");
        armature.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void startTheGame()
    {
        armature.SetActive(true);
        CleanNNStructure();
        eyes = GetEyes();
        Info.Instance.eyeRotations = new Quaternion[eyes.Length];
        for (int i=0;i<eyes.Length;i++)
        {
            Info.Instance.eyeRotations[i] = eyes[i].GetComponent<Transform>().localRotation;
        }
        Info.Instance.unitStatuses = new int[Info.Instance.unitCount];
        Info.Instance.ClearStatusCounts();
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
