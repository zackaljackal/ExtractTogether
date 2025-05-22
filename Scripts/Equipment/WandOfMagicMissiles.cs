using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class WandOfMagicMissiles : Wand
{
    public override Rarity Rarity { get { return Rarity.Common; } }
    public override Spell Spell
    {
        get { return SpellSingleton.Instance.MagicMissile; }
    }
    public override EquipmentId Id { get { return EquipmentId.WandOfMagicMissiles; } }
    public override void Cast(Vector3 castLocation, Vector3 dir, Wizard caster)
    {
        Spell.GetComponent<MagicMissile>().Cast(dir, caster);
    }
    public override Sprite Sprite {
        get { 
            Sprite[] wands = Resources.LoadAll<Sprite>("Wands");
            foreach(Sprite wand in wands)
            {
                if (wand.name == "MagicMissileWand")
                    return wand;
            }
            return null;
        }
    }   
}
