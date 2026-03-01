using UnityEngine;
using System.Collections;

public class Delivery : MonoBehaviour
{
    private IEnumerator coroutine;
    GameObject gameObjectWanted;


    public GameObject onDeliveryEffect;
    public Color color;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        // Start function WaitAndPrint as a coroutine.
        this.GetComponent<Renderer>().material.color = color;

        coroutine = WaitAndPrint(2.0f);
        StartCoroutine(coroutine);

        print("Coroutine started");
    }
    private IEnumerator WaitAndPrint(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        print("Coroutine ended: " + Time.time + " seconds");
        color = gameObjectWanted.GetComponent<Pickup>().color;

    }
    void OnLevelWasLoaded()
    {


        //this.GetComponent<Renderer>().material.color = color;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public GameObject GetWantedDelivery()
    {
        return gameObjectWanted;
    }


    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var refPlayer = other.transform.parent.gameObject.GetComponent<PlayerController>();
            var pickupList = refPlayer.getPickupList();
            if (pickupList.Contains(gameObjectWanted))
            {
                refPlayer.updatePickupList();
            }
        }
    }
    */
}
