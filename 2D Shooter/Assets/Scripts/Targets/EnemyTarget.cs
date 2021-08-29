using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTarget : MonoBehaviour
{
    private PlayerCombatController _combatController;

    private void Start()
    {
        _combatController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombatController>();
    }

    private void OnBecameVisible()
    {
        _combatController.EnemyTargets.Add(this);
    }

    private void OnBecameInvisible()
    {
        _combatController.EnemyTargets.Remove(this);
    }
}
