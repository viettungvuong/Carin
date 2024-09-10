using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class AimStateManager : MonoBehaviour
{
    AimBaseState currentState;
    public HipFireState Hip = new HipFireState();
    public AimState Aim = new AimState();

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

    [SerializeField] Transform muzzle;
    [SerializeField] float bulletForce = 100f;
    [SerializeField] float range = 100f;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] LayerMask targetLayer;

    [SerializeField] Image aimingReticle; 

    private static bool shot = false;

    void Start()
    {
        vCam = GetComponentInChildren<CinemachineVirtualCamera>();
        hipFov = vCam.m_Lens.FieldOfView;
        anim = GetComponent<Animator>();
        SwitchState(Hip);
        aimingReticle.gameObject.SetActive(false);  
    }

    void Update()
    {
        HandleAiming();
        HandleShooting();
        currentState.UpdateState(this);
    }

    private void LateUpdate()
    {
        camFollowPos.localEulerAngles = new Vector3(yAxis, camFollowPos.localEulerAngles.y, camFollowPos.localEulerAngles.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, xAxis, transform.eulerAngles.z);
    }

    private void HandleAiming()
    {
        xAxis += Input.GetAxisRaw("Mouse X") * mouseSense;
        yAxis -= Input.GetAxisRaw("Mouse Y") * mouseSense;
        yAxis = Mathf.Clamp(yAxis, -80, 80);

        vCam.m_Lens.FieldOfView = Mathf.Lerp(vCam.m_Lens.FieldOfView, currentFov, fovSmoothSpeed * Time.deltaTime);

        Vector2 screenCentre = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = mainCamera.ScreenPointToRay(screenCentre);
        if (Physics.Raycast(ray, out RaycastHit hit, range, aimMask))
        {
            aimPos.position = Vector3.Lerp(aimPos.position, hit.point, aimSmoothSpeed * Time.deltaTime);
            Vector3 screenPos = mainCamera.WorldToScreenPoint(aimPos.position);
            aimingReticle.transform.position = screenPos;
        }
        // aimingReticle.transform.position = aimPos.position;

        if (currentState == Aim)
        {
            aimingReticle.gameObject.SetActive(true); 
        }
        else
        {
            aimingReticle.gameObject.SetActive(false); 
        }
    }


    private void HandleShooting()
    {
        if (Input.GetMouseButtonDown(0) && !shot)
        {
            shot = true;
            StartCoroutine(DelayedShoot());
        }
    }

    private IEnumerator DelayedShoot()
    {
        yield return new WaitForSeconds(0.1f);  // Slight delay for shooting
        Shoot();
        shot = false;
    }

    private void Shoot()
    {
        muzzleFlash.Play();

        Vector3 shootDirection = (aimPos.position - muzzle.position).normalized;

        RaycastHit hit;
        if (Physics.Raycast(muzzle.position, shootDirection, out hit, range, targetLayer))
        {
            if (hit.collider.gameObject.CompareTag("Zombie"))
            {
                hit.collider.gameObject.GetComponent<Zombie>().TakeDamage(100);
            }

            Rigidbody hitRb = hit.collider.GetComponent<Rigidbody>();
            if (hitRb != null)
            {
                hitRb.AddForce(-hit.normal * bulletForce);
            }
        }

        GameObject bullet = ObjectPool.SpawnFromPool("Bullet", muzzle.position);
        bullet.transform.rotation = muzzle.rotation;

        // fly desired direction
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        if (bulletRb != null)
        {
            bulletRb.AddForce(shootDirection * bulletForce, ForceMode.Impulse);
        }
    }




    public void SwitchState(AimBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }
}
