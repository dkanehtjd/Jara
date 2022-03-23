using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneController : MonoBehaviour
{
    //변수 선언
    static string nextScene;


    //진행바로 사용될 이미지를 담을 변수 선언
    [SerializeField]
    Image progressBar;

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName; //매개변수에 대입
        SceneManager.LoadScene("LoadingScene");//씬매니저로 로딩씬 불러옴

    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadSceneProcess());
    }

    //코루틴으로 로드씬 프로세스 함수 만들기
    IEnumerator LoadSceneProcess()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);//로드씬 함수에서 받아둔 nextScene을 매개변수로 
                                                                   //SceneManager.LoadScene : 동기방식 씬 불러오기 - 씬을 불러오기전 까지 다른 작업 수행 불가
                                                                   //SceneManager.LoadSceneAsync : 비동기방식

        //AsyncOperation 클래스 객체 op를 통해  allowSceneActivation을 false로 변경 
        //비동기 방식에서 씬의 로딩이 끝나면 자동으로 불러온 씬으로 이동할 것인지 설정 
        //false : 90%까지 로딩한 상태에서 다음 씬으로 넘어가지 않고 대기 (true가 될 때 까지 대기)
        //  1.생각보다 로딩 속도가 빠를 때 (속도 조절:페이크 로딩)
        //  2. 로딩화면에서 씬 외에 다른 것을 불러와야할 때 (리소스 로딩 대기)
        op.allowSceneActivation = false;


        float timer = 0f;
        //씬 로딩이 끝나지 않았다면 계속 반복
        while (!op.isDone)
        {
            yield return null;//반복될 때 마다 유니티에 제어권이 넘어가게함
            //하지 않으면 반복문이 끝나기 전에 화면이 갱신되지 않아 진행바가 차오르지 않음

            if (op.progress < 0.9f)
            {
                progressBar.fillAmount = op.progress;//로딩바 차오르게
            }
            else
            {
                //페이크로딩 진행
                timer += Time.unscaledDeltaTime;
                progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);
                if (progressBar.fillAmount >= 1f)//로딩 끝나면
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}

