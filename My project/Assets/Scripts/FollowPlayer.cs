using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    private Vector3 forwardVectorPlayer;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        forwardVectorPlayer = player.transform.forward;
        this.transform.position = player.transform.position + new Vector3(0f, 5f, 0f) + forwardVectorPlayer * (-10);
        this.transform.LookAt(player.transform.position);
    }
}
