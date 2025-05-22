using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class DroppedItem : NetworkBehaviour
{
    public Equipment equipment;
    public Sprite EtherSprite;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Wizard")
        {
            if (collision.gameObject.GetComponent<Wizard>().Inventory.Count < 3)
            {
                collision.gameObject.GetComponent<Wizard>().PickUpItemServerRpc(equipment.Id);
                Destroy(gameObject);
            }
        }
    }
}
