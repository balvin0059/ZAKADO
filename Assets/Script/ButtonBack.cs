﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBack : MonoBehaviour {

    public void OnBack()
    {
        SceneManager.LoadScene("MainScene");
    }
}