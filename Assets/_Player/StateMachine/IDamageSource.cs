using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDamageSource : MonoBehaviour
{
    public float dmgAmount;
    int Damage { get; }
    GameObject Owner { get; }
}
