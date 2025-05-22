using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandOfWindGust : Wand
{
    public override Rarity Rarity { get { return Rarity.Rare; } }

    public override Spell Spell
    {
        get { return SpellSingleton.Instance.WindGust; }
    }
    public override EquipmentId Id { get { return EquipmentId.WandOfWindGust; } }
    public override void Cast(Vector3 castLocation, Vector3 dir, Wizard caster)
    {
        Spell.GetComponent<WindGust>().Cast(dir, caster);
    }
    public override Sprite Sprite
    {
        get
        {
            Sprite[] wands = Resources.LoadAll<Sprite>("Wands");
            foreach (Sprite wand in wands)
            {
                if (wand.name == "WindGustWand")
                    return wand;
            }
            return null;
        }
    }
}
