﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 1f;

    public void LoadNextLevel(int sceneIndex)
    {
        StartCoroutine(LoadLevel(sceneIndex));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        //Play animation
        transition.SetTrigger("Start");
        //Wait
        yield return new WaitForSeconds(transitionTime);
        //Load scene
        SceneManager.LoadScene(levelIndex);
    }
}
