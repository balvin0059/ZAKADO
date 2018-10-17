using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Loading : MonoBehaviour {

    public Image loadLine;
    public GameObject catie;
    public Text loadText;
    float disPlayProgress;

    public void GotoScene(string sceneName)
    {
        StartCoroutine(DisPlayLoadingScreen(sceneName));
        
    }
    private IEnumerator DisPlayLoadingScreen(string sceneName)
    {
        int toProgress;

        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);

        async.allowSceneActivation = false;
        while(async.progress < 0.9f)
        {
            toProgress = (int)async.progress * 100;
            while(disPlayProgress < toProgress)
            {
                disPlayProgress += 2 + Time.deltaTime;
                if (disPlayProgress >= 100)
                {
                    disPlayProgress = 100;
                }
                setLoading(disPlayProgress);
                yield return  new WaitForEndOfFrame();
            }
        }
        toProgress = 100;
        while (disPlayProgress < toProgress)
        {
            disPlayProgress += 2 + Time.deltaTime;
            if(disPlayProgress >= 100)
            {
                disPlayProgress = 100;
            }
            setLoading(disPlayProgress);
            yield return new WaitForEndOfFrame();
        }

        async.allowSceneActivation = true;
        string n_sceneName = SceneManager.GetActiveScene().name;
        System.GC.Collect();
    }

    private void setLoading(float percent)
    {
        loadLine.fillAmount = percent / 100f;
        loadText.text = ((int)percent).ToString() + "%";
    }
}
