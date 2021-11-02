using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    [SerializeField] private Weapon currentWeapon;
    [SerializeField] private LayerMask enemyLayer;

    private float _localRateOfFire;
    private bool _reloaded;

    private void Update()
    {
        if (currentWeapon != null)
        {
            if (Input.GetButton("Fire1") && _reloaded)
                Shoot();
            else
                Reload();
        }
    }

    private void Shoot()
    {
        if (_reloaded)
        {
            RaycastHit2D hit = Physics2D.Raycast(currentWeapon.FirePoint.position, currentWeapon.FirePoint.up, currentWeapon.Info.Distance, enemyLayer);

            if (hit.collider != null)
            {
                Debug.Log(hit.collider.name);
            }

            _reloaded = false;
        }
    }

    private void Reload()
    {
        if (_localRateOfFire <= 0 && !_reloaded)
        {
            _localRateOfFire = 2;
            _localRateOfFire = currentWeapon.Info.RateOfFire;

            _reloaded = true;
        }
        else
        {
            _localRateOfFire -= Time.deltaTime;

            _reloaded = false;
        }
    }

    private void OnDrawGizmos()
    {
        if (currentWeapon != null)
            Gizmos.DrawRay(currentWeapon.FirePoint.position, currentWeapon.FirePoint.up * currentWeapon.Info.Distance);
    }
}
