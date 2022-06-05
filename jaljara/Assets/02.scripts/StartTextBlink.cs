using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StartTextBlink : MonoBehaviour
{
    Text flashingText; // Use this for initialization
    void Start()
    {
        flashingText = GetComponent<Text>();
        StartCoroutine(BlinkStartText());
    }

    public IEnumerator BlinkStartText()
    {
        while (true)
        {
            flashingText.text = "";
            yield return new WaitForSeconds(1f);
            flashingText.text = "Touch to Start";
            yield return new WaitForSeconds(1f);
        }
    }
}

