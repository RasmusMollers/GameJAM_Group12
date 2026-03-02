
using Unity.VisualScripting;
using UnityEngine;

public class dtime : MonoBehaviour
{
    //singelton class for score
    public static dtime DTime;
    
    public int number = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (DTime == null)
        {
            DTime = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            if (DTime == this)
            {
                
            }else Destroy(this);
        }
    }

    
}
