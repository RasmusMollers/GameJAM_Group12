using System;
using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class DeliveryOverseer : MonoBehaviour
{
    public int deliveries = 0;

    private float t = 0;
    
    private void Start()
    {
        t = Time.time;
    }

    public void CheckComplete()
    {
        deliveries--;
        if (deliveries<=0)
        {
            ending();
        }
    }

    private void ending()
    {
        dtime.DTime.number = (int)(Time.time - t);
        
        SceneManager.LoadScene(0);
    }
}
