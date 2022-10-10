using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIExitBtn : MonoBehaviour
{
    public GameObject gameObject;
    public AudioSource BtnAudio;

    public void GameObjectUnsetActive()
    {
        gameObject.SetActive(false);
        BtnAudio.Play();
    }
}
