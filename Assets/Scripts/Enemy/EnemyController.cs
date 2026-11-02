
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Transform m_Target;
    [SerializeField] float m_Speed = 3f;
    [SerializeField] float m_RotationSpeed = 5f;
    [SerializeField] float m_DetectionRange = 10f;
    [SerializeField] float m_AttackRange = 2f;

    [SerializeField] LayerMask m_ObstacleMask;

    void Start()
    {
        if (m_Target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player != null)
            {
                m_Target = player.transform;
            }
        }
    }

    void FixedUpdate()
    {
        if (m_Target == null)
            return;

        if (CanSeePlayer())
        {
            ChasePlayer();
            AttackPlayer();
        }
    }

    bool CanSeePlayer()
    {
        Vector3 direction = m_Target.position - transform.position;

        float distance = direction.magnitude;

        if (distance > m_DetectionRange)
            return false;

        if (Physics.Raycast(transform.position + Vector3.up,direction.normalized,distance,m_ObstacleMask))
        {
            return false;
        }

        return true;
    }

    void ChasePlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, m_Target.position);

        if (distanceToPlayer > m_AttackRange)
        {
            Vector3 newPos = new Vector3(
                m_Target.position.x,
                transform.position.y,
                m_Target.position.z);

            transform.position = Vector3.MoveTowards(transform.position,newPos,m_Speed * Time.fixedDeltaTime);
        }

        RotateTowardsPlayer();
    }

    void AttackPlayer()
    {
        float distanceToPlayer = Vector3.Distance(
            transform.position, m_Target.position);

        if (distanceToPlayer <= m_AttackRange)
        {
            Debug.Log("Attacking Player!");
        }
    }

    void RotateTowardsPlayer()
    {
        Vector3 direction = m_Target.position - transform.position;
        direction.y = 0;

        if (direction.sqrMagnitude < 0.001f)
            return;

        Quaternion lookRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            lookRotation,
            Time.fixedDeltaTime * m_RotationSpeed);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
            Debug.Log("Player Died!");
        }
    }
}
