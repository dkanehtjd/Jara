using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIExitBtn : MonoBehaviour
{
    public GameObject gameObject;

    public void GameObjectUnsetActive()
    {
        gameObject.SetActive(false);
    }
}
