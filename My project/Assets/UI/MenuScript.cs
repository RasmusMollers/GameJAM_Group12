using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void PlayGame ()
    {
        
        SceneManager.LoadScene("GameScene");

    }
    public void QuitGame ()
    {
        Debug.Log("quit");
        Application.Quit();
    }



}
