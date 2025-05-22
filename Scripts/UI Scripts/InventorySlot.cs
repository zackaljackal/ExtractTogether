using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] int inventorySlot;

    // Update is called once per frame
    void Update()
    {
        var sprite = FindSprite();
        if (sprite == null)
        {
            GetComponent<Image>().color = Color.clear;
        }
        else
        {
            GetComponent<Image>().color = new Color(29, 19, 19, 65);
            gameObject.GetComponent<Image>().sprite = sprite;
        }
    }

    private Sprite FindSprite()
    {
        //var wizardInventory = NetworkManager.Singleton.LocalClient.PlayerObject.GetComponent<Wizard>().Wand;
        //if (wizardInventory.Count > inventorySlot)
        //{
        //    switch (NetworkManager.Singleton.LocalClient.PlayerObject.GetComponent<Wizard>().inventory[inventorySlot].Type)
        //    {
        //        case ItemType.Ether:
        //            return ItemSprites.Instance.Ether;
        //        default:
        //            return null;
        //    }
        //}
        return null;
    }
}
