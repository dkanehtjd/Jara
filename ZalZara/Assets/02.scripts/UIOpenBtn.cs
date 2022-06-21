using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIOpenBtn : MonoBehaviour
{
    public GameObject gameObject;
    public GameObject otherGameObject1;
    public GameObject otherGameObject2;
    public GameObject otherGameObject3;


    public void GameObjectSetActive()
    {
        gameObject.SetActive(!gameObject.active);
        otherGameObject1.SetActive(false);
        otherGameObject2.SetActive(false);
        otherGameObject3.SetActive(false);
    }
}
