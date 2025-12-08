using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackCall : MonoBehaviour
{
    [SerializeField] GameObject _playerAttackCollision;


    public void AttackCollisionCall()
    {
        _playerAttackCollision.SetActive(true);
    }
}
