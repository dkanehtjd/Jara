using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
public class SignUpBtn : MonoBehaviour
{
    public void SceneChangeToLogIn()
    {
        SceneManager.LoadScene("LogIn");
    }

    public void SceneChangeToSignUp()
    {
        SceneManager.LoadScene("SignUp");
    }

    //로그인 페이지에서 회원가입버튼 -> 회원가입페이지
    //회원가입페이지에서 회원가입버튼 -> 로그인 페이p
    public void SceneChange()
    {
        if (SceneManager.GetActiveScene().name == "LogIn")
        {
            SceneChangeToSignUp();
        }
        else
        {
            SceneChangeToLogIn();
        }
    }

    // 회원가입 버튼 누르면 DB에 저장되게 해주세요

}
