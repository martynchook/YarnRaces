using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "LevelManager", menuName = "Managers/LevelManager")]
public class LevelManager : ManagerBase, IAwake, IStart
{
    [SerializeField]
    private int CurrentLevel = 1;
    
    public void OnAwake()
    {
        //PlayerPrefs.DeleteAll();
        Debug.Log(PlayerPrefs.GetInt("SaveSceen"));
        Debug.Log(CurrentLevel);
        if (PlayerPrefs.GetInt("SaveSceen") != 0)
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("SaveSceen"), LoadSceneMode.Additive);
            Toolbox.Get<LevelManager>().CurrentLevel = PlayerPrefs.GetInt("SaveSceen");
            Debug.Log(Toolbox.Get<LevelManager>().CurrentLevel);
        }
        else
        {
            SceneManager.LoadScene(Toolbox.Get<LevelManager>().CurrentLevel, LoadSceneMode.Additive);
            PlayerPrefs.SetInt("SaveSceen", Toolbox.Get<LevelManager>().CurrentLevel);
            Debug.Log(Toolbox.Get<LevelManager>().CurrentLevel);
        }
    }
    
    public void OnStart()
    {
        Toolbox.Get<EventManager>().OnLoudNextLevelEvent += LoadNextLexel;
    }
    
    public void LoadNextLexel()
    {
        Toolbox.Get<LevelManager>().CurrentLevel += 1;
        if (Toolbox.Get<LevelManager>().CurrentLevel >= SceneManager.sceneCountInBuildSettings)
        {
            int nextSceen = Random.Range(1, SceneManager.sceneCountInBuildSettings - 1);
            SceneManager.LoadScene(nextSceen, LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync(Toolbox.Get<LevelManager>().CurrentLevel - 1);
            Toolbox.Get<LevelManager>().CurrentLevel = nextSceen;
        }
        else
        {
            PlayerPrefs.SetInt("SaveSceen", Toolbox.Get<LevelManager>().CurrentLevel);
            SceneManager.LoadScene(Toolbox.Get<LevelManager>().CurrentLevel, LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync(Toolbox.Get<LevelManager>().CurrentLevel - 1);
        }
    }
    
    public void CleerSceens()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(Toolbox.Get<LevelManager>().CurrentLevel);
        CurrentLevel = 1;
    }
}
