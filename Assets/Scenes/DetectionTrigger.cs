using UnityEngine;

public class DetectionTrigger : MonoBehaviour
{
    public EnemyPatrol enemy;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("TEST");
            enemy.StartChasing();
        }
    }
}