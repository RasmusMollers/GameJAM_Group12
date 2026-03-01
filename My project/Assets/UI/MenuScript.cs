using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void PlayGame ()
    {
        
        SceneManager.LoadScene("CityTrack");

    }
    public void QuitGame ()
    {
        Debug.Log("quit");
        Application.Quit();
    }



}
