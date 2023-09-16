using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager Instance = null;

    #region Unity_function

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    #region Scene_transition
    public void StartGame()
    {
        SceneManager.LoadScene("Project1");
    }
    public void LoseGame()
    {
        SceneManager.LoadScene("WinScene");
    }
    public void WinGame()
    {
        SceneManager.LoadScene("WinScene");
    }
    public void MainGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
    #endregion
}
