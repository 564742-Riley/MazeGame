using UnityEngine;

public class HeadBob : MonoBehaviour
{
    
    public float bobbingSpeed = 14.0f;
    public float bobbingAmount = 0.05f;


    public Rigidbody playerRb;

    private float timer = 0.0f;
    private float defaultY = 0.0f;

    void Start()
    {
        defaultY = transform.localPosition.y;
    }

    void Update()
    {
        float horizontalSpeed = new Vector3(playerRb.linearVelocity.x, 0, playerRb.linearVelocity.z).magnitude;

        if (horizontalSpeed > 0.1f)
        {
            timer += Time.deltaTime * bobbingSpeed;
            transform.localPosition = new Vector3(
                transform.localPosition.x,
                defaultY + Mathf.Sin(timer) * bobbingAmount,
                transform.localPosition.z
            );
        }
        else
        {
            timer = 0;
            transform.localPosition = new Vector3(
                transform.localPosition.x,
                Mathf.Lerp(transform.localPosition.y, defaultY, Time.deltaTime * bobbingSpeed),
                transform.localPosition.z
            );
        }
    }
}
