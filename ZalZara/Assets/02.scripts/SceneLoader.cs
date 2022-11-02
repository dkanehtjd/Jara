using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour
{
/*    public void ToLoginScene()
    {
        SceneManager.LoadScene("Login");
    }
*/
    public void ToLodingScene()
    {
        SceneManager.LoadScene("LoadingScene");
        LoadingSceneController.LoadScene("RewardScenes");
    }

    public void ToMainScene()
    {
        SceneManager.LoadScene("main");
    }

    public void ToSurveyScene()
    {
        SceneManager.LoadScene("SurveyScene");
    }

    public void ToSleepDataScene()
    {
        SceneManager.LoadScene("SleepDataScene");
    }
}
