using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private bool localInventory;
    public Container SelectedContainer;
    private bool isHovering = false;
    public int index;

    // Update is called once per frame
    void Update()
    {
        
    //     if(!localInventory && SelectedContainer?.ContainedItems.Count > index)
    //     {
    //         GetComponent<Image>().sprite = SelectedContainer.ContainedItems[index].Sprite;
    //     }

    //     if (isHovering && Input.GetKeyDown(KeyCode.E))
    //     {
    //         var localWizard = NetworkManager.Singleton.LocalClient.PlayerObject.GetComponent<Wizard>();
    //         if (localInventory)
    //         {
    //             if (FindObjectOfType<PlayerInteraction>().HasOpenContainer)
    //             {
    //                 SelectedContainer.DepositEquipmentServerRpc(localWizard.Inventory[index].Id);
    //                 localWizard.DropItemServerRpc(index);
    //                 GetComponent<Image>().sprite = null;
    //             }
    //             else
    //             {
    //                 localWizard.SwapEquipmentServerRpc(index);
    //             }
    //         }
    //         else
    //         {
    //             localWizard.PickUpItemServerRpc(SelectedContainer.ContainedItems[index].Id);
    //             SelectedContainer.gameObject.GetComponent<Container>().GrabContainedItemServerRpc(index);
    //             GetComponent<Image>().sprite = null;
    //             if (SelectedContainer.ContainedItems.Count == 0)
    //             {
    //                 isHovering = false;
    //                 FindObjectOfType<PlayerInteraction>().ContainerClosed();
    //             }
    //         }
    //     }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
    }
}
