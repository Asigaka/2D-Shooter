using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    [SerializeField] private Weapon currentWeapon;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private List<EnemyTarget> enemyTargets = new List<EnemyTarget>();

    private EnemyTarget _mainTarget;
    private bool _canRotate = false;
    private bool _canShoot = false;

    public List<EnemyTarget> EnemyTargets { get => enemyTargets; set => enemyTargets = value; }

    private void Start()
    {

    }

    private void Update()
    {
        FindMainTarget();

        if (currentWeapon != null && _mainTarget != null && _canRotate)
        {
            RotateWeaponToMainTarget();
        }
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

    }

    public void OnFireUp()
    {
        _canRotate = false;
        _canShoot = false;
    }

    private void RotateWeaponToMainTarget()
    {
        Vector2 direction = _mainTarget.transform.position - currentWeapon.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        currentWeapon.transform.rotation = Quaternion.Slerp(currentWeapon.transform.rotation, rotation, currentWeapon.Info.RotateSpeed * Time.deltaTime);
    }
}
