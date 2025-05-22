using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellbookOfFireball : Spellbook
{
    public override Rarity Rarity { get { return Rarity.Common; } }

    public override Spell Spell
    {
        get { return SpellSingleton.Instance.FireBall; }
    }

    public override EquipmentId Id { get { return EquipmentId.SpellbookOfFireball; } }

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
                if (wand.name == "SpellbookOfFireball")
                    return wand;
            }
            return null;
        }
    }
}
