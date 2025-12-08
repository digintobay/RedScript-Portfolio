using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RabbitAI : MonoBehaviour
{
    public int enemyHP = 11;

    [SerializeField]
    private GameObject enemy_Sprites;
    private Animator anim;
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private NavMeshAgent navAgent;
    [SerializeField] private float escapeDistance = 5f; [SerializeField] private float runDistance = 2f;

    private Transform _playerPosition;

    public enum EnemyAnimState { Idle, Walk, RunJump, Death }
    private EnemyAnimState _currentAnimState;

    public bool enemyDeathCheck = false;

    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
        anim = enemy_Sprites.GetComponent<Animator>();
        spriteRenderer = enemy_Sprites.GetComponent<SpriteRenderer>();
        _playerPosition = GameObject.Find("PYSpot").transform;
    }

    private void Start()
    {
        ChangeAnimState(EnemyAnimState.Idle);
        StartCoroutine(EscapeCoroutine());
    }

    void Update()
    {

        if (navAgent.velocity.sqrMagnitude > 0.01f)
        {
            SpriteFlipX(navAgent.velocity.x);
        }

        if (enemyHP <= 0 && !enemyDeathCheck)
        {
            spriteRenderer.color = new Color(1, 1, 1, 1);
            navAgent.isStopped = true;
            StopAllCoroutines();
            StartCoroutine(Death());
            enemyDeathCheck = true;


        }

    }

    public void ChangeAnimState(EnemyAnimState newState)
    {
        if (_currentAnimState == newState) return;
        _currentAnimState = newState;

        switch (_currentAnimState)
        {
            case EnemyAnimState.Idle:
                anim.SetTrigger("Idle");
                break;
            case EnemyAnimState.Walk:
                anim.SetTrigger("Walk");
                break;
            case EnemyAnimState.RunJump:
                anim.SetTrigger("RunJump");
                break;
            case EnemyAnimState.Death:
                anim.SetTrigger("Death");
                break;


        }
    }

    private IEnumerator EscapeCoroutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.1f);

        while (_playerPosition != null)
        {
            float distance = Vector3.Distance(transform.position, _playerPosition.position);
            Vector3 dir = (transform.position - _playerPosition.position).normalized;
            Vector3 runTo = _playerPosition.position + dir * escapeDistance;

            EnemyAnimState nextState = EnemyAnimState.Idle;
            if (distance < runDistance)
            {
                if (NavMesh.SamplePosition(runTo, out NavMeshHit hit, 2.0f, NavMesh.AllAreas))
                {
                    navAgent.speed = 4f;
                    navAgent.isStopped = false;
                    navAgent.SetDestination(hit.position);
                    nextState = EnemyAnimState.RunJump;
                }
            }
            else if (distance < escapeDistance)
            {
                if (NavMesh.SamplePosition(runTo, out NavMeshHit hit, 2.0f, NavMesh.AllAreas))
                {
                    navAgent.speed = 2f;
                    navAgent.isStopped = false;
                    navAgent.SetDestination(hit.position);
                    nextState = EnemyAnimState.Walk;
                }
            }
            else
            {
                navAgent.isStopped = true;
                nextState = EnemyAnimState.Idle;
            }

            if (navAgent.velocity.sqrMagnitude < 0.01f)
            {
                nextState = EnemyAnimState.Idle;
            }

            if (_currentAnimState != nextState)
            {
                ChangeAnimState(nextState);
                _currentAnimState = nextState;
            }

            yield return wait;
        }

    }




    public void TakeDamage(int damage)
    {
        enemyHP -= damage;
        StartCoroutine(HitColor());
    }

    private IEnumerator Death()
    {
        ChangeAnimState(EnemyAnimState.Death);
        yield return new WaitForSeconds(2f);
        MoneyManager.Instance.AddMoney(1);
        Destroy(gameObject);

    }

    private IEnumerator HitColor()
    {
        spriteRenderer.color = new Color(155 / 255f, 17 / 255f, 30 / 255f);
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }


    private void SpriteFlipX(float x)
    {
        Vector3 currentScale = enemy_Sprites.transform.localScale;
        currentScale.x = x > 0 ? Mathf.Abs(currentScale.x) : -Mathf.Abs(currentScale.x);
        enemy_Sprites.transform.localScale = currentScale;

    }


}
