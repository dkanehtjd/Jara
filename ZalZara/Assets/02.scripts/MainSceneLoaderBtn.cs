using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneLoaderBtn : MonoBehaviour
{
    public void SceneChange()
    {
        SceneManager.LoadScene("main");
    }
}
