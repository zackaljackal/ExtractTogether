using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class HostNetwork : NetworkBehaviour
{
    private bool raidInProgress = false;
    private List<Vector3> spawnPoints = new List<Vector3>() {
        new Vector3(8, 0),
        new Vector3(-8, 0)
    };

    // Update is called once per frame
    void Update()
    {
        if (!IsServer) Destroy(this);
        var players = GameObject.FindGameObjectsWithTag("Wizard");
        bool everyoneReady = true;
        foreach (var player in players)
        {
            if (!player.GetComponent<Wizard>().isReady)
            {
                everyoneReady = false;
            }
        }
        if (everyoneReady)
        {
            StartRaidServerRpc();
            raidInProgress = true;
        }
    }

    [ServerRpc]
    public void StartRaidServerRpc()
    {
        var players = GameObject.FindGameObjectsWithTag("Wizard");
        for(int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<Wizard>().SpawnClientRpc(spawnPoints[i]);
            players[i].GetComponent<Wizard>().isReady = false;
        }

        var rngContainers = GameObject.FindGameObjectsWithTag("RNGContainers");
        for(int i = 0; i < rngContainers.Length; i++)
        {
            rngContainers[i].GetComponent<Container>().GenerateItemServerRpc();
        }
    }
}
