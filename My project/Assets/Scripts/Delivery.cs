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
        color = gameObjectWanted.GetComponent<Renderer>().material.color;
        this.GetComponent<Renderer>().material.color = color;
        FindFirstObjectByType<DeliveryOverseer>().deliveries++;
    }

    public GameObject GetWantedDelivery()
    {
        return gameObjectWanted;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody.gameObject.TryGetComponent<truck>(out truck Truck))
        {
            for (int i = 0; i < Truck.goods.Count; i++)
            {
                if (Truck.goods[i] == color)
                {
                    Destroy(gameObject);
                    FindFirstObjectByType<DeliveryOverseer>().CheckComplete();
                    Truck.deliveries++;
                }
            }
        }
    }
}
