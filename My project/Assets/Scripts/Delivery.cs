using UnityEngine;

public class Delivery : MonoBehaviour
{
    GameObject gameObjectWanted;

    public GameObject onDeliveryEffect;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
