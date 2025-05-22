using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSingleton : MonoBehaviour
{
    public static SpellSingleton Instance { get; private set; }

    private Spell[] Spells;
    
    public Spell MagicMissile { get; private set; }
    public Spell FireBall { get; private set; }
    public Spell Boomerang { get; private set; }
    public Spell WindGust { get; private set; }
    public Spell Tornado { get; private set; }


    private Wand[] Wands;
    private Spellbook[] Spellbooks;

    public Wand WandOfMagicMissiles { get; private set; }
    public Wand WandOfBoomerang { get; private set; }
    public Wand WandOfWindGust { get; private set; }
    public Spellbook SpellbookOfFireball { get; private set; }
    public Spellbook SpellbookOfTornado { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            Spells = Resources.LoadAll<Spell>("Spells");
            foreach(var spell in Spells)
            {
                switch (spell.name)
                {
                    case "MagicMissile":
                        MagicMissile = spell;
                        break;
                    case "Fireball":
                        FireBall = spell;
                        break;
                    case "Boomerang":
                        Boomerang = spell;
                        break;
                    case "WindGust":
                        WindGust = spell;
                        break;
                    case "Tornado":
                        Tornado = spell;
                        break;
                    default:
                        break;
                }
            }
            Wands = Resources.LoadAll<Wand>("Equipment");
            foreach(var gear in Wands)
            {
                switch (gear.name)
                {
                    case "WandOfMagicMissiles":
                        WandOfMagicMissiles = gear;
                        break;
                    case "WandOfBoomerang":
                        WandOfBoomerang = gear;
                        break;
                    case "WandOfWindGust":
                        WandOfWindGust = gear;
                        break;
                    default:
                        break;
                }
            }
            Spellbooks = Resources.LoadAll<Spellbook>("Equipment");
            foreach(var gear in Spellbooks)
            {
                switch (gear.name)
                {
                    case "SpellbookOfFireball":
                        SpellbookOfFireball = gear;
                        break;
                    case "SpellbookOfTornado":
                        SpellbookOfTornado = gear;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
