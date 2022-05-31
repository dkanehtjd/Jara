using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIOpenBtn : MonoBehaviour
{
    public GameObject gameObject;

    public void GameObjectSetActive()
    {
        gameObject.SetActive(!gameObject.active);
    }
}
