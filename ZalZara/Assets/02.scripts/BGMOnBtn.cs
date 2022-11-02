using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMOnBtn : MonoBehaviour
{
    public GameObject gameObject;
    public GameObject gameObject2;
    public GameObject otherGameObject;
    public GameObject otherGameObject2;
    public GameObject audioGameObject1;
    public GameObject audioGameObject2;


    public AudioSource audioSource;
    public AudioSource otherAudioSource;

    public void GameObjectSetActive()
    {
        gameObject.SetActive(true);
        gameObject2.SetActive(true);

        otherGameObject.SetActive(false);
        otherGameObject2.SetActive(false);


        audioGameObject1.SetActive(true);
        audioGameObject2.SetActive(false);
    }

    /*    public void GameObjectUnsetActive()
        {
            gameObject.SetActive(false);
        }*/

    public void AudioPlay()
    {
        audioSource.Play();
        otherAudioSource.Play();

    }

    public void AudioStop()
    {
        audioSource.Stop();
        otherAudioSource.Stop();
    }



}
