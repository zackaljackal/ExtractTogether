using System.ComponentModel;
using Unity;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    
    [SerializeField] private Transform primarySpellIcon;
    [SerializeField] private Transform secondarySpellIcon;
    [SerializeField] private Transform dashSpellIcon;
    [SerializeField] private CooldownIndicator spellCooldown;
    [SerializeField] private Transform Inventory;
    [SerializeField] private Transform DraggableItem;
    [SerializeField] private Transform InventoryPanel;
    [SerializeField] private Transform OpenedContainerPanel;
    [SerializeField] private Container OpenedContainer;
    private bool isHoveringInventory = false;
    public bool HasOpenContainer = false;
    private Wizard playerWizard;

    private void Awake()
    {
        playerWizard = NetworkManager.Singleton.LocalClient.PlayerObject.GetComponent<Wizard>();
        playerWizard.PlayerHUD = this;
    }

    private void Update()
    {
        UpdateEquipment();
        InventoryPanel.GetChild(1).GetComponent<Image>().sprite = playerWizard.Wand.Sprite;
        InventoryPanel.GetChild(2).GetComponent<Image>().sprite = playerWizard.Spellbook.Sprite;
        //3 because max wizard inventory length ?!
        for(int i = 0; i < 3; i++)
        {
            if (i < playerWizard.Inventory.Count)
            {
                InventoryPanel.GetChild(i + 3).GetComponent<Image>().sprite = playerWizard.Inventory[i].Sprite;
            }
            else
            {
                InventoryPanel.GetChild(i + 3).GetComponent<Image>().sprite = null;
            }
        }
        if(OpenedContainerPanel.gameObject.activeSelf == true)
        {
            //9 because max containerlength?!
            for (int i = 0; i < 9; i++)
            {
                if (i < OpenedContainer?.ContainedItems.Count)
                    OpenedContainerPanel.GetChild(i).GetComponent<Image>().sprite = OpenedContainer.ContainedItems[i].Sprite;
                else
                    OpenedContainerPanel.GetChild(i).GetComponent<Image>().sprite = null;
            }       
        }

        if(isHoveringInventory && Input.GetKeyDown(KeyCode.Mouse0))
        {
            
        }

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        isHoveringInventory = true;
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        isHoveringInventory = false;
    }

    public void ContainerOpened(Container container)
    {
        HasOpenContainer = true;
        InventoryPanel.gameObject.SetActive(true);
        OpenedContainerPanel.gameObject.SetActive(true);
        //3 because max wizard inventory length ?!
        for(int i = 0; i < 3; i++)
        {
            InventoryPanel.GetChild(i + 8).GetComponent<InventoryItem>().SelectedContainer = container;
        }
        //9 because max containerlength?!
        for (int i = 0; i < 9; i++)
        {
            OpenedContainerPanel.GetChild(i).GetComponent<InventoryItem>().SelectedContainer = container;
            if (i < container.ContainedItems.Count)
                OpenedContainerPanel.GetChild(i).GetComponent<Image>().sprite = container.ContainedItems[i].Sprite;
            else
                OpenedContainerPanel.GetChild(i).GetComponent<Image>().sprite = null;
        }

    }

    public void ContainerClosed()
    {
        HasOpenContainer = false;
        //Setting to 9 since that is max number of container inventory slots in UI
        for(int i = 0; i < 9; i++)
        {
            OpenedContainerPanel.GetChild(i).GetComponent<InventoryItem>().SelectedContainer = null;
            OpenedContainerPanel.GetChild(i).GetComponent<Image>().sprite = null;
        }
        OpenedContainerPanel.gameObject.SetActive(false);
    }

    public void InventoryPanelToggled()
    {
        InventoryPanel.gameObject.SetActive(!InventoryPanel.gameObject.activeSelf);
        ContainerClosed();
    }

    public void PrimarySpellFired(float cooldown)
    {
        spellCooldown.Ability = CooldownIndicator.AbilitySlot.Primary;
        var cooldownIcon = Instantiate(spellCooldown, primarySpellIcon.localPosition, Quaternion.identity, gameObject.transform);
        cooldownIcon.transform.localPosition = new Vector3(primarySpellIcon.localPosition.x, primarySpellIcon.localPosition.y - (75/2));
    }

    public void SecondarySpellFired(float cooldown)
    {
        spellCooldown.Ability = CooldownIndicator.AbilitySlot.Secondary;
        var cooldownIcon = Instantiate(spellCooldown, secondarySpellIcon.localPosition, Quaternion.identity, transform.parent);
        cooldownIcon.transform.localPosition = new Vector3(secondarySpellIcon.localPosition.x, secondarySpellIcon.localPosition.y - (75 / 2));
    }

    public void Dash(float cooldown)
    {
        spellCooldown.Ability = CooldownIndicator.AbilitySlot.Dash;
        var cooldownIcon = Instantiate(spellCooldown, dashSpellIcon.localPosition, Quaternion.identity, gameObject.transform);
        cooldownIcon.transform.localPosition = new Vector3(dashSpellIcon.localPosition.x, dashSpellIcon.localPosition.y - (75 / 2));
    }

    public void UpdateEquipment()
    {
        primarySpellIcon.GetComponent<Image>().sprite = playerWizard.Wand.Spell.Sprite;
        secondarySpellIcon.GetComponent<Image>().sprite = playerWizard.Spellbook.Spell.Sprite;
    }
}
