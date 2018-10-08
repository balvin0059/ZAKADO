using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ConnectDesigner : MonoBehaviour {
    public Animator anim;
    public GameObject infotext;
    public RectTransform Panel;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}
    public void OnConnectDesigner()
    {
        anim.SetBool("connection", true);
        StartCoroutine(AnimEnd());
    }
    IEnumerator AnimEnd()
    {

        yield return new WaitForSeconds(1f);
        infotext.SetActive(true);
    }
    public void ClosePanel(GameObject gO)
    {
        infotext.SetActive(false);
        anim.SetBool("connection", false);
        gO.SetActive(false);
        Panel.sizeDelta = new Vector2(450, 360);
        Panel.localPosition = new Vector3(0, 0, 0);
    }
}
