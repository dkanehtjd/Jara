using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvenAndBuying : MonoBehaviour
{
    public GameObject[] animals = new GameObject[5];
    private void Start()
    {
        animals[0].SetActive(true);
    }
    // 활성화,비활성화 할 오브젝트
    public GameObject UI_BuyCharacter; // 뽑는 창
    public GameObject OpeningBox; //상자 열리는 창
    public GameObject UI_Collect;
    // 변수
    public int RandomInt; // 랜덤 변수

    private void Update()
    {
        RandomInt = Random.Range(1, 5); // 랜덤 범위를 설정합니다.
    }

    public void OneDraw() // 1회 뽑기 버튼을 클릭 시
    {
        
        if (RandomInt == 1) // RandomInt가 1이라면
        {

            animals[1].SetActive(true);
        }
        else if (RandomInt == 2)
        {
            animals[2].SetActive(true);
        }
        else if (RandomInt == 3)
        {
            animals[3].SetActive(true);
        }
        else if (RandomInt == 4)
        {
            animals[4].SetActive(true);
        }

        OpeningBox.SetActive(true); // 상자를 출력한 화면을 활성화합니다.
        UI_BuyCharacter.SetActive(false); //뽑기 창 닫기

        Invoke("CloseDraw", 2.0f); // 2초 뒤에 CloseDraw 함수를 실행합니다.
    }

    public void CloseDraw() // 뽑기 스크립트가 실행되고 자동으로 실행되게 합니다.
    {
        //DrawImage.sprite = null; // 적용했던 이미지를 초기화합니다.
        UI_BuyCharacter.SetActive(false); // 뽑기 창 닫기
        OpeningBox.SetActive(false); //상자 창 닫기
        UI_Collect.SetActive(true); //인벤토리 창 열기 
    }
    //뽑기버튼 -> 상자이펙트 2초후 -> 인벤토리

}