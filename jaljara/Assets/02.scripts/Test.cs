using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Test : MonoBehaviour
{

    [SerializeField] GameObject obj;        //삭제할 오브젝트 선언

    public void DestroyObj()
    {
        Destroy(obj);
    }

}