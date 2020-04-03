using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoaderController : MonoBehaviour
{
    string _sceneName;
    public void OnTriggerStay(Collider other)
    {
        if(Input.GetButtonDown("Submit"))
        {
            switch(gameObject.name)
            {
                case "Game1Loader":
                    _sceneName = "Gravilota";
                    break;
                case "Game2Loader":
                    _sceneName = "Esencia";
                    break;
                case "Game3Loader":
                    _sceneName = "Canoncito";
                    break;
                case "Game4Loader":
                    _sceneName = "Gravilota";
                    break;
            }
            SceneManager.LoadScene(_sceneName);
        }
    }
}
