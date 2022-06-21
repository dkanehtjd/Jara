using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    public Text txtName;
    public Text txtSentence;
    public GameObject gameObject;

    Queue<string> sentences = new Queue<string>();

    public void Begin(Dialogue info)
    {
/*        sentences.Clear();
*/
        txtName.text = info.name;

        foreach(var sentence in info.sentences)
        {
            sentences.Enqueue(sentence);
        }
        Next();
    }

    public void Next()
    {

        if(sentences.Count==0)
        {
            End();
            return;
        }
        txtSentence.text = sentences.Dequeue();
    }

    public void End()
    {
        //밑에 이거 왜 필요?
        //txtSentence.text = string.Empty;
        //UI OFF
        gameObject.SetActive(!gameObject.active);
    }
}
