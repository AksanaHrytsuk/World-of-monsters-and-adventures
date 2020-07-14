using UnityEngine;

public class EnemyMovement : Movement
{
    [Header("Movements")]
    public GameObject[] patrolPoints;
    public float distanceToPoint;
    private CharacterScript _character;
    protected override void StartAdditional()
    {
        base.StartAdditional();
        _character = FindObjectOfType<CharacterScript>();
    }
    
    public override Vector3 Direction()
    {
        // Если в плеера попол IceBall и плеер Frozen, монстрик следует к плееру
        if (_character.Frozen)
        {
            return _character.transform.position - transform.position;
        }
        if (patrolPoints.Length == 0)
        {
            StopMovement();
            return transform.position;
        }
        Vector3 direction = patrolPoints[0].transform.position - transform.position; // желаемое - текущее 
        ChangeDirection();
        return direction;
    }
	
    private void ChangeDirection()
    {    
        float distance = Vector3.Distance(transform.position, patrolPoints[0].transform.position);
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