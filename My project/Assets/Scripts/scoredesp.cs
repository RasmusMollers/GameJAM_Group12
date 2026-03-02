using UnityEngine;
using TMPro;
public class scoredesp : MonoBehaviour
{
    public TMP_Text text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(dtime.DTime.number>10)
        text.text = ("Time taken for last delivery run: " + dtime.DTime.number.ToString()+"s");
    }
}
