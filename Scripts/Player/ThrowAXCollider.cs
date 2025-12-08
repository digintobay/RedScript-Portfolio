using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowAXCollider : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        int damage = 10;

        if (other.CompareTag("Enemy"))
        {
            if (InventoryManager.Instance.attackPower)
            {
                damage += 7;
                other.GetComponent<EnemyAI>().TakeDamage(damage);
                StartCoroutine(AutoDisable());
            }
            else
            {
                other.GetComponent<EnemyAI>().TakeDamage(damage);
                StartCoroutine(AutoDisable());

            }

         
        }
        else if (other.CompareTag("SheepEnemy"))
        {
            if (InventoryManager.Instance.attackPower)
            {
                damage += 7;
                other.GetComponent<SheepEnemy>().TakeDamage(damage);
                StartCoroutine(AutoDisable());
            }
            else
            {
                other.GetComponent<SheepEnemy>().TakeDamage(damage);
                StartCoroutine(AutoDisable());

            }

        }
        else if (other.CompareTag("Rabbit"))
        {
            if (InventoryManager.Instance.attackPower)
            {
                damage += 7;


                other.GetComponent<RabbitAI>().TakeDamage(damage);
                StartCoroutine(AutoDisable());
            }
            else
            {
                other.GetComponent<RabbitAI>().TakeDamage(damage);
                StartCoroutine(AutoDisable());

            }

        }
    }

    private IEnumerator AutoDisable()
    {
        yield return new WaitForSeconds(0.1f);

        gameObject.SetActive(false);
    }
}
