using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public LevelLoader levelLoader;
    public void OnClickLoadLevel(int level)
    {
        levelLoader.LoadNextLevel(level);
    }

    public void OnClickExit()
    {
        Application.Quit();
    }
}
