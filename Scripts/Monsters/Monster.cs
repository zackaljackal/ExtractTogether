using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Monster : NetworkBehaviour
{
    public abstract int Health { get; }
    public abstract float MovementSpeed { get; }
}
