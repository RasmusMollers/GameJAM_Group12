using UnityEngine;

public class WheelControl : MonoBehaviour
{
    public Transform wheelModel;

    [HideInInspector] public WheelCollider WheelCollider;

    Vector3 position;
    Quaternion rotation;

    // Start is called before the first frame update
    private void Start()
    {
        WheelCollider = GetComponent<WheelCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.CompareTag("Steering"))
        {
            WheelCollider.GetWorldPose(out position, out rotation);
            wheelModel.transform.position = position;
            wheelModel.transform.rotation = rotation;
        }
    }
}
