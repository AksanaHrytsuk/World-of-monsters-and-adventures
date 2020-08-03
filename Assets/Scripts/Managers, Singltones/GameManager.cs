using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool loadGame;
    void Start()
    {
        loadGame = true;
        DontDestroyOnLoad(gameObject);
    }
}
