using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NewCode
{

    public class LoadSceneOne : MonoBehaviour {

        public void LoadMenu()
        {
            Info.Instance.game.winnerFound = false;
            SceneManager.LoadScene("ConfigurationStage");
        }
    }

}