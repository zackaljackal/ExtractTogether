using System.ComponentModel;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class InventoryMenu : MonoBehaviour
{
    //public static event Action<List<InventoryItem>> OnInventoryChange;
    private Wizard localWizard;
    [SerializeField] private Transform WandInventorySlot;
    [SerializeField] private Transform SpellbookInventorySlot;

    // Start is called before the first frame update
    void Start()
    {
        WandInventorySlot.GetComponent<Image>().sprite = localWizard.Wand.Sprite;
        SpellbookInventorySlot.GetComponent<Image>().sprite = localWizard.Spellbook.Sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InventoryUpdate()
    {
        WandInventorySlot.GetComponent<Image>().sprite = localWizard.Wand.Sprite;
        SpellbookInventorySlot.GetComponent<Image>().sprite = localWizard.Spellbook.Sprite;
    }
}
