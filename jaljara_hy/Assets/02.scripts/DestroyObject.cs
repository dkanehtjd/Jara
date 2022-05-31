using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    [SerializeField] GameObject obj;        //삭제할 오브젝트 선언


    public void DestroyObj()
    {
        Destroy(obj);
    }
}
