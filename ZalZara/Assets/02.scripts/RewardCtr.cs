using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardCtr : MonoBehaviour
{
    public Text ScriptTxt;

    void Start()
    {//디비랑 연동해서 잘 해보세요
        ScriptTxt.text = "100만포인트";
    }
}
