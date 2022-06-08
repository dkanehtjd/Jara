using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    // 동물 보유 여부 DB랑 잘 해보세요
    // 인벤토리에 넣을 동물 9마리
    public GameObject[] animals = new GameObject[9];

    public void GameObjectSetActive()
    {
        // 4, 5번 째 동물 가지고 있지 않음 인벤토리에서 안보이게
        animals[4].SetActive(!animals[4].active);
        animals[5].SetActive(!animals[5].active);
    }

    private void Start()
    {
        GameObjectSetActive();
    }
}
