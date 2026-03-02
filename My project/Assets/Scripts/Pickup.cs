using System;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private float rotSpeed = 1.0f;
    private float bounceFactor = 0.01f;
    public Color color;

    [SerializeField] private int cooldown=int.MaxValue;
    private float t = 0f;

    private bool pickedup = false;
    //public GameObject onPickupEffect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }
    void Awake()
    {
        this.GetComponent<Renderer>().material.color = color;

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(0,rotSpeed,0);
        this.transform.Translate(0, Mathf.Sin(Time.time*4)* bounceFactor, 0);
        if(pickedup)
        {
            t += Time.deltaTime;
            if (t >= 0)
            {
                t = 0;
                pickedup = false;
                this.GetComponent<MeshRenderer>().enabled = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(1);
        if (other.attachedRigidbody.gameObject.TryGetComponent<truck>(out truck Truck)&&!pickedup)
        {
            Debug.Log(2);
            Truck.goods.Add(color);
            this.GetComponent<MeshRenderer>().enabled = false;
            pickedup = true;
            t = -cooldown;

            //Instantiate(onPickupEffect, this.transform);
        }
        
        
    }

}
