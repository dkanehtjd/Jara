using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMouseDown : MonoBehaviour
{
    public GameObject obj;

    void onMouseDown()
    {
        var trigger = FindObjectOfType<DialogueTrigger>();
        trigger.Trigger();
    }
}
