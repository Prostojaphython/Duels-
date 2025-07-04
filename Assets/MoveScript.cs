using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MoveScript : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float sprintSpeed = 8f;
    public float jumpForce = 15f;

    [Header("Mouse Look")]
    public Transform cameraTransform;
    public float mouseSensitivity = 2f;
    public float maxLookAngle = 80f;

    private Rigidbody rb;
    private float xRotation = 0f;
    public bool isGrounded;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.3f;
    public LayerMask groundMask;
    [SerializeField] Animator anim;
    [SerializeField] GameObject pistol, rifle, miniGun;
    bool isPistol, isRifle, isMiniGun;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        Cursor.lockState = CursorLockMode.Locked;
        anim = GetComponent<Animator>();

    }

    void Update()
    {
        GroundCheck();        // Проверка выведена в отдельный метод
        HandleMouseLook();
        HandleJump();
        if(Input.GetKeyDown(KeyCode.Alpha1) && isPistol)
        {
            ChooseWeapon(Weapons.Pistol);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && isRifle)
        {
            ChooseWeapon(Weapons.Rifle);
        }
        //Здесь допиши логику для минигана и для отсутствия оружия

    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        bool isSprinting = Input.GetKey(KeyCode.LeftShift);
        float currentSpeed = isSprinting ? sprintSpeed : moveSpeed;

        Vector3 move = transform.right * x + transform.forward * z;
        Vector3 targetVelocity = move * currentSpeed;

        Vector3 velocity = rb.velocity;
        velocity.x = targetVelocity.x;
        velocity.z = targetVelocity.z;
        rb.velocity = velocity;
    }
    public enum Weapons
    {
        None,
        Pistol,
        Rifle,

    }
    Weapons weapons = Weapons.None;

    void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            anim.SetBool("Jump", true);
        }
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -maxLookAngle, maxLookAngle);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
        anim.SetBool("Run", rb.velocity.magnitude > 0.1f && isGrounded);
    }

    void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        //Debug.Log("isGrounded = " + isGrounded);
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
        anim.SetBool("Jump", false);
    }
    public void ChooseWeapon(Weapons weapons)
    {
        anim.SetBool("Pistol", weapons == Weapons.Pistol);
        anim.SetBool("Assault", weapons == Weapons.Rifle);
        anim.SetBool("NoWeapon", weapons == Weapons.None);
        pistol.SetActive(weapons == Weapons.Pistol);
        rifle.SetActive(weapons == Weapons.Rifle);
    }
    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
            {
                case "pistol":
                    if (!isPistol)
                    {
                        isPistol = true;
                        ChooseWeapon(Weapons.Pistol);
                    }
                    break;
                case "rifle":
                    if (!isRifle)
                    {
                        isRifle = true;
                        ChooseWeapon(Weapons.Rifle);
                    }
                    break;
                default:
                    break;
        }
            Destroy(other.gameObject);
    }  
}