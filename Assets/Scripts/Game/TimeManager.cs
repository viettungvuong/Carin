using TMPro;
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

    private void Update()
    {
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
        }
    }

    public void ResumeGame()
    {
        if (isPaused)
        {
            isPaused = false;
            Time.timeScale = 1f;
            PauseChanged?.Invoke(isPaused);
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
