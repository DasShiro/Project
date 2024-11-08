using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void PlayerDesign()
    {

}

    public void CloseGame()
    {
        //if (Application.isEditor)
        //{
        //    EditorApplication.isPlaying = false;
        //}
        //else
        //{
            Application.Quit();
       // }
    }
}
