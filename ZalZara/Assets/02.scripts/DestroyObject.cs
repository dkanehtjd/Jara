using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    [SerializeField] GameObject obj;        //?????? ???????? ????

    //느낌표 이거 완전 파괴하는 거 마자?

    public void DestroyObj()
    {
        Destroy(obj);
    }
}
