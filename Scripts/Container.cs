using System.Collections.Generic;
using System.Linq;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class Container : NetworkBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public List<Equipment> ContainedItems = new List<Equipment>();
    private bool isHovering = false;
    public bool isStorageContainer = false;
    [SerializeField] private Sprite closedSprite;
    [SerializeField] private Sprite openedSprite;

    private void Awake()
    {
        GenerateItemServerRpc();
    }

    private void Update()
    {
        if (isHovering && Input.GetKeyDown(KeyCode.E))
        {
            var localWizard = NetworkManager.Singleton.LocalClient.PlayerObject.GetComponent<Wizard>();
            if(Vector3.Distance(localWizard.transform.position, transform.position) < 5)
            {
                localWizard.gameObject.GetComponent<Wizard>().PlayerHUD.ContainerOpened(this);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
    }

    [ServerRpc(RequireOwnership = false)]
    public void DepositEquipmentServerRpc(EquipmentId type)
    {
        DepositEquipmentClientRpc(type);
    }

    [ClientRpc]
    public void DepositEquipmentClientRpc(EquipmentId type)
    {
        if (ContainedItems.Count < 9)
        {
            switch (type)
            {
                case EquipmentId.WandOfMagicMissiles:
                    ContainedItems.Add(SpellSingleton.Instance.WandOfMagicMissiles);
                    break;
                case EquipmentId.WandOfBoomerang:
                    ContainedItems.Add(SpellSingleton.Instance.WandOfBoomerang);
                    break;
                case EquipmentId.WandOfWindGust:
                    ContainedItems.Add(SpellSingleton.Instance.WandOfWindGust);
                    break;
                case EquipmentId.SpellbookOfFireball:
                    ContainedItems.Add(SpellSingleton.Instance.SpellbookOfFireball);
                    break;
                case EquipmentId.SpellbookOfTornado:
                    ContainedItems.Add(SpellSingleton.Instance.SpellbookOfTornado);
                    break;
                default:
                    break;
            }
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void GrabContainedItemServerRpc(int index)
    {
        GrabContainedItemClientRpc(index);
    }

    [ClientRpc]
    public void GrabContainedItemClientRpc(int index)
    {
        if (ContainedItems.Count == 1)
        {
            ContainedItems.RemoveAt(index);
            if (!isStorageContainer)
            {
                GetComponent<SpriteRenderer>().sprite = openedSprite;
                //We want to change to open sprite
            }
        }
        else
        {
            ContainedItems.RemoveAt(index);
        }
    }

    [ServerRpc]
    public void GenerateItemServerRpc()
    {
        if (!isStorageContainer)
        {
            int randomSpawn = Random.Range(1, 4);
            switch (randomSpawn)
            {
                case 1:
                    GenerateItemClientRpc(EquipmentId.WandOfBoomerang);
                    break;
                case 2:
                    GenerateItemClientRpc(EquipmentId.WandOfWindGust);
                    break;
                case 3:
                    GenerateItemClientRpc(EquipmentId.SpellbookOfTornado);
                    break;
                default:
                    break;
            }
        }
    }

    [ClientRpc]
    public void GenerateItemClientRpc(EquipmentId type)
    {
        GetComponent<SpriteRenderer>().sprite = closedSprite;
        switch (type)
        {
            case EquipmentId.WandOfMagicMissiles:
                ContainedItems.Add(SpellSingleton.Instance.WandOfMagicMissiles);
                break;
            case EquipmentId.WandOfBoomerang:
                ContainedItems.Add(SpellSingleton.Instance.WandOfBoomerang);
                break;
            case EquipmentId.WandOfWindGust:
                ContainedItems.Add(SpellSingleton.Instance.WandOfWindGust);
                break;
            case EquipmentId.SpellbookOfFireball:
                ContainedItems.Add(SpellSingleton.Instance.SpellbookOfFireball);
                break;
            case EquipmentId.SpellbookOfTornado:
                ContainedItems.Add(SpellSingleton.Instance.SpellbookOfTornado);
                break;
            default:
                break;
        }
    }
}
