using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderActions : MonoBehaviour {

    public Text[] Neurons;
    string[] neuroncountstartingtexts;
    public Text unittext;
    public Text randomtext;
    public Text hiddenLayerCounttext;
    GameObject[] layerSliders;
    public int[] hiddenLayerStructure;
    int maxCount = 5;

    void Start()
    {   
        layerSliders = GetLayerSliders();
        SetTexts();
        hiddenLayerStructure = new int[5];
        Info.Instance.hiddenLayerStructure = hiddenLayerStructure;
        HideLayerSlides(0);
    }

    void SetTexts()
    {
        Neurons = new Text[layerSliders.Length];
        hiddenLayerCounttext = GameObject.Find("Hidden Layer Count (Value)").GetComponent<Text>();
        randomtext = GameObject.Find("Random Value").GetComponent<Text>();
        unittext = GameObject.Find("Units COUNTER").GetComponent<Text>();
        neuroncountstartingtexts = new string[layerSliders.Length];
        for (int i = 0; i<neuroncountstartingtexts.Length; i++)
        {
            neuroncountstartingtexts[i] = layerSliders[i].GetComponentInChildren<Text>().text.ToString();
            Neurons[i] = layerSliders[i].GetComponentInChildren<Text>();
            Neurons[i].text = neuroncountstartingtexts[i] + " 1";
        }
    }
    // Update is called once per frame
    public void ChangeLayerCount (float layers) {
        hiddenLayerCounttext.text = "" + layers;
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
        //DebugStructure();
    }

    public void ChangeFirstLayerNeuronCount(float neurons)
    {
        hiddenLayerStructure[0] = (int)neurons;
        Neurons[0].text = neuroncountstartingtexts[0] + " " + neurons;
        //DebugStructure();
    }

    public void ChangeSecondLayerNeuronCount(float neurons)
    {
        hiddenLayerStructure[1] = (int)neurons;
        Neurons[1].text = neuroncountstartingtexts[1] + " " + neurons;
        //DebugStructure();
    }

    public void ChangeThirdLayerNeuronCount(float neurons)
    {
        hiddenLayerStructure[2] = (int)neurons;
        Neurons[2].text = neuroncountstartingtexts[2] + " " + neurons;
        //DebugStructure();
    }

    public void ChangeFourthLayerNeuronCount(float neurons)
    {
        hiddenLayerStructure[3] = (int)neurons;
        Neurons[3].text = neuroncountstartingtexts[3] + " " + neurons;
        //DebugStructure();
    }

    public void ChangeFifthLayerNeuronCount(float neurons)
    {
        Neurons[4].text = neuroncountstartingtexts[4] + " " + neurons;
        hiddenLayerStructure[4] = (int)neurons;
        //DebugStructure();
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
        this.unittext.text = "" + unitcount;
        Info.Instance.unitCount = unitcount;
    }

    public void SetStartRandomness(float factor)
    {
        Info.Instance.randomness = factor;
        randomtext.text = "" + factor;
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
