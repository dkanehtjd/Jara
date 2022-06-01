using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderBtn : MonoBehaviour
{
    public void SceneChange()
    {
        LoadingSceneController.LoadScene("main");


    }
}
