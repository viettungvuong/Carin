using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCar : MonoBehaviour
{
    public TextMeshProUGUI instructionText; 
    public Transform car;
    public Camera carCamera, playerCamera;
    public string interactKey = "F";
    public float interactionDistance = 5f;
    public float carDuration = 60f; // duration driving a car (in seconds)

    private DateTime lastOpenedCar;
    private bool isDriving = false;

    public Slider carRunSlider;

    void Start()
    {
        instructionText.gameObject.SetActive(false); 
        carRunSlider.gameObject.SetActive(false); // Hide slider initially
    }

    void Update()
    {
        float distanceToCar = Vector3.Distance(transform.position, car.position);

        if (Mode.mode == TypeMode.DRIVING)
        {
            if (Input.GetKeyDown(KeyCode.F)) // if currently driving
            {
                ExitCarMode();
                return;
            }
            
            float elapsedTime = (float)(DateTime.Now - lastOpenedCar).TotalSeconds;
            float remainingTime = Mathf.Max(carDuration - elapsedTime, 0f);
            
            // slider reflect remaining time to drive
            carRunSlider.value = remainingTime / carDuration;
            carRunSlider.gameObject.SetActive(true);

            if (remainingTime <= 0f)
            {
                ExitCarMode();
            }
        }
        else
        {
            carRunSlider.gameObject.SetActive(false);
        }

        if (Mode.mode == TypeMode.WALKING && distanceToCar <= interactionDistance) // show instruction text when near a car
        {
            instructionText.gameObject.SetActive(true);
            instructionText.text = interactKey + ": Drive";
            UpdateInstructionTextPosition();
            
            if (Input.GetKeyDown(KeyCode.F))
            {
                EnterCarMode();
            }
        }
        else
        {
            instructionText.gameObject.SetActive(false);
        }

        if (isDriving)
        {
            // slider follow car
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(car.position + new Vector3(0, 2f, 0)); 
            carRunSlider.transform.position = screenPosition;
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
        lastOpenedCar = DateTime.Now;
        isDriving = true;

        transform.localScale = new Vector3(0, 0, 0);
        carCamera.gameObject.SetActive(true);
        playerCamera.gameObject.SetActive(false);

        instructionText.gameObject.SetActive(false);
    }

    void ExitCarMode()
    {
        Mode.mode = TypeMode.WALKING;
        isDriving = false;

        transform.localScale = new Vector3(1, 1, 1);
        carCamera.gameObject.SetActive(false);
        playerCamera.gameObject.SetActive(true);

        instructionText.gameObject.SetActive(false);
        carRunSlider.gameObject.SetActive(false); // hide slider when not driving
    }
}
