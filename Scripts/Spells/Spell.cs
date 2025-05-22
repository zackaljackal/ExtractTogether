using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    public abstract float MaxCooldown { get; set; }
    public abstract void Cast(Vector3 dir, Wizard caster);

    public abstract Sprite Sprite { get; }
}
