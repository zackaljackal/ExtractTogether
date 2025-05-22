using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;

public abstract class Equipment : NetworkBehaviour
{
    public abstract Rarity Rarity { get; }
    public abstract EquipmentId Id { get; }
    public abstract Sprite Sprite { get; }
}

public abstract class Wand : Equipment
{
    public abstract Spell Spell { get; }

    public abstract void Cast(Vector3 castLocation, Vector3 dir, Wizard caster);

}

public abstract class Spellbook : Equipment
{
    public abstract Spell Spell { get; }
    public abstract void Cast(Vector3 castLocation, Vector3 dir, Wizard caster);
}

public enum Rarity
{
    Common = 0,
    Rare = 1,
    Epic = 2,
    Legendary = 3
}

public enum EquipmentId
{
    WandOfMagicMissiles = 0,
    WandOfBoomerang = 1,
    WandOfWindGust = 2,
    SpellbookOfFireball = 25,
    SpellbookOfTornado = 26,
}