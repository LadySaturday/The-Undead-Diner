﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{public void changeScene(int newScene)
    {
        SceneManager.LoadScene(newScene);
    }
}
