using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public void NewGame(){
        SceneManager.LoadScene("MainScene");
    }

    public void Unpause(){
        TimeManager.Instance.ResumeGame();
    }

    public void Quit(){
        #if UNITY_STANDALONE
            Application.Quit();
        #endif
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
