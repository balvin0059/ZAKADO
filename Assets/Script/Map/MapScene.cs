using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapScene : MonoBehaviour {

	public void OnLevel()
    {
        SceneManager.LoadScene("GameScene");
    }
}
