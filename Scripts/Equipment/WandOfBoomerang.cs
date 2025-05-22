using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class WandOfBoomerang : Wand
{
    public override Rarity Rarity { get { return Rarity.Rare; } }
    public override Spell Spell
    {
        get { return SpellSingleton.Instance.Boomerang; }
    }
    public override EquipmentId Id { get { return EquipmentId.WandOfBoomerang; } }

    public override void Cast(Vector3 castLocation, Vector3 dir, Wizard caster)
    {
        Spell.GetComponent<Boomerang>().Cast(dir, caster);
    }
    public override Sprite Sprite
    {
        get
        {
            Sprite[] wands = Resources.LoadAll<Sprite>("Wands");
            foreach (Sprite wand in wands)
            {
                if (wand.name == "BoomerangWand")
                    return wand;
            }
            return null;
        }
    }
}
