using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class Wizard : NetworkBehaviour
{
    private const float DASHLENGTH = 0.33f;
    private Vector3 LOBBYSPAWN = new Vector3(-45, 5);
    private const float LOBBYSIZE = 9.6f;

    public int health = 10;
    float moveSpeed = 3f;
    private bool isDashing = false;
    public float DashLength = DASHLENGTH;
    private Rigidbody2D rb;

    public float PrimarySpellCooldown = 0.75f;
    public float SecondarySpellCooldown = 2.5f;
    public float DashCooldown = 0f;
    public float MaxDashCooldown = 2f;
    private Vector3 movementInput;
    public Container deathContainer;

    public bool isReady = false;

    //Extraction information
    public bool IsExtracting = false;
    private Vector3 ExtractLocation = new Vector3(0, 0);
    private float ExtractTimer = 3f;

    //Spells
    public PlayerInteraction PlayerHUD;

    //Equipment
    public Wand Wand;
    public Spellbook Spellbook;

    public List<Equipment> Inventory = new List<Equipment>();


    [SerializeField] private DroppedItem droppedItem = new DroppedItem();

    private void Awake()
    {
        transform.position = LOBBYSPAWN;
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsExtracting)
        {
            CountdownExtract();
        }

        if (!IsOwner) return;
        if (PrimarySpellCooldown > 0f) PrimarySpellCooldown -= Time.deltaTime;
        if (SecondarySpellCooldown > 0f) SecondarySpellCooldown -= Time.deltaTime;
        if (DashCooldown > 0f) DashCooldown -= Time.deltaTime;


        if (Input.GetKeyUp(KeyCode.Space) && DashCooldown <= 0)
        {
            isDashing = true;
            DashCooldown = 2f;
            PlayerHUD.Dash(DashCooldown);
        }

        if (isDashing)
        {
            rb.velocity = movementInput * 2f;
            DashLength -= Time.deltaTime;
            if(DashLength <= 0)
            {
                isDashing = false;
                DashLength = DASHLENGTH;
                DashCooldown = 2f;
            }
        }
        else
        {
            movementInput = new Vector3(moveSpeed * Input.GetAxisRaw("Horizontal"), moveSpeed *  Input.GetAxisRaw("Vertical"), 0);
            if (movementInput.x > 0)
                GetComponent<SpriteRenderer>().flipX = false;
            else if (movementInput.x < 0)
                GetComponent<SpriteRenderer>().flipX = true;
            //transform.position += movementInput * moveSpeed * Time.deltaTime;
            rb.velocity =  movementInput;
        }

        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);

        if (Input.GetKeyUp(KeyCode.P))
        {
            RequestDeathServerRpc();
        }

        if (Input.GetKeyUp(KeyCode.Mouse0) && PrimarySpellCooldown <= 0 && (Vector2.Distance(transform.position, LOBBYSPAWN) > LOBBYSIZE))
        {
            var direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            direction = new Vector3(direction.x, direction.y, 0).normalized;
            RequestPrimarySpellServerRpc(direction);
            PrimarySpellCooldown = Wand.Spell.MaxCooldown;
            PlayerHUD.PrimarySpellFired(PrimarySpellCooldown);
        }

        //Currently just throws fireball
        if (Input.GetKeyUp(KeyCode.Mouse1) && SecondarySpellCooldown <= 0 && (Vector2.Distance(transform.position, LOBBYSPAWN) > LOBBYSIZE))
        {
            var placement = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            placement = new Vector3(placement.x, placement.y, 0);
            RequestSecondarySpellServerRpc(placement);
            SecondarySpellCooldown = Spellbook.Spell.MaxCooldown;
            PlayerHUD.SecondarySpellFired(SecondarySpellCooldown);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            PlayerHUD.InventoryPanelToggled();
        }

    }

    [ServerRpc(RequireOwnership = false)]
    public void ReadyToRaidServerRpc()
    {
        isReady = true;
    }

    [ServerRpc]
    private void RequestPrimarySpellServerRpc(Vector3 dir)
    {
        RequestPrimarySpellClientRpc(dir);
    }

    [ClientRpc]
    private void RequestPrimarySpellClientRpc(Vector3 dir)
    {
        var primarySpell = Instantiate(Wand.Spell, transform.position, Quaternion.identity);
        Physics2D.IgnoreCollision(primarySpell.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        primarySpell.Cast(dir, this);
    }

    [ServerRpc]
    private void RequestSecondarySpellServerRpc(Vector3 dir)
    {
        RequestSecondarySpellClientRpc(dir);
    }

    [ClientRpc]
    private void RequestSecondarySpellClientRpc(Vector3 dir)
    {
        var seconarySpell = Instantiate(Spellbook.Spell, dir, Quaternion.identity);
    }

    public void TakeDamage(int damage)
    {
        if (Vector2.Distance(transform.position, LOBBYSPAWN) > LOBBYSIZE)
        {
            health -= damage;
            if (health <= 0 && IsOwner)
            {
                var deathLocation = transform.position;
                RequestDeathServerRpc();
            }
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void PickUpItemServerRpc(EquipmentId type)
    {
        PickUpItemClientRpc(type);
    }

    [ClientRpc]
    private void PickUpItemClientRpc(EquipmentId type)
    {
        if (Inventory.Count < 3)
        {
            switch (type)
            {
                case EquipmentId.WandOfMagicMissiles:
                    Inventory.Add(SpellSingleton.Instance.WandOfMagicMissiles);
                    break;
                case EquipmentId.WandOfBoomerang:
                    Inventory.Add(SpellSingleton.Instance.WandOfBoomerang);
                    break;
                case EquipmentId.WandOfWindGust:
                    Inventory.Add(SpellSingleton.Instance.WandOfWindGust);
                    break;
                case EquipmentId.SpellbookOfFireball:
                    Inventory.Add(SpellSingleton.Instance.SpellbookOfFireball);
                    break;
                case EquipmentId.SpellbookOfTornado:
                    Inventory.Add(SpellSingleton.Instance.SpellbookOfTornado);
                    break;
                default:
                    break;
            }
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void DropItemServerRpc(int index)
    {
        DropItemClientRpc(index);
    }

    [ClientRpc]
    private void DropItemClientRpc(int index)
    {
        Inventory.RemoveAt(index);
    }

    [ServerRpc]
    public void SwapEquipmentServerRpc(int index)
    {
        SwapEquipmentClientRpc(index);
    }

    [ClientRpc]
    public void SwapEquipmentClientRpc(int index)
    {
        var equipmentType = Inventory[index].GetType();
        if (equipmentType.IsSubclassOf(typeof(Wand)))
        {
            Inventory.Insert(index, Wand);
            Wand = (Wand)Inventory[index + 1];
            Inventory.RemoveAt(index + 1);
        }
        else if (equipmentType.IsSubclassOf(typeof(Spellbook)))
        {
            Inventory.Insert(index, Spellbook);
            Spellbook = (Spellbook)Inventory[index + 1];
            Inventory.RemoveAt(index + 1);
        }
    }

    [ServerRpc]
    public void SwapInventoryServerRpc(int index1, int index2)
    {
        SwapInventoryClientRpc(index1, index2);
    }

    [ClientRpc]
    public void SwapInventoryClientRpc(int index1, int index2)
    {
        Equipment tempEquipment = Inventory[index1];
        Inventory[index1] = Inventory[index2];
        Inventory[index2] = tempEquipment;
    }

    [ServerRpc]
    private void RequestDeathServerRpc()
    {
        var deathLoot = Instantiate(deathContainer, transform.position, Quaternion.identity);
        deathLoot.ContainedItems = new List<Equipment>();
        var spawnedDeathLoot = deathLoot.GetComponent<NetworkObject>();
        if (Wand.Rarity != Rarity.Common)
        {
            deathLoot.ContainedItems.Add(Wand);
        }
        if (Spellbook.Rarity != Rarity.Common)
        {
            deathLoot.ContainedItems.Add(Spellbook);
        }
        for (int i = Inventory.Count - 1; i >= 0; i--)
        {
            if (Inventory[i].Rarity != Rarity.Common)
                deathLoot.ContainedItems.Add(Inventory[i]);
        }
        spawnedDeathLoot.GetComponent<Container>().ContainedItems = new List<Equipment>(deathLoot.ContainedItems);
        spawnedDeathLoot.Spawn();
        //deathLoot.gameObject.SetActive(false);
        RequestDeathClientRpc();
    }

    [ClientRpc]
    private void RequestDeathClientRpc()
    {
        //Drop everything and replace with starter items
        if (Wand.Rarity != Rarity.Common)
        {
            Wand = new WandOfMagicMissiles();
        }
        if (Spellbook.Rarity != Rarity.Common)
        {
            Spellbook = new SpellbookOfFireball();
        }
        for (int i = Inventory.Count - 1; i >= 0; i--)
        {
            Inventory.RemoveAt(i);
        }


        //Teleport back to lobby
        health = 10;
        transform.position = LOBBYSPAWN;
    }

    [ServerRpc(RequireOwnership = false)]
    public void BeginExtractServerRpc(Vector3 extractPoint)
    {
        IsExtracting = true;
        ExtractLocation = extractPoint;
    }
    private void CountdownExtract()
    {
        if(Vector3.Distance(transform.position, ExtractLocation) > 2f)
        {
            ExtractTimer = 3f;
            IsExtracting = false;
        }

        if (ExtractTimer > 0)
        {
            ExtractTimer -= Time.deltaTime;
        }
        else
            ExtractClientRpc();
    }

    [ClientRpc]
    private void ExtractClientRpc()
    {
        transform.position = LOBBYSPAWN;
    }

    [ClientRpc]
    public void SpawnClientRpc(Vector3 location)
    {
        transform.position = location;
    }
}
