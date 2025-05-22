using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;


public class TeleportCircle : NetworkBehaviour
{
    [SerializeField] private bool IsExtract;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Wizard" && IsServer)
        {
            if (IsExtract)
                collision.gameObject.GetComponent<Wizard>().BeginExtractServerRpc(transform.position);
            else
                collision.gameObject.GetComponent<Wizard>().ReadyToRaidServerRpc();
        }
    }
}
