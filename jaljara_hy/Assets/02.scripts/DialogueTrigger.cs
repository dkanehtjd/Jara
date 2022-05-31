using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue info;
    public GameObject target;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            target = GetClickedObject();

            if(target.Equals(gameObject))
            {
                Trigger();
            }
        }
    }
    
    public GameObject GetClickedObject()
    {
        RaycastHit hit;
        GameObject target = null;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //마우스 포인트 근처 좌표 생성

        //아래 링크에서 여기 내용 추가하기
        //https://m.blog.naver.com/PostView.naver?isHttpsRedirect=true&blogId=ateliersera&logNo=220439790504
        if(true==(Physics.Raycast(ray.origin, ray.direction*10, out hit))) // 마우스 근처에 오브젝트 있는지 확인
        {
            target = hit.collider.gameObject;
        }
        return target;
    
    }

    public void Trigger()
    {
            var system = FindObjectOfType<DialogueSystem>();
            system.Begin(info);
    }
}

