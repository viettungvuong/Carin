using TMPro;
using UnityEngine;

public class PlayerCar : MonoBehaviour
{
    public TextMeshProUGUI instructionText; 
    public Transform car; 
    public string interactKey = "F";
    public float interactionDistance = 5f; 

    void Start()
    {
        instructionText.gameObject.SetActive(false); 
    }

    void Update()
    {
        float distanceToCar = Vector3.Distance(transform.position, car.position);
        
        if (Mode.mode == TypeMode.DRIVING) // if currently driving
        {
            ExitCarMode();
            return;
        }
        
        if (distanceToCar <= interactionDistance)
        {
            instructionText.gameObject.SetActive(true);
            instructionText.text = interactKey + ": Drive";
            UpdateInstructionTextPosition();
            
            if (Input.GetKeyDown(KeyCode.F) && Mode.mode == TypeMode.WALKING)
            {
                EnterCarMode();
            }
        }
        else
        {
            instructionText.gameObject.SetActive(false);
        }
    }

    void UpdateInstructionTextPosition()
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        instructionText.transform.position = screenPosition + new Vector3(100, 100, 0); 
    }

    void EnterCarMode()
    {
        Mode.mode = TypeMode.DRIVING;
        instructionText.gameObject.SetActive(false);
    }

    void ExitCarMode()
    {
        Mode.mode = TypeMode.WALKING;
        instructionText.gameObject.SetActive(false);
    }
}