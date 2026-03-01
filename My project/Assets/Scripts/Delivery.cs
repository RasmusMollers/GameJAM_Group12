using UnityEngine;
using System.Collections;

public class Delivery : MonoBehaviour
{
    public GameObject gameObjectWanted;


    //public GameObject onDeliveryEffect;
    public Color color;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        // Start function WaitAndPrint as a coroutine.
        this.GetComponent<Renderer>().material.color = gameObjectWanted.GetComponent<Renderer>().material.color;

    }

    public GameObject GetWantedDelivery()
    {
        return gameObjectWanted;
    }
}
