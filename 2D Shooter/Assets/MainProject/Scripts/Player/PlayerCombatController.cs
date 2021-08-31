using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    [SerializeField] private Weapon currentWeapon;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private List<EnemyTarget> enemyTargets = new List<EnemyTarget>();

    private PlayerController _pc;
    private EnemyTarget _mainTarget;

    private float _localRateOfFire;
    private bool _canRotate = false;
    private bool _canShoot = false;
    private bool _reloaded = true;
    private bool _targetAimMode = false;
    private bool _defaultAimMode = true;

    public List<EnemyTarget> EnemyTargets { get => enemyTargets; set => enemyTargets = value; }

    private void Start()
    {
        _pc = GetComponent<PlayerController>();
    }

    private void Update()
    {
        FindMainTarget();

        if (currentWeapon != null)
        {
            if (!_reloaded)
                Reload();

            if (_mainTarget != null)
                RotateWeaponToMainTarget();

            if (_canShoot)
                Shoot();
        }
    }

    public void SwitchAimMode()
    {

    }

    private EnemyTarget FindMainTarget()
    {
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (EnemyTarget target in enemyTargets)
        {
            Vector3 diff = target.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                _mainTarget = target;
                distance = curDistance;
            }
        }
        return _mainTarget;
    }

    public void OnFireDown()
    {
        _canRotate = true;
        _canShoot = true;
    }

    private void Shoot()
    {
        if (_canShoot)
        {
            if (_reloaded)
            {
                RaycastHit2D hit = Physics2D.Raycast(currentWeapon.FirePoint.position, currentWeapon.FirePoint.right, currentWeapon.Info.Distance, 7);

                if (hit.collider != null)
                {
                    if (hit.collider.GetComponent<EnemyTarget>())
                    {
                        EnemyTarget target = hit.collider.GetComponent<EnemyTarget>();

                        Debug.Log(hit.collider.name);
                    }
                }

                _reloaded = false;
            }
        }
    }

    private void Reload()
    {
        if (_localRateOfFire <= 0 && !_reloaded)
        {
            _localRateOfFire = currentWeapon.Info.RateOfFire;

            _reloaded = true;
        }
        else
        {
            _localRateOfFire -= Time.deltaTime;

            _reloaded = false;
        }
    }

    public void OnFireUp()
    {
        _canRotate = false;
        _canShoot = false;
    }

    private void RotateWeaponToMainTarget()
    {
        if (_canRotate)
        {
            Vector2 direction = _mainTarget.transform.position - currentWeapon.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            currentWeapon.transform.rotation = Quaternion.Slerp(currentWeapon.transform.rotation, rotation, currentWeapon.Info.RotateSpeed * Time.deltaTime);
        }
        else
        {
            currentWeapon.transform.rotation = Quaternion.Slerp(currentWeapon.transform.rotation, Quaternion.Euler(0, 0, 0), currentWeapon.Info.RotateSpeed * Time.deltaTime);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(currentWeapon.FirePoint.position, currentWeapon.FirePoint.right * currentWeapon.Info.Distance);
    }
}
