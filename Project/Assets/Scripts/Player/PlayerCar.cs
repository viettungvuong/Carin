using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCar : MonoBehaviour
{
    public TextMeshProUGUI instructionText; 
    public GameObject car;
    public Camera carCamera, playerCamera;
    public string interactKey = "F";
    public float interactionDistance = 5f;
    public float carDuration = 30f; // duration driving a car (in seconds)

    private DateTime lastOpenedCar;
    private Player player;

    public Slider carRunSlider;

    private ZombieSpawn zombieSpawn, carZombieSpawn;
    private Rigidbody rb;

    public int carMoney = 100;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        zombieSpawn = GetComponent<ZombieSpawn>();
        carZombieSpawn = car.GetComponent<ZombieSpawn>();

        instructionText.gameObject.SetActive(false); 
        carRunSlider.gameObject.SetActive(false); // Hide slider initially

        player = GetComponent<Player>();    
    }

    void Update()
    {
        float distanceToCar = Vector3.Distance(transform.position, car.transform.position);

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
            carRunSlider.value = elapsedTime / carDuration;
            carRunSlider.gameObject.SetActive(true);

            // slider follow car
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(car.transform.position + new Vector3(0, 2f, 0)); 
            carRunSlider.transform.position = screenPosition;

            if (remainingTime <= 0f)
            {
                ExitCarMode();
            }
        }
        else
        { // not driving
            carRunSlider.gameObject.SetActive(false);
        }

        if (Mode.mode == TypeMode.WALKING && distanceToCar <= interactionDistance && player.EnoughMoney(carMoney)) // show instruction text when near a car
        {
            instructionText.gameObject.SetActive(true);
            instructionText.text = interactKey + ": Drive\n$"+carMoney.ToString();
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(car.transform.position);
            instructionText.transform.position = screenPosition + new Vector3(100, 100, 0);

            if (Input.GetKeyDown(KeyCode.F))
            {
                EnterCarMode();
            }
        }
        else
        {
            instructionText.gameObject.SetActive(false);
        }


    }

    private void LateUpdate() {
        if (Mode.mode == TypeMode.DRIVING){
            rb.position = car.transform.position + car.transform.right;
        }

    }


    void EnterCarMode()
    {
        Mode.mode = TypeMode.DRIVING;
        lastOpenedCar = DateTime.Now;

        transform.localScale = new Vector3(0, 0, 0);
        carCamera.gameObject.SetActive(true);
        playerCamera.gameObject.SetActive(false);

        instructionText.gameObject.SetActive(false);

        zombieSpawn.enabled = false;
        carZombieSpawn.enabled = true; // zombie spawn component

        // transform.SetParent(car.transform); // move player with car
    }

    void ExitCarMode()
    {
        Mode.mode = TypeMode.WALKING;

        transform.localScale = new Vector3(1, 1, 1);
        carCamera.gameObject.SetActive(false);
        playerCamera.gameObject.SetActive(true);

        instructionText.gameObject.SetActive(false);
        carRunSlider.gameObject.SetActive(false); // hide slider when not driving

        zombieSpawn.enabled = true;
        carZombieSpawn.enabled = false;

        // transform.SetParent(null); // no longer need move player with car
    }
}
