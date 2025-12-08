using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static EnemyAI;

public class SheepEnemy : MonoBehaviour
{
    public int enemyDamage = 1;
    public int enemyHP = 1;

    [SerializeField]
    private GameObject enemy_Sprites;
    private Animator anim;
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private NavMeshAgent navAgent;

    private Transform _playerPosition;

    private float lifeTime = 0f;
    private bool isDying = false;

    public enum EnemyAnimState { Idle, Start, Walk, Die }
    private EnemyAnimState _currentAnimState;


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
        StartCoroutine(MoveCoroutine());
    }

    private void Update()
    {

        lifeTime += Time.deltaTime;

        if (!isDying && lifeTime >= 15f)
        {
            isDying = true;
            navAgent.isStopped = true;
            StopAllCoroutines();
            StartCoroutine(DeathByTime());
        }

        if (navAgent.velocity.sqrMagnitude > 0.01f)
        {
            SpriteFlipX(navAgent.velocity.x);
        }

        if (enemyHP <= 0 && !isDying)
        {
            navAgent.isStopped = true;
            StopAllCoroutines();
            StartCoroutine(Death());
            enemyHP = 1;
        }

    }


    private IEnumerator DeathByTime()
    {
        ChangeAnimState(EnemyAnimState.Die);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }


    private IEnumerator MoveCoroutine()
    {
        Debug.Log("실행됨");
        WaitForSeconds wait = new WaitForSeconds(0.1f);

        yield return new WaitForSeconds(5f);

        ChangeAnimState(EnemyAnimState.Start);

        yield return null;

        while (_playerPosition != null)
        {
            float distance = Vector3.Distance(transform.position, _playerPosition.position);


            MoveToPlayer();

            if (navAgent.velocity.sqrMagnitude > 0.01f)
            {
                Debug.Log("양 워크 실행 중");
                ChangeAnimState(EnemyAnimState.Walk);
            }

            yield return wait;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_currentAnimState == EnemyAnimState.Walk && other.CompareTag("Player"))
        {
            other.GetComponent<PlayerMove>().TakeDamage(enemyDamage);
            navAgent.isStopped = true;
            StopAllCoroutines();
            StartCoroutine(Death());
        }
    }

    private void MoveToPlayer()
    {
        if (_playerPosition != null)
        {
            navAgent.SetDestination(_playerPosition.position);
        }
    }

    public void TakeDamage(int damage)
    {
        enemyHP -= damage;
        StartCoroutine(HitColor());
    }

    private IEnumerator Death()
    {
        ChangeAnimState(EnemyAnimState.Die);
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);

    }

    private IEnumerator HitColor()
    {
        spriteRenderer.color = new Color(155 / 255f, 17 / 255f, 30 / 255f);
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }


    public void ChangeAnimState(EnemyAnimState newState)
    {
        if (_currentAnimState == newState) return;
        _currentAnimState = newState;

        switch (_currentAnimState)
        {
            case EnemyAnimState.Start:
                anim.SetTrigger("Start");
                break;
            case EnemyAnimState.Walk:
                anim.SetTrigger("Walk");
                break;
            case EnemyAnimState.Die:
                anim.SetTrigger("Death");
                break;

                ;
        }
    }

    private void SpriteFlipX(float x)
    {
        Vector3 currentScale = enemy_Sprites.transform.localScale;
        currentScale.x = x > 0 ? Mathf.Abs(currentScale.x) : -Mathf.Abs(currentScale.x);
        enemy_Sprites.transform.localScale = currentScale;

    }

}
