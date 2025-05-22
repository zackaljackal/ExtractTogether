using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellbookOfTornado : Spellbook
{
    public override Rarity Rarity { get { return Rarity.Rare; } }

    public override Spell Spell
    {
        get { return SpellSingleton.Instance.Tornado; }
    }

    public override EquipmentId Id { get { return EquipmentId.SpellbookOfTornado; } }

    public override void Cast(Vector3 castLocation, Vector3 dir, Wizard caster)
    {
        Spell.GetComponent<Tornado>().Cast(dir, caster);
    }

    public override Sprite Sprite
    {
        get
        {
            Sprite[] wands = Resources.LoadAll<Sprite>("Wands");
            foreach (Sprite wand in wands)
            {
                if (wand.name == "SpellbookOfTornado")
                    return wand;
            }
            return null;
        }
    }
}
