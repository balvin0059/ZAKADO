using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemShopControll : MonoBehaviour {
    public GameObject[] itemSold;
    public ItemAttribute[] itemAttribute;
    public Image[] itemImage;
    public Text itemName;
    public Text itemValue;
    public Text itemInfo;
    public Image itemSprite;
    public GameObject button;
    public GameObject soldText;
    public GameObject Holder;
    public GameObject NoFoodAnim;
    public int index;
	// Use this for initialization
	void Start () {
		for(int i = 0; i < itemImage.Length; i++)
        {
            itemImage[i].sprite = itemAttribute[i].icon;
            itemSold[i].SetActive(ItemHolder.instance.storeCount[i]);
        }
	}	
	// Update is called once per frame
	void Update () {
        button.SetActive(!ItemHolder.instance.storeCount[index]);
        soldText.SetActive(ItemHolder.instance.storeCount[index]);
    }
    public void SelectItem(int i)
    {
        index = i;

        itemName.text = itemAttribute[index].itemName;
        itemValue.text = itemAttribute[index].SellValue.ToString();
        itemInfo.text = itemAttribute[index].itemInfo;
        itemSprite.sprite = itemAttribute[index].icon;
    }
    public void BuyItem()
    {
        if (GlobalValue.instance.gold >= itemAttribute[index].SellValue)
        {
            GlobalValue.instance.gold -= itemAttribute[index].SellValue;
            ItemAPI.AddItem(itemAttribute[index].itemID);
            print("新增道具 名稱:" + (itemAttribute[index].itemName));
            itemSold[index].SetActive(true);
            ItemHolder.instance.storeCount[index] = true;
        }
        else
        {
            Instantiate(NoFoodAnim, Holder.transform);
        }
    }
    public void PlaySFX()
    {
        SoundControll.Instance.PlayEffecSound(SoundControll.Instance.buttonClip);
    }
}