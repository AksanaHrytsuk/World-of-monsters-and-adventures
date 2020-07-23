using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    [SerializeField] private float reloadLevelDelay;
    #region Singletone

    public static SceneLoader Instance { get; private set; }

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
    public  void RestartLevel(float delay)
    {
        StartCoroutine(RestartLevelCoroutine(delay));
    }
    
    IEnumerator RestartLevelCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
    
    public void RestartLevel()
    {
        // получить индекс текущей сцены
        int ccurentScene = SceneManager.GetActiveScene().buildIndex;
        // перезагрузка текущей сцены
        SceneManager.LoadScene(ccurentScene);
    }
    
    public void LoadNextScene()
    {
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeSceneIndex + 1, LoadSceneMode.Single);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    
    public void LoadNextSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void LoadLevel(int index)
    {
        SceneManager.LoadScene(index);
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
