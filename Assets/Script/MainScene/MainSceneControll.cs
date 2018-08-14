using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainSceneControll : MonoBehaviour {
    public Text goldText;
    public Text expText;
    public Text enegyText;

    void Update()
    {
        expText.text = GlobalValue.instance.exp.ToString();
        goldText.text = GlobalValue.instance.gold.ToString();
        enegyText.text = GlobalValue.instance.enegy.ToString();
    }

	public void OnAdventure()
    {
        SceneManager.LoadScene("MapScene");
    }
    public void OnTeammate()
    {
        SceneManager.LoadScene("TeamScene");
    }
    public void OnUpgrade()
    {
        SceneManager.LoadScene("UpgradeScene");
    }
}
