using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class CooldownIndicator : MonoBehaviour
{
    public AbilitySlot Ability;
    public float MaxCooldown = 1000f;
    public float Cooldown = 1000f;

    private void Awake()
    {
        switch (Ability) {
            case AbilitySlot.Dash:
                Cooldown = NetworkManager.Singleton.LocalClient.PlayerObject.GetComponent<Wizard>().DashCooldown;
                MaxCooldown = NetworkManager.Singleton.LocalClient.PlayerObject.GetComponent<Wizard>().MaxDashCooldown;
                break;
            case AbilitySlot.Primary:
                Cooldown = NetworkManager.Singleton.LocalClient.PlayerObject.GetComponent<Wizard>().PrimarySpellCooldown;
                MaxCooldown = NetworkManager.Singleton.LocalClient.PlayerObject.GetComponent<Wizard>().Wand.Spell.MaxCooldown;
                break;
            case AbilitySlot.Secondary:
                Cooldown = NetworkManager.Singleton.LocalClient.PlayerObject.GetComponent<Wizard>().SecondarySpellCooldown;
                MaxCooldown = NetworkManager.Singleton.LocalClient.PlayerObject.GetComponent<Wizard>().Spellbook.Spell.MaxCooldown;
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(transform.localScale.x,
            transform.localScale.y - (0.8f / Cooldown * Time.deltaTime));
        if (transform.localScale.y < 0)
            Destroy(gameObject);
    }

    public enum AbilitySlot
    {
        Dash = 0,
        Primary = 1,
        Secondary = 2
    }
}
