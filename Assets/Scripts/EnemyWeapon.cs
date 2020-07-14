using System.Collections;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] private float delay = 2f;
    [SerializeField] private float rate = 1f;
    [SerializeField] private Bullets fireBallPrefab;
    
    private bool _canShoot = true;

    void Start()
    {
        StartCoroutine(CreateFireBalls(delay, rate));
    }
    
    IEnumerator CreateFireBalls(float delay, float rate)
    {
        yield return new WaitForSeconds(delay);
        while (_canShoot)
        {
            Instantiate(fireBallPrefab, transform.position, transform.rotation);
            yield return new WaitForSeconds(rate);
        }
    }
}
