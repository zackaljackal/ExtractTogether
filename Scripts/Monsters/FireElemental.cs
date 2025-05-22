using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class FireElemental : Monster
{

    public override int Health { get { return _health; } }

    public override float MovementSpeed { get { return _movementSpeed; } }

    private int _health = 6;
    private int _movementSpeed = 2;
    private Wizard aggrodWizard = null;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsServer)
            Destroy(this);

        if(aggrodWizard == null)
        {
            aggrodWizard = NetworkManager.Singleton.LocalClient.PlayerObject.GetComponent<Wizard>();
        }
        rb = GetComponent<Rigidbody2D>();
        var direction = transform.position - aggrodWizard.transform.position;
        direction = new Vector3(direction.x, direction.y, 0).normalized;
        rb.velocity = direction * _movementSpeed; 

            

    }
}
