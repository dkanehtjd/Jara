using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvenAndBuying : MonoBehaviour
{
    public GameObject[] animals = new GameObject[5];

    // 보유 포인트
    int Point = 1030;
    // 표시 포인트
    public Text ScriptTxt;

    public GameObject Sheep; //1번 hexagon
    public GameObject BtnSheep; //miniMapSheepBtn
    public GameObject LockSheep; //miniMapSheepLock
    public GameObject Tiger; //2번 hexagon
    public GameObject BtnTiger; //miniMapTigerBtn
    public GameObject LockTiger; //miniMapTigerLock
    public GameObject Camel; //3번 hexagon
    public GameObject BtnCamel; //miniMapCamelBtn
    public GameObject LockCamel; //miniMapCamelLock
    public GameObject Duck; //4번 hexagon
    public GameObject BtnDuck; //miniMapDuckBtn
    public GameObject LockDuck; //miniMapDuckLock

    private void Start()
    {
        animals[0].SetActive(true);
    }

    // 활성화,비활성화 할 오브젝트
    public GameObject UI_BuyCharacter; // 뽑는 창
    public GameObject OpeningBox; //상자 열리는 창
    public GameObject UI_Collect; //인벤토리 창

    // 헥사곤 게임오브젝트
    //private GameObject Sheep = GameObject.Find("hexagon_sheep");
    //private GameObject Tiger = GameObject.Find("hexagon_tiger");
    //private GameObject Camel = GameObject.Find("hexagon_camel");
    //private GameObject Duck = GameObject.Find("hexagon_duck");




    // 변수
    public int RandomInt; // 랜덤 변수

    private void Update()
    {
        RandomInt = Random.Range(1, 5); // 랜덤 범위를 설정합니다.
    }

    public void OneDraw() // 1회 뽑기 버튼을 클릭 시
    {
        Point -= 300;
        ScriptTxt.text = Point.ToString();
        if (RandomInt == 1) // RandomInt가 1이라면
        {
            Invoke("ActiveSheep", 2.0f);
            animals[1].SetActive(true);
            BtnSheep.SetActive(true);
            LockSheep.SetActive(false);

        }
        else if (RandomInt == 2)
        {
            Invoke("ActiveTiger", 2.0f);
            animals[2].SetActive(true);
            BtnTiger.SetActive(true);
            LockTiger.SetActive(false);
        }
        else if (RandomInt == 3)
        {
            Invoke("ActiveCamel", 2.0f);
            animals[3].SetActive(true);
            BtnCamel.SetActive(true);
            LockCamel.SetActive(false);
        }
        else if (RandomInt == 4)
        {
            Invoke("ActiveDuck", 2.0f);
            animals[4].SetActive(true);
            BtnDuck.SetActive(true);
            LockDuck.SetActive(false);
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

    private void ActiveSheep()
    {
        Sheep.SetActive(true);
    }

    private void ActiveTiger()
    {
        Tiger.SetActive(true);
    }

    private void ActiveCamel()
    {
        Camel.SetActive(true);
    }

    private void ActiveDuck()
    {
        Duck.SetActive(true);
    }

}