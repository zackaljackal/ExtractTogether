using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using Unity.Networking;
using System.Runtime.CompilerServices;
using UnityEngine.SceneManagement;
using Unity.Collections;
using Unity.VisualScripting;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport;

public class ConnectionUI : NetworkBehaviour
{
    [SerializeField] private Button hostButton;
    [SerializeField] private InputField ipAddress;
    [SerializeField] private Button connectButton;
    [SerializeField] private Canvas playerHUD;
    private bool isDone = false;
    private double waitTime = 1;
    
    // Start is called before the first frame update
    void Awake()
    {
        Camera.main.transform.position = new Vector3(100, 100, -10);
        connectButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
            isDone = true;
        });

        hostButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
            Camera.main.AddComponent<HostNetwork>();
            isDone = true;
        });
    }

    private void Update()
    {
        if (isDone)
        {
            if(waitTime > 0)
            {
                waitTime -= Time.deltaTime;
            }
            else
            {
                Instantiate(playerHUD);
                Destroy(gameObject);
            }
        }
    }
}
