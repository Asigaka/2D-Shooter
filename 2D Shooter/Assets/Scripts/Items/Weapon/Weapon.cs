using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponInfo info;
    [SerializeField] private Transform firePoint;

    public WeaponInfo Info { get => info; set => info = value; }
    public Transform FirePoint { get => firePoint; set => firePoint = value; }
}
