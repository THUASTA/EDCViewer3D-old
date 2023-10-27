using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// manage the Inventory
/// </summary>
public class InventoryManager : MonoBehaviour
{
    static InventoryManager instance;
    public Inventory myBag;
    public GameObject slotGrid;
    public Slot slotPrefab;
    public ObjectDisplay Emerald;
    public delegate void ManageObject(ObjectDisplay objectDiaplay);
    /// <summary>补充代码
    /// public event ManageObject OnManageObject;
    /// xxx.OnManageObject += myBag.ObjectManager;
    /// xxx.OnManageObject += RefreshObject;
    /// </summary>
    
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        instance = this;
    }

    void Start()
    {
        Emerald.objectHeld = 1;
    }
    void Update()
    {
        
        myBag.ObjectManager(Emerald);
        RefreshObject();
    }
    public static void CreatNewObject(ObjectDisplay objectDiaplay)
    {
            Slot newObject = Instantiate(instance.slotPrefab, instance.slotGrid.transform.position, Quaternion.identity);
            newObject.gameObject.transform.SetParent(instance.slotGrid.transform);
            newObject.slotObject = objectDiaplay;
            newObject.slotImage.sprite = objectDiaplay.objectImage;
            newObject.slotNum.text = objectDiaplay.objectHeld.ToString();
    }

    /// <summary>
    /// 如果获得重复物品，更新背包显示
    /// </summary>
    public static void RefreshObject()
    {
        //for (int i = 0; i < instance.transform.childCount; i++) 
        //{
        //    if (instance.slotGrid.transform.childCount == 0) 
        //    {
        //        break;
        //    }
        //    Destroy(instance.slotGrid.transform.GetChild(i).gameObject);
        //}

        //for (int i = 0; i < instance.myBag.objectList.Count; i++)
        //{
        //    CreatNewObject(instance.myBag.objectList[i]);
        //}

    }
}
