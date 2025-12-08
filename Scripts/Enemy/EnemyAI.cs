using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyAI : MonoBehaviour
{
    private Vector3 _initialPosition;
    private bool _isPlayerInWalkable = false;

    public int enemyHP = 100;
    private float currentEHP;
    private float maxEHP;
    private HealthUI healthUI;

    [SerializeField] private GameObject enemy_Sprites;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private NavMeshAgent navAgent;
    private Transform _playerPosition;

    [Header("적 공격 오브젝트")]
    [SerializeField] private GameObject punchAttackCollision;
    [SerializeField] private GameObject rotateAttackCollision;

    [Header("이벤트용 패널")]
    public GameObject FadeOut;
    public GameObject FoodMissionPanel;
    public GameObject IngameFoodMissionPanel;
    public GameObject IngameHappyEndWolfPanel;

    [Header("발자국 이펙트")]
    [SerializeField] private ParticleSystem footStepEffect;
    private ParticleSystem.EmissionModule footEmission;

    private bool isAttacking = false;
    public int attackTimeCheck = 0;

    [Header("도망 설정")]
    [SerializeField] private float escapeDistance = 5f;        [SerializeField] private float escapeSpeed = 5f;       
    public bool firstMeet = false;

    public bool firstSad = false;

    public bool happyEnd = false;

    private bool escapeCheck = false;
    private bool firstDie = false;

    private void Start()
    {
        maxEHP = enemyHP;
        currentEHP = maxEHP;

        footEmission = footStepEffect.emission;
        healthUI = GetComponent<HealthUI>();
        anim = enemy_Sprites.GetComponent<Animator>();
        spriteRenderer = enemy_Sprites.GetComponent<SpriteRenderer>();
        navAgent = GetComponent<NavMeshAgent>();
        _playerPosition = GameObject.Find("PYSpot").transform;

        _initialPosition = transform.position;

        navAgent.isStopped = true;
        ChangeAnim("Idle");
    }

    private void Update()
    {
        if (!firstMeet)
            return;

        if (enemyHP <= 0 && !firstDie)
        {
            Die();
            return;
        }

                 if (navAgent.velocity.sqrMagnitude > 0.01f)
            SpriteFlipX(navAgent.velocity.x);

        if (happyEnd)
        {
            StopAllCoroutines();
            StartCoroutine(ReturnToInitialPosition());
            happyEnd = false;
        }

        if (enemyHP <= 40)
        {
            if (!escapeCheck)
            {
                escapeCheck = true;
                StopAllCoroutines();
                QuestSystem.Instance.FoodMissionFirstActive();
                StartCoroutine(EscapeCoroutine());
                IngameFoodMissionPanel.SetActive(true);
                firstSad = true;
            }
            return;
        }


        bool playerOnWalkable = IsPlayerOnWalkable();

        if (playerOnWalkable && !_isPlayerInWalkable)
        {
            _isPlayerInWalkable = true;
            navAgent.isStopped = false;
            StartCoroutine(MoveCoroutine());
        }
        else if (!playerOnWalkable && _isPlayerInWalkable)
        {
            _isPlayerInWalkable = false;
            StartCoroutine(ReturnToInitialPosition());
        }

                 if (attackTimeCheck >= 4)
        {
            _isPlayerInWalkable = true;              isAttacking = false;
            attackTimeCheck = 0;
            StopAllCoroutines();
            StartCoroutine(AttackMakeSheep());
        }

     
    
    }

    public void HappyEnd()
    {
        happyEnd = true;
    }

    private IEnumerator MoveCoroutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.1f);

        while (_isPlayerInWalkable && !isAttacking)
        {
            if (_playerPosition == null) yield break;

            float distance = Vector3.Distance(transform.position, _playerPosition.position);
            navAgent.SetDestination(_playerPosition.position);

            if (distance <= 2f && !isAttacking)
            {
                StartCoroutine(AttackCoroutine());
                yield break;
            }

            if (navAgent.remainingDistance > navAgent.stoppingDistance + 0.05f)
            {
                ChangeAnim("Walk");
            }
            else if (navAgent.velocity.sqrMagnitude < 0.05f)
            {
                ChangeAnim("Idle");
            }
            yield return wait;
        }
    }

    private IEnumerator EscapeCoroutine()
    {
        spriteRenderer.color = Color.white;
        escapeCheck = true;
        isAttacking = true;
        navAgent.speed = escapeSpeed;
        navAgent.isStopped = false;

        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (enemyHP <= 40 && _playerPosition != null)
        {
            float distance = Vector3.Distance(transform.position, _playerPosition.position);

                         Vector3 dir = (transform.position - _playerPosition.position).normalized;
            Vector3 targetPos = _playerPosition.position + dir * escapeDistance;

            if (NavMesh.SamplePosition(targetPos, out NavMeshHit hit, 2f, NavMesh.AllAreas))
            {
                navAgent.SetDestination(hit.position);
            }

                         if (navAgent.velocity.sqrMagnitude > 0.1f)
                ChangeAnim("SadWalk");
            else
                ChangeAnim("SadIdle");

            yield return wait;
        }

                 navAgent.isStopped = true;
        ChangeAnim("SadIdle");
        isAttacking = false;
    }


    private IEnumerator ReturnToInitialPosition()
    {
        navAgent.isStopped = false;
        navAgent.SetDestination(_initialPosition);

        while (Vector3.Distance(transform.position, _initialPosition) > 0.1f)
        {
            ChangeAnim("Walk");
            yield return null;
        }

        navAgent.isStopped = true;
        ChangeAnim("Idle");
    }

    private IEnumerator AttackCoroutine()
    {
        isAttacking = true;
        navAgent.isStopped = true;

        int attackIndex = Random.Range(1, 4); 
        string trigger = $"Attack0{attackIndex}";
        anim.SetTrigger(trigger);
        Debug.Log(trigger);
        attackTimeCheck++;
        yield return new WaitForSeconds(1f);

        if (navAgent.velocity.sqrMagnitude < 0.05f)
        {
            ChangeAnim("Idle");
        }

        float distance = Vector3.Distance(transform.position, _playerPosition.position);

        if (distance <= 2f)
        {
            yield return new WaitForSeconds(2f);
            StartCoroutine(AttackCoroutine());
            yield break;
        }
        else
        {
           
            isAttacking = false;
            navAgent.isStopped = false;
            StartCoroutine(MoveCoroutine());
            yield break;
        }
      

    }

    private IEnumerator AttackMakeSheep()
    {
        ChangeAnim("Idle");
        isAttacking = true;
        navAgent.isStopped = true;
    

        StartCoroutine(BulletAttack.Instance.MakeBulletTime());
        yield return new WaitForSeconds(4f);

        StartCoroutine(BulletAttack.Instance.MakeSheepTime());
        isAttacking = false;

        navAgent.isStopped = false;
        StartCoroutine(MoveCoroutine());
    }

    private bool IsPlayerOnWalkable()
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(_playerPosition.position, out hit, 0.3f, NavMesh.AllAreas))
        {
            int walkableMask = 1 << NavMesh.GetAreaFromName("Walkable");
            return (hit.mask & walkableMask) != 0;
        }
        return false;
    }

    private void SpriteFlipX(float x)
    {
        Vector3 currentScale = enemy_Sprites.transform.localScale;
        currentScale.x = x > 0 ? Mathf.Abs(currentScale.x) : -Mathf.Abs(currentScale.x);
        enemy_Sprites.transform.localScale = currentScale;
    }

    private void ChangeAnim(string trigger)
    {
        anim.ResetTrigger("Idle");
        anim.ResetTrigger("Walk");
        anim.ResetTrigger("Attack01");
        anim.ResetTrigger("Attack02");
        anim.ResetTrigger("Attack03");
        anim.SetTrigger(trigger);

        OnFootStepEffect(trigger == "Walk");
    }

    private void Die()
    {
        spriteRenderer.color = Color.white;
        firstDie = true;
        StopAllCoroutines();
        navAgent.isStopped = true;
        ChangeAnim("Death");
        StartCoroutine(FadeOutSceneChanger("NormalEnd"));
             }

    private IEnumerator FadeOutSceneChanger(string scene)
    {
        yield return new WaitForSeconds(4f);
        FadeOut.SetActive(true);
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(scene);
    }

    public void OnFootStepEffect(bool isMoved)
    {
        footEmission.rateOverTime = isMoved ? 20 : 0;
    }

    public void TakeDamage(int damage)
    {
        if (enemyHP <= 0) return;
        anim.SetTrigger("Hit");
        enemyHP -= damage;
        currentEHP -= damage;
        healthUI.SetHealth(currentEHP, maxEHP);

        StartCoroutine(HitColor());
    }

    private IEnumerator HitColor()
    {
        spriteRenderer.color = new Color(0.6f, 0.07f, 0.1f);
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
    }
}
