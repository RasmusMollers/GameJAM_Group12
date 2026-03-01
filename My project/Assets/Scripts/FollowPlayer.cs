using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    private Vector3 forwardVectorPlayer;
    private Vector3 baseCameraPosition;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = player.transform.position + new Vector3(0f, 2f, 0) + forwardVectorPlayer * (-10f);
    }

    // Update is called once per frame
    void Update()
    {
        forwardVectorPlayer = player.transform.forward;
        baseCameraPosition = player.transform.position + new Vector3(0f, 2f, 0) + forwardVectorPlayer*(-10f);
        
        this.transform.position = Vector3.Lerp(baseCameraPosition, this.transform.position, 0.5f*Time.deltaTime);
        this.transform.LookAt(player.transform.position);
    }
}
