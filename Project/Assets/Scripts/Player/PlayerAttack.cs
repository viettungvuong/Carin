using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    BaseAim currentState;
    [HideInInspector] public Fire hip = new Fire();
    [HideInInspector] public Aim aim = new Aim();

    [SerializeField] float mouseSense = 1;
    [SerializeField] Transform camFollowPos;
    float xAxis, yAxis;
    public Camera mainCamera;
    [HideInInspector] public Animator anim;
    [HideInInspector] public CinemachineVirtualCamera vCam;
    public float adsFov = 40;
    [HideInInspector] public float hipFov;
    [HideInInspector] public float currentFov;
    public float fovSmoothSpeed = 10;

    [SerializeField] Transform aimPos;
    [SerializeField] float aimSmoothSpeed = 20;
    [SerializeField] LayerMask aimMask;

    [SerializeField] Transform muzzle, gun;

    [SerializeField] float bulletForce = 100f;
    [SerializeField] float range = 100f;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] LayerMask targetLayer;

    [SerializeField] Image aimingReticle;
    [SerializeField] private Image refillProgressBar;  
    [SerializeField] float hipFireDistance = 10f; 
    


    private static bool shot = false;
    private int bullets = 150;
    private int bulletsInClip = 30;
    private bool isRefilling = false;
    private float refillDuration = 2f;  // refill time

    public TextMeshProUGUI bulletText;

    public AudioClip shotClip, reloadClip;
    private AudioSource audioSource;

    void Start()
    {
        vCam = GetComponentInChildren<CinemachineVirtualCamera>();
        hipFov = vCam.m_Lens.FieldOfView;
        anim = GetComponent<Animator>();
        SwitchState(hip);
        aimingReticle.gameObject.SetActive(false);
        refillProgressBar.fillAmount = 0f;

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        Handleaiming();
        HandleShooting();
        currentState.UpdateState(this);

        if (bulletsInClip <= 0 && bullets>0 && !isRefilling)
        {
            StartCoroutine(RefillClip());
        }
    }

    private void FixedUpdate()
    {
        bulletText.text = bulletsInClip.ToString() + "/" + bullets.ToString();
    }

    private void LateUpdate()
    {
        camFollowPos.localEulerAngles = new Vector3(yAxis, camFollowPos.localEulerAngles.y, camFollowPos.localEulerAngles.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, xAxis, transform.eulerAngles.z);
    }

    private void Handleaiming()
    {
        xAxis += Input.GetAxisRaw("Mouse X") * mouseSense;
        yAxis -= Input.GetAxisRaw("Mouse Y") * mouseSense;
        yAxis = Mathf.Clamp(yAxis, -80, 80);

        vCam.m_Lens.FieldOfView = Mathf.Lerp(vCam.m_Lens.FieldOfView, currentFov, fovSmoothSpeed * Time.deltaTime);

        if (currentState == aim)
        {
            Vector2 screenCentre = new Vector2(Screen.width / 2, Screen.height / 2);
            Ray ray = mainCamera.ScreenPointToRay(screenCentre);
            if (Physics.Raycast(ray, out RaycastHit hit, range, aimMask))
            {
                aimPos.position = Vector3.Lerp(aimPos.position, hit.point, aimSmoothSpeed * Time.deltaTime);
            }
            aimingReticle.gameObject.SetActive(true);
        }
        else // when not aiming
        {
            aimPos.position = transform.position + transform.forward * hipFireDistance;
            aimingReticle.gameObject.SetActive(false);
        }

        Vector3 screenPos = mainCamera.WorldToScreenPoint(aimPos.position);
        aimingReticle.transform.position = screenPos;
    }


    private void HandleShooting()
    {
        if (bulletsInClip > 0 && Input.GetMouseButtonDown(0) && !shot)
        {
            shot = true;
            StartCoroutine(DelayedShoot());
        }
    }

    private IEnumerator DelayedShoot()
    {
        yield return new WaitForSeconds(0.1f);  
        Shoot();
        shot = false;
    }

    private void Shoot()
    {
        void PlayShootSfx(){
            audioSource.clip = shotClip;
            audioSource.Play();
        }
        muzzleFlash.Play();
        Vector3 flyTo = aimPos.position + new Vector3(0,2,0);

        Vector3 shootDirection = (flyTo - muzzle.position).normalized;

        // RaycastHit hit;
        // if (Physics.Raycast(muzzle.position, shootDirection, out hit, range, targetLayer))
        // {
        //     if (hit.collider.gameObject.CompareTag("Zombie"))
        //     {
        //         hit.collider.gameObject.GetComponent<Zombie>().TakeDamage(100);
        //     }

        //     Rigidbody hitRb = hit.collider.GetComponent<Rigidbody>();
        //     if (hitRb != null)
        //     {
        //         hitRb.AddForce(-hit.normal * bulletForce);
        //     }
        // }

        GameObject bullet = ObjectPool.SpawnFromPool("Bullet",  muzzle.position);

        Vector3 gunForward = new Vector3(gun.transform.forward.x, 0f, gun.transform.forward.z);
        Vector3 gunAngle = Quaternion.LookRotation(gunForward).eulerAngles;
        Vector3 offset = gun.transform.rotation.eulerAngles - bullet.transform.rotation.eulerAngles;

        bullet.transform.rotation = Quaternion.Euler(gunAngle + offset);



        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        if (bulletRb != null)
        {
            bulletRb.AddForce(shootDirection * bulletForce, ForceMode.Impulse);
        }

        PlayShootSfx();

        bulletsInClip--;  // decrease bullets in clip after shooting
    }

    private IEnumerator RefillClip()
    {
        audioSource.clip = reloadClip;
        audioSource.Play();
        isRefilling = true;
        float elapsed = 0f;
        refillProgressBar.gameObject.SetActive(true);
        while (elapsed < refillDuration)
        {
            elapsed += Time.deltaTime;
            refillProgressBar.fillAmount = elapsed / refillDuration;  

            Vector3 screenPos = mainCamera.WorldToScreenPoint(transform.position + new Vector3(0, 2, 0));
            refillProgressBar.transform.position = screenPos;

            yield return null;
        }

        int refillAmount = Mathf.Min(3, bullets);
        bulletsInClip = refillAmount;  
        bullets -= refillAmount;  
        isRefilling = false;
        refillProgressBar.fillAmount = 0f;  
        refillProgressBar.gameObject.SetActive(false);
    }

    public void SwitchState(BaseAim state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    public void RefillBullets(int bullets){
        this.bullets += bullets;
    }
}
