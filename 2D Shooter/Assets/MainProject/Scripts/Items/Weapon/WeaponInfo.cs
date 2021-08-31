using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Weapon")]
public class WeaponInfo : ScriptableObject
{
    [SerializeField] private string nameWeapon;
    [SerializeField] private int damage;
    [SerializeField] private float rateOfFire;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float distance;

    public string NameWeapon { get => nameWeapon; set => nameWeapon = value; }
    public int Damage { get => damage; set => damage = value; }
    public float RateOfFire { get => rateOfFire; set => rateOfFire = value; }
    public float RotateSpeed { get => rotateSpeed; set => rotateSpeed = value; }
    public float Distance { get => distance; set => distance = value; }
}
