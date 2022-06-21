using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardCtr : MonoBehaviour
{
    public Text ScriptTxt;
    public int point = 1400;


    void Start()
    {//디비랑 연동해서 잘 해보세요
        ScriptTxt.text = point.ToString();
    }
}
