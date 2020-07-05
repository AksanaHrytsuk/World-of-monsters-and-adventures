using UnityEngine;


public class EnemyMovement : Movement
{
    [Header("Movements")]
    public GameObject[] patrolPoints;
    public float distanceToPoint;

    public override Vector2 Direction()
    {
        if (patrolPoints.Length == 0)
        {
            StopMovement();
            return transform.position;
        }
	
        Vector2 direction = patrolPoints[0].transform.position - transform.position; // желаемое - текущее 
        ChangeDirection();
        return direction;
    }
	
    private void ChangeDirection()
    {
        float distance = Vector2.Distance(transform.position, patrolPoints[0].transform.position);
        if (distance < distanceToPoint)
        {
            ChangeArray();
        }
    }
	
    private void ChangeArray()
    {
        GameObject tmp = patrolPoints[0];
        for (int i = 0; i < patrolPoints.Length - 1; i++)
        {
            patrolPoints[i] = patrolPoints[i + 1];
        }
	
        patrolPoints[patrolPoints.Length - 1] = tmp; // обращение к последнему элементу массива
    }
}