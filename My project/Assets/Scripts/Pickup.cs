using UnityEngine;

public class Pickup : MonoBehaviour
{
    private float rotSpeed = 1.0f;
    private float bounceFactor = 0.01f;
    //public GameObject onPickupEffect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(0,rotSpeed,0);
        this.transform.Translate(0, Mathf.Sin(Time.time*4)* bounceFactor, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        this.GetComponent<MeshRenderer>().enabled = false;
        //gameObject.SetActive(false);

        //Instantiate(onPickupEffect, this.transform);
    }

}
