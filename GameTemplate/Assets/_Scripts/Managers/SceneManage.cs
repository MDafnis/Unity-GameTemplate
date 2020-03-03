using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    public static SceneManage instance;
    [SerializeField] private int sceneNumber;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void OpenScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }

    public void QuitScene()
    {
        //EditorApplication.isPlaying = false;
        Application.Quit();
    }

    public void ResumeScene()
    {
        Time.timeScale = 1;
    }
}
