using System;
using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.UI;
public class DailyBonusController : MonoBehaviour
{
    public BonusItem[] BonusItems;
    public RectTransform Arrow;
    public AudioSource SpineSound;
    public AudioSource ChoiceEndedSound;
    public GameObject button;
    public GameObject ComfirmPanel;
    public Text itemName;
    public int listIndex;
    bool ontime;
	// Use this for initialization
	void Start () {
        ontime = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ChoiceRandom()
    {
        button.SetActive(false);
        var choice = ProbabilityController.ChoiceRandom(BonusItems);
        Debug.Log(BonusItems[choice].ID);
        itemName.text = BonusItems[choice].name;
        StartCoroutine(
            RotateToAngle(choice , new Vector3(0,0,1),
            Arrow.rotation.eulerAngles.z,
            Arrow.rotation.eulerAngles.z + (360 - Arrow.rotation.eulerAngles.z) + BonusItems[choice].Angle,
            10, EndRandomChoice));
        
    }

    public void EndRandomChoice()
    {
        ChoiceEndedSound.Play();
    }

    IEnumerator RotateToAngle(int id, Vector3 rotateAxis,float currentAngle, float targetAngleValue, 
        float speed = 10, Action endFired = null)
    {
        var itemSoundAngle = currentAngle + (360/BonusItems.Length);
        while (true)
        {
            var step = ((targetAngleValue - currentAngle) + speed) * Time.deltaTime;
            if (currentAngle + step > targetAngleValue)
            {
                step = targetAngleValue - currentAngle;
                Arrow.Rotate(rotateAxis, step);
                if (endFired != null)
                    endFired();

                break;
            }
            currentAngle += step;
            if (currentAngle >= itemSoundAngle)
            {
                SpineSound.Play();
                itemSoundAngle = currentAngle + (360 / BonusItems.Length);
            }
            Arrow.Rotate(rotateAxis, step);

            yield return null;
        }
        Reward(id);


    }
    public void CloseAllDaliyPanel(GameObject g)
    {
        ComfirmPanel.SetActive(false);
        g.SetActive(false);
    }
    void Reward(int id)
    {
        if (!ontime)
        {
            ontime = true;
            ComfirmPanel.SetActive(true);
            GlobalValue.instance.daliyBonus = true;

            if (BonusItems[id].rewardType == 2)
            {
                ItemAPI.AddItem(BonusItems[id].itemNumber);
            }
            else if (BonusItems[id].rewardType == 1)
            {
                GlobalValue.instance.exp += BonusItems[id].itemNumber;
            }
            else if (BonusItems[id].rewardType == 0)
            {
                GlobalValue.instance.gold += BonusItems[id].itemNumber;
            }
        }
    }
}