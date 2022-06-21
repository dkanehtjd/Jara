using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBtnCtrl : MonoBehaviour
{
    public GameObject mainC;
    public GameObject subC1;
    public GameObject subC2;
    public GameObject subC3;
    public GameObject subC4;
    public GameObject gameObject;


    public void MainCOn()
    {
        //mainC.enabled = true;
        //subC.enabled = false;
        mainC.SetActive(true);
        subC1.SetActive(false);
        subC2.SetActive(false);
        subC3.SetActive(false);
        subC4.SetActive(false);
        gameObject.SetActive(true);


    }

    public void SubCOn1()
    {
        //mainC.enabled = false;
        //subC.enabled = true;
        mainC.SetActive(false);
        subC1.SetActive(true);
        subC2.SetActive(false);
        subC3.SetActive(false);
        subC4.SetActive(false);
        gameObject.SetActive(false);
    }

    public void SubCOn2()
    {

        mainC.SetActive(false);
        subC1.SetActive(false);
        subC2.SetActive(true);
        subC3.SetActive(false);
        subC4.SetActive(false);
        gameObject.SetActive(false);
    }

    public void SubCOn3()
    {

        mainC.SetActive(false);
        subC1.SetActive(false);
        subC2.SetActive(false);
        subC3.SetActive(true);
        subC4.SetActive(false);
        gameObject.SetActive(false);
    }

    public void SubCOn4()
    {

        mainC.SetActive(false);
        subC1.SetActive(false);
        subC2.SetActive(false);
        subC3.SetActive(false);
        subC4.SetActive(true);
        gameObject.SetActive(false);
    }

}