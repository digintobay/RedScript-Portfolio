using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackCollision : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine("AutoDisable");
    }

    private void OnTriggerEnter(Collider other)
    {
        int damage = 2;

        if (other.CompareTag("Enemy"))
        {
            if (InventoryManager.Instance.attackPower)
            {
                damage += 7;
            }

            other.GetComponent<EnemyAI>().TakeDamage(damage);
        }
        else if (other.CompareTag("SheepEnemy"))
        {
            if (InventoryManager.Instance.attackPower)
            {
                damage += 7;
            }

            other.GetComponent<SheepEnemy>().TakeDamage(damage);
        }
        else if (other.CompareTag("Rabbit"))
        {
            if (InventoryManager.Instance.attackPower)
            {
                damage += 7;
            }
            other.GetComponent<RabbitAI>().TakeDamage(damage);
       
        }
    }

    private IEnumerator AutoDisable()
    {
        yield return new WaitForSeconds(0.1f);

        gameObject.SetActive(false);
    }
}
