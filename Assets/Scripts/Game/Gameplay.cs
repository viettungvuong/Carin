using UnityEngine;

public class Gameplay : MonoBehaviour
{
    public static Gameplay Instance { get; private set; }
    [HideInInspector] public int score;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
