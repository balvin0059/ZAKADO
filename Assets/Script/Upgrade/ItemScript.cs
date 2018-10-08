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
    public GameObject[] itemList;
    public Image itemImage;
    public Text itemNameText;
    public Text itemInfoText;
    public int catID;
    public int lastTimecatID;
    public int itemID;
    public int lastTimeItemID;
    public RectTransform context;
    public Text lvText;
    public Text hpText;
    public Text atkText;


    public void ItemPanelOpen()
    {
        itemPanel.SetActive(true);
        if (ItemHolder.instance.amount > 3)
        {
            context.sizeDelta += new Vector2(180 * (ItemHolder.instance.amount - 3), 0);
        }
        gameObject.GetComponent<UpgradeScene>().itemSelect = true;
        catID = gameObject.GetComponent<UpgradeScene>().indexCat;
        OrderCheck();
        UseCheck();
    }

    #region 面板控制
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
                lvText.text = "Lv:" + GlobalValue.instance.catHolder[catID].GetComponent<CatControll>().state.lv.ToString("00");
                hpText.text = "HP:" + GlobalValue.instance.catHolder[catID].GetComponent<CatControll>().state.hp.ToString();
                atkText.text = "攻擊:" + GlobalValue.instance.catHolder[catID].GetComponent<CatControll>().state.attack.ToString();
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
                lvText.text = "Lv:" + GlobalValue.instance.catHolder[catID].GetComponent<CatControll>().state.lv.ToString("00");
                hpText.text = "HP:" + GlobalValue.instance.catHolder[catID].GetComponent<CatControll>().state.hp.ToString();
                atkText.text = "攻擊:" + GlobalValue.instance.catHolder[catID].GetComponent<CatControll>().state.attack.ToString();
            }
        }
    }
    #endregion

    #region 控制方法
    //實現效果方法
    public void AttributesEffect(ItemAttribute.ItemType itemType, int getItemID, bool itemSelect)
    {
        Debug.Log("進入物品判定");
        Debug.Log("物品編號" + getItemID);
        Debug.Log("物品" + itemID);
        //此為素質物品區
        if (itemType == ItemAttribute.ItemType.AttributesEffect)
        {
            if(getItemID == 1000||getItemID == 1008||getItemID == 1011)
            {
                if (!itemSelect)
                {
                    GlobalValue.instance.catHolder[catID].GetComponent<CatControll>().state.attack += ItemHolder.instance.globleItems[itemID].itemAttribute.itemBuffer;
                }
                else
                {
                    GlobalValue.instance.catHolder[catID].GetComponent<CatControll>().state.attack -= ItemHolder.instance.globleItems[itemID].itemAttribute.itemBuffer;
                }
            }// 攻擊增減
            if (getItemID == 1001 || getItemID == 1009 || getItemID == 1012)
            {
                if (!itemSelect)
                {
                    GlobalValue.instance.catHolder[catID].GetComponent<CatControll>().state.hp += ItemHolder.instance.globleItems[itemID].itemAttribute.itemBuffer;
                }
                else
                {
                    GlobalValue.instance.catHolder[catID].GetComponent<CatControll>().state.hp -= ItemHolder.instance.globleItems[itemID].itemAttribute.itemBuffer;
                }
            }// hp 增減
            if (getItemID == 1002 || getItemID == 1010)
            {
                if (!itemSelect)
                {
                    GlobalValue.instance.playerEnegyPower += ItemHolder.instance.globleItems[itemID].itemAttribute.itemBuffer;
                }
                else
                {
                    GlobalValue.instance.playerEnegyPower -= ItemHolder.instance.globleItems[itemID].itemAttribute.itemBuffer;
                }
            }// 能量 增減
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
                if (getItemID == 1007 || getItemID == 1013)
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
    //簡化步驟巢狀迴圈 功能:判定先加入的道具順序 依照順序擺放
    void OrderCheck()
    {
        for (int i = 0; i < ItemHolder.instance.amount; i++)
        {
            for (int j = 0; j < itemList.Length; j++)
            {
                for (int k = 0; k < ItemHolder.instance.globleItems.Count; k++)
                {
                    if (ItemHolder.instance.globleItems[k].order == j + 1)
                    {
                        itemList[k].SetActive(true);
                        itemList[k].transform.localPosition = new Vector3((85f + 180 * j), -92.5f, 0.0f);
                    }
                }
            }
        }
    }
    //簡化步驟迴圈 功能:被使用過的將不能再使用
    void UseCheck()
    {
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
    #endregion
}