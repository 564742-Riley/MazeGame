using UnityEngine;


public class PlayerScript : MonoBehaviour
{
    
    public float moveSpeed = 7.0f;
   
    private Rigidbody rb;
    private Vector3 moveDirection;
    private bool isGrounded;

    void Start()
    {
        
        rb = GetComponent<Rigidbody>();

        rb.freezeRotation = true;
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        moveDirection = (transform.forward * vertical + transform.right * horizontal).normalized;

    }

    void FixedUpdate()
    {
        
        rb.linearVelocity = new Vector3(moveDirection.x * moveSpeed, rb.linearVelocity.y, moveDirection.z * moveSpeed);
    }
}
