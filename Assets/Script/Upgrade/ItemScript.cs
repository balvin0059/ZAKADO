using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemScript : MonoBehaviour {

    #region 單例模式
    public static ItemScript instance;
    public static ItemScript Instance
    {
        get
        {
            return instance;
        }
    }
    private void Awake()
    {
        instance = this;
    }
    #endregion

    [Header("Item Holder")]
    public GameObject itemPanel;
    public GameObject itemEquiepmentShow;
    public GameObject itemInfoPanel;
    public GameObject[] itemUse;
    public Image itemImage;
    public Text itemNameText;
    public Text itemInfoText;
    public int catID;
    public int lastTimecatID;
    public int itemID;
    public int lastTimeItemID;

    public void ItemPanelOpen()
    {
        itemPanel.SetActive(true);
        gameObject.GetComponent<UpgradeScene>().itemSelect = true;
        catID = gameObject.GetComponent<UpgradeScene>().indexCat;
        for (int i = 0; i < itemUse.Length; i++)
        {
            itemUse[i].SetActive(ItemHolder.instance.globleItems[i].itemUse);
            if (GlobalValue.instance.catHolder[catID].GetComponent<CatControll>().state.equiepItem_id == ItemHolder.instance.globleItems[i].id)
            {
                lastTimecatID = GlobalValue.instance.catHolder[catID].GetComponent<CatControll>().state.equiepItem_id - 1000;
                itemImage.sprite = ItemHolder.instance.globleItems[lastTimecatID].itemAttribute.icon;
                itemNameText.text = ItemHolder.instance.globleItems[lastTimecatID].itemAttribute.itemName;
                itemInfoText.text = ItemHolder.instance.globleItems[lastTimecatID].itemAttribute.itemInfo;
                itemInfoPanel.SetActive(true);
                itemImage.gameObject.SetActive(true);
            }
        }
    }
	public void CloseItemPanel(GameObject g)
    {
        GlobalValue.instance.SaveAllData();
        itemInfoPanel.SetActive(false);
        itemImage.gameObject.SetActive(false);
        g.SetActive(false);
        gameObject.GetComponent<UpgradeScene>().itemSelect = false;
    }
    public void ItemInfoShow(int id)
    {
        itemID = id;
    }
    public void SelectItem()
    {
        if (!GlobalValue.instance.catHolder[catID].GetComponent<CatControll>().state.equiepItem)
        {
            if (!ItemHolder.instance.globleItems[itemID].itemUse)
            {
                itemImage.sprite = ItemHolder.instance.globleItems[itemID].itemAttribute.icon;
                itemNameText.text = ItemHolder.instance.globleItems[itemID].itemAttribute.itemName;
                itemInfoText.text = ItemHolder.instance.globleItems[itemID].itemAttribute.itemInfo;
                AttributesEffect(ItemHolder.instance.globleItems[itemID].itemAttribute.itemType, ItemHolder.instance.globleItems[itemID].id, GlobalValue.instance.catHolder[catID].GetComponent<CatControll>().state.equiepItem);
                GlobalValue.instance.catHolder[catID].GetComponent<CatControll>().state.equiepItem = true;
                GlobalValue.instance.catHolder[catID].GetComponent<CatControll>().state.equiepItem_id = ItemHolder.instance.globleItems[itemID].id;
                ItemHolder.instance.globleItems[itemID].itemUse = true;                
                lastTimeItemID = itemID;
                lastTimecatID = catID;
                itemUse[itemID].SetActive(true);
                itemInfoPanel.SetActive(true);
                itemImage.gameObject.SetActive(true);
            }
        }
        else if (GlobalValue.instance.catHolder[catID].GetComponent<CatControll>().state.equiepItem)
        {
            if (GlobalValue.instance.catHolder[catID].GetComponent<CatControll>().state.equiepItem_id == ItemHolder.instance.globleItems[itemID].id)
            {
                AttributesEffect(ItemHolder.instance.globleItems[itemID].itemAttribute.itemType, ItemHolder.instance.globleItems[itemID].id, GlobalValue.instance.catHolder[catID].GetComponent<CatControll>().state.equiepItem);
                itemUse[itemID].SetActive(false);
                itemInfoPanel.SetActive(false);
                itemImage.gameObject.SetActive(false);

                GlobalValue.instance.catHolder[catID].GetComponent<CatControll>().state.equiepItem = false;
                GlobalValue.instance.catHolder[catID].GetComponent<CatControll>().state.equiepItem_id = 0;
                ItemHolder.instance.globleItems[itemID].itemUse = false;
            }
        }
    }
    public void AttributesEffect(ItemAttribute.ItemType itemType, int getItemID, bool itemSelect)
    {
        Debug.Log("進入物品判定");
        Debug.Log("物品編號" + getItemID);
        Debug.Log("物品" + itemID);
        //此為素質物品區
        if (itemType == ItemAttribute.ItemType.AttributesEffect)
        {
            if(getItemID == 1000)
            {
                if (!itemSelect)
                {
                    GlobalValue.instance.catHolder[catID].GetComponent<CatControll>().state.attack += 10;
                }
                else
                {
                    GlobalValue.instance.catHolder[catID].GetComponent<CatControll>().state.attack -= 10;
                }
            }
            if (getItemID == 1001)
            {
                if (!itemSelect)
                {
                    GlobalValue.instance.catHolder[catID].GetComponent<CatControll>().state.hp += 100;
                }
                else
                {
                    GlobalValue.instance.catHolder[catID].GetComponent<CatControll>().state.hp -= 100;
                }
            }
            if (getItemID == 1002)
            {
                if (!itemSelect)
                {
                    GlobalValue.instance.playerEnegyPower += 5;
                }
                else
                {
                    GlobalValue.instance.playerEnegyPower -= 5;
                }
            }
        }
        if (itemType == ItemAttribute.ItemType.FoodBubble)
        {
            if(getItemID == 1006)
            {
                Debug.Log("物品" + itemID);
                if (!itemSelect)
                {
                    Debug.Log("設定OK");
                    GlobalValue.instance.itemSBubbleBufferAmount = ItemHolder.instance.globleItems[itemID].itemAttribute.itemBuffer;
                }
                else
                {
                    Debug.Log("設定取消");
                    GlobalValue.instance.itemSBubbleBufferAmount = 0;
                }
            }
            else
            {
                if (getItemID == 1007)
                {
                    if (!itemSelect)
                    {
                        GlobalValue.instance.itemNBubbleBufferAmount = ItemHolder.instance.globleItems[itemID].itemAttribute.itemBuffer;
                    }
                    else
                    {
                        GlobalValue.instance.itemNBubbleBufferAmount = 0;
                    }
                }
            }
        }
    }
}