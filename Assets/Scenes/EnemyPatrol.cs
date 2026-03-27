using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public Transform player;
public Transform pointC;
public Transform pointD;
    public float speed = 3f;

    private Transform targetPoint;
    private bool chasing = false;

    void Start()
    {
        targetPoint = pointA;
    }

    void Update()
    {
        if (chasing)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                player.position,
                speed * Time.deltaTime
            );
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPoint.position,
            speed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            if (targetPoint == pointA)
                targetPoint = pointB;
            else if (targetPoint == pointB)
                targetPoint = pointC;
            else if (targetPoint == pointC)
                targetPoint = pointD;
            else
                targetPoint = pointA;
        }
    }

    public void StartChasing()
    {
        chasing = true;
    }
}