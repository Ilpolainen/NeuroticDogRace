using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Beginning : MonoBehaviour {

    public void Forward()
    {
        SceneManager.LoadScene("ConfigurationStage");
    }

    public void End()
    {
        Application.Quit();
    }
}
