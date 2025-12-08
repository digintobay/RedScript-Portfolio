using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackCollisionCall : MonoBehaviour
{
    [SerializeField] GameObject _enemyPunchAttackCollision;
    [SerializeField] GameObject _enemyRotateAttackCollision;


    public void AttackPunchCollisionCall()
    {
        _enemyPunchAttackCollision.SetActive(true);
    }

    public void AttackRotateCollisionCall()
    {
        _enemyRotateAttackCollision.SetActive(true);
    }
}
