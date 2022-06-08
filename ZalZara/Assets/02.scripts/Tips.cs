using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tips : MonoBehaviour
{
    public Text ScriptTxt;
    
    public Text TipTxt;
    

    // Start is called before the first frame update
    void Start()
    {
        //DB에 있는 수면 팁을 TipTxt변수에 넣어주셈
        //TipTxt.text = DB 데이터 텍스트;
        TipTxt.text = "여기 DB데이터 텍스트";
        ScriptTxt.text = TipTxt.text;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
