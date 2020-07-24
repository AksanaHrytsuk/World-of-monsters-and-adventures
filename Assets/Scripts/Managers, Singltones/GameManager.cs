using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    #region Singletone

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
           Destroy(gameObject); 
        }
        else
        {
            Instance = this;
        }
    }

    #endregion

    public bool startGame = true;
    
    void Start()
    {
       
       DontDestroyOnLoad(gameObject);
    }
}
