using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class SpellSprites : MonoBehaviour
{
    public static SpellSprites Instance { get; private set; }
    private Sprite[] Spells;
    public Sprite Fireball { get; private set; }

    private void Awake()
    {
        
    }
}
