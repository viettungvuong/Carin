using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEditor.Rendering.Universal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public static TimeManager Instance { get; private set; }

    public delegate void OnPauseChanged(bool isPaused);
    public event OnPauseChanged PauseChanged;

    public float TotalGameTime { get; private set; }

    private bool isPaused;
    public GameObject pausePanel;

    private Player player;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Update()
    {
        if (player.isDied()){
            return;
        }
        if (!isPaused)
        {
            TotalGameTime += Time.deltaTime;
            timeText.text = FormatTime(TotalGameTime);
        }

        if (SceneManager.GetActiveScene().name == "MainScene")
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePause();
            }
        }
    }

    public string SurviveTime(){
        string time =  FormatTime(TotalGameTime);
        List<String> splitTime = time.Split(":").ToList();
        int i = 0;

        while (i<2) {
            if (splitTime[i]=="00"){
                splitTime.RemoveAt(0);
            }
            else{
                break;
            }
            i++;
        }
        string res = String.Join(":", splitTime);
        Debug.Log(res);
        return res;
    }

    private string FormatTime(float time)
    {
        int hours = Mathf.FloorToInt(time / 3600);
        int minutes = Mathf.FloorToInt((time % 3600) / 60);
        int seconds = Mathf.FloorToInt(time % 60);

        return $"{hours:D2}:{minutes:D2}:{seconds:D2}";
    }

    public void PauseGame()
    {
        if (!isPaused)
        {
            isPaused = true;
            Time.timeScale = 0f;
            PauseChanged?.Invoke(isPaused);
            pausePanel.SetActive(true);
        }
    }

    public void ResumeGame()
    {
        if (isPaused)
        {
            isPaused = false;
            Time.timeScale = 1f;
            PauseChanged?.Invoke(isPaused);
            pausePanel.SetActive(false);
        }
    }

    public void TogglePause()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void ResetTime()
    {
        TotalGameTime = 0f;
    }

    public bool IsPaused()
    {
        return isPaused;
    }
}
