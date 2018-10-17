using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneLockScript : MonoBehaviour {
    public GameObject go;
    public void MissionNotComplete(Transform gp)
    {
        Instantiate(go, new Vector3(0, 0, 0), Quaternion.identity, gp);
    }
}
