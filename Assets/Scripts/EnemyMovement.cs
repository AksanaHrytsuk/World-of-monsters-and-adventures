using System;
using UnityEngine;

public class EnemyMovement : Movement
{
    [Header("Movements")]
    public GameObject[] patrolPoints;
    public float distanceToPoint;
    private CharacterScript _character;

    [SerializeField]
    private bool cyclically;

    private int _currentIndex = 0;
    private int _step;
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
        Vector3 direction = NextPoint().position - transform.position; // желаемое - текущее 
        ChangeDirection();
        return direction;
    }

    private Transform NextPoint()
    {
        if (cyclically)
        {
            return patrolPoints[0].transform;
        }
        return patrolPoints[_currentIndex].transform;
    }
    private void ChangeDirection()
    {    
        float distance = Vector3.Distance(transform.position, NextPoint().position);
        if (distance < distanceToPoint)
        {
            if (cyclically)
            {
                ChangeArray();
            }
            else
            {
                ChangeIndex();
            }
        }
    }

    private void ChangeIndex()
    {
        if (_currentIndex == 0)
        {
            _step = 1;
        }
        if (_currentIndex == patrolPoints.Length-1)
        {
            _step = -1;
        }
        _currentIndex += _step;
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