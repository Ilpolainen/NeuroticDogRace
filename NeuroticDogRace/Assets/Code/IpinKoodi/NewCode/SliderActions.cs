using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderActions : MonoBehaviour {

    GameObject[] layerSliders;
    public int[] hiddenLayerStructure;
    int maxCount = 5;

    void Start()
    {
        hiddenLayerStructure = new int[5];
        layerSliders = GetLayerSliders();
        HideLayerSlides(0);
    }
    // Update is called once per frame
    public void ChangeLayerCount (float layers) {
        for (int i = 0; i < layers; i++)
        {
            if (!layerSliders[i].activeSelf)
            {
                if (hiddenLayerStructure[i] < 0)
                {
                    hiddenLayerStructure[i] = Mathf.Abs(hiddenLayerStructure[i]);
                } else
                {
                    hiddenLayerStructure[i] = 1;
                }
                
            }
            layerSliders[i].SetActive(true);
            VisualizeRenderers(layerSliders[i],true);
        }
        HideLayerSlides((int)layers);
        // DebugStructure();
    }

    public void ChangeFirstLayerNeuronCount(float neurons)
    {
        hiddenLayerStructure[0] = (int)neurons;
        DebugStructure();
    }

    public void ChangeSecondLayerNeuronCount(float neurons)
    {
        hiddenLayerStructure[1] = (int)neurons;
        DebugStructure();
    }

    public void ChangeThirdLayerNeuronCount(float neurons)
    {
        hiddenLayerStructure[2] = (int)neurons;
        DebugStructure();
    }

    public void ChangeFourthLayerNeuronCount(float neurons)
    {
        hiddenLayerStructure[3] = (int)neurons;
        DebugStructure();
    }

    public void ChangeFifthLayerNeuronCount(float neurons)
    {
        hiddenLayerStructure[4] = (int)neurons;
        DebugStructure();
    }

    void HideLayerSlides(int from)
    {
        for (int i = from; i < maxCount; i++)
        {
            hiddenLayerStructure[i] = -Mathf.Abs(hiddenLayerStructure[i]);
            VisualizeRenderers(layerSliders[i], false);
            layerSliders[i].SetActive(false);
        }
    }

    void VisualizeRenderers(GameObject ob, bool show) 
    {
        Renderer[] rends = ob.transform.GetComponentsInChildren<Renderer>();
        foreach (Renderer rend in rends)
        {
            if (show)
            {
                rend.enabled = true;
            }
            else
            {
                rend.enabled = false;
            }
        }
    }

    GameObject[] GetLayerSliders()
    {
        GameObject[] sliders= new GameObject[maxCount];
        for (int i = 1; i<=maxCount; i++)
        {
            string sliderName = "Slider (" + i + ")";
            GameObject slider = GameObject.Find(sliderName);
            sliders[i - 1] = slider;
        }
        return sliders;
    }

    public void SetAcademySize(float units)
    {
        int unitcount = (int)units;
        Debug.Log(unitcount);
        Info.Instance.units = unitcount;
    }

    public void SetStartRandomness(float factor)
    {
        Info.Instance.randomness = factor;
    }

    void DebugStructure()
    {
        string structure = "";
        foreach (int value in hiddenLayerStructure)
        {
            structure = structure + "|" + value;
        }
        Debug.Log(structure);
    }
    
}
