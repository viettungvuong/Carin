using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevel : MonoBehaviour
{
    public static PlayerLevel Instance { get; private set; }

    private int killed = 0;

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

    public void AddKill()
    {
        killed++;
    }

    public int GetLevel()
    {
        if (killed < 10)
        {
            return 1;
        }
        else if (killed < 20)
        { 
            return 2;
        }
        else if (killed < 30)
        {
            return 3;   
        }
        else if (killed < 50)
        {
            return 4;
        }
        else
        {
            return 5;
        }
    }
}
