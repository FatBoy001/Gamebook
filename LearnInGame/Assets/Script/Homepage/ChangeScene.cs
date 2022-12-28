using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void changeScene(){
        SceneManager.LoadScene(RecordData.game);
    }
    public void changeSceneToTutorial()
    {
        SceneManager.LoadScene(RecordData.game+"Tutorial");
    }
    public void backToHomePage()
    {
        SceneManager.LoadScene("HomePage");
    }
    public void Logout()
    {
        StudentData.setStudentId("");
        SceneManager.LoadScene("Login");
    }
    public void shutDownGame()
    {
        Application.Quit();
    }
    
}
