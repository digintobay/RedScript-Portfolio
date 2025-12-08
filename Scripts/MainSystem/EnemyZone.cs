
using UnityEngine;

public class EnemyZone : MonoBehaviour
{
    [SerializeField]private string uiMethod;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
                         var method = typeof(CombatPanelManager).GetMethod(uiMethod);
            method?.Invoke(CombatPanelManager.Instance, null);
        }
    }
}
