using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkTestScript : MonoBehaviour
{
    public GameObject atkO;
    void OnEnable()
    {
        Debug.Log($"{name} was enabled! Stack trace:\n" + Environment.StackTrace);
        
    }
    private void Update()
    {
        if (atkO.activeSelf == true)
        {
            Debug.Log("atk actiove");
        }
    }
}
