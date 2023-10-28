using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// manage the Inventory
/// </summary>
public class InventoryManager : MonoBehaviour
{
    public Inventory myBag;
    public GameObject slotGrid;
    public Slot slotPrefab;
    public ObjectDisplay Emerald;
    public ObjectDisplay Stone;
    public delegate void ManageObject(ObjectDisplay objectDiaplay);
    /// <summary>补充代码
    /// public event ManageObject OnManageObject;
    /// xxx.OnManageObject += myBag.ObjectManager;
    /// xxx.OnManageObject += RefreshObject;
    /// </summary>
    /*
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        instance = this;
    }*/

    void Start()
    {
        Emerald.objectHeld = 1;
    }
    void Update()
    {
        
        myBag.ObjectManager(Emerald);
        myBag.ObjectManager(Stone);
        RefreshObject();
    }
    public void CreatNewObject(ObjectDisplay objectDiaplay)
    {
            Slot newObject = Instantiate(slotPrefab, slotGrid.transform.position, Quaternion.identity);
            newObject.gameObject.transform.SetParent(slotGrid.transform);
            newObject.slotObject = objectDiaplay;
            newObject.slotImage.sprite = objectDiaplay.objectImage;
            newObject.slotNum.text = objectDiaplay.objectHeld.ToString();
    }

    /// <summary>
    /// 如果获得重复物品，更新背包显示
    /// </summary>
    public void RefreshObject()
    {
        for (int i = 0; i < slotGrid.transform.childCount; i++) 
        {
            if (slotGrid.transform.childCount == 0) 
            {
                break;
            }
            Destroy(slotGrid.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < myBag.objectList.Count; i++)
        {
            CreatNewObject(myBag.objectList[i]);
        }

    }
}
