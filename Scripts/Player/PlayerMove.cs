using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    public GameObject Player_Sprites;
    public GameObject Player_AttackCollision;


    [Header("움직임 관련 제어 변수")]
    public static bool allowControll { get; private set; }
    public float moveSpeed;
    public bool moveStop = false;
    public bool death = false;

    [Header("캐릭터 체력")]
    [SerializeField] private int playerHP;
    // ui 적용 current 헬스, 맥스 헬스값
    private float currentHP;
    private float maxHP;
    private HealthUI healthUI;

    [Header("이벤트용 패널")]
    public GameObject FadeOut;

    [Header("SFX 이펙트 효과음")]
    public AudioClip SFXattack;

    [Header("파티클 관련 이펙트")]
    [SerializeField]
    private ParticleSystem footStepEffect;
    private ParticleSystem.EmissionModule footEmission;



    /// <summary>
    /// pirvate값 변수들
    /// </summary>
    private Rigidbody rigid;
    private Animator anim;
    private Material material;
    private SpriteRenderer spriteRender;
    private ThrowAX throwAX;

    private bool[] firstcheck = new bool[10];
    //private SpriteRenderer[] sprites;


    private void Awake()
    {
        

        PlayerStruct();
        ComponentGroup();
      
        for(int i=0; i<firstcheck.Length; i++)
        {
            firstcheck[i] = false;
        }
    }

    private void Update()
    {

        if (moveStop) return;

        Move();




        if (playerHP <= 0 && death == false)
        {
            StopAllCoroutines();
            anim.SetTrigger("Death");
            moveStop = true;
            rigid.constraints = RigidbodyConstraints.FreezeAll;
            gameObject.tag = "Invincibility";

            StartCoroutine(FadeOutSceneChanger("NoneTutorials"));
            death = true;

        }


        if (anim.GetBool("Collect"))
            return;



        if (Input.GetMouseButtonDown(0))
        {
            if (!InventoryManager.Instance.IsItemActive("ax"))
                return;

            if (!firstcheck[0])
            {
                QuestSystem.Instance.AxcMission("leftax");
                firstcheck[0] = true;
            }

            Attack();
        }

        if (Input.GetMouseButtonDown(1)) // 마우스 오른쪽 클릭
        {
            if (!InventoryManager.Instance.IsItemActive("ax"))
                return;

            if (!firstcheck[1])
            {
                QuestSystem.Instance.AxcMission("rightax");
                firstcheck[1] = true;
            }

            ThrowAttack();
            throwAX.ThrowObject();
            InventoryManager.Instance.SetItemActive("ax", false);
            Debug.Log(InventoryManager.Instance.IsItemActive("ax"));
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {



            var potioncheck = InventoryManager.Instance.IsItemActive("potion");
            if (!potioncheck) return;
            else
            {
                //포션 사용, AddItem으로 false시 개수 감소
                InventoryManager.Instance.AddItem("potion", false);
                //포션 ui 개수 변경
                InventoryManager.Instance.potionText.text =
                    InventoryManager.Instance.GetItemCount("potion").ToString();

                playerHP += (int)7;
                currentHP += (int)7;
                playerHP = Mathf.Clamp(playerHP, 0, 100);
                currentHP = Mathf.Clamp(currentHP, 0, 100);
                healthUI.SetHealth(currentHP, maxHP);
                
                if (!InventoryManager.Instance.IsItemActive("potion"))
                {
                    InventoryManager.Instance.potionUI.SetActive(false);
                }
                
            }
            
  
          

        }
    }


    /// <summary>
    /// 플레이어 정보 함수 모음
    /// </summary>
    private void PlayerStruct()
    {
        playerHP = 100;
        maxHP = playerHP;
        currentHP = maxHP;
    }

    /// <summary>
    /// 컴포넌트 그룹화
    /// </summary>
    private void ComponentGroup()
    {
        footEmission = footStepEffect.emission;
        healthUI = GetComponent<HealthUI>();

        spriteRender = Player_Sprites.GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody>();
        anim = Player_Sprites.GetComponent<Animator>();
        throwAX = GetComponent<ThrowAX>();

    }

    /// <summary>
    /// 캐릭터 움직임 컨트롤
    /// </summary>
    private void Move()
    {
        if (anim.GetBool("Collect"))
            return;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        horizontal *= 0.6f;

        Vector3 moveInput = new Vector3(horizontal, 0, vertical);
        bool isMove = moveInput.magnitude > 0;

        if (isMove)
        {
            anim.SetBool("Walk", true);
            anim.SetBool("Idle", false);
            Vector3 moveDir = Camera.main.transform.TransformDirection(moveInput);

            SpriteFlipX(moveDir.x);

            moveDir.y = 0;
            moveDir.Normalize();

            rigid.MovePosition(transform.position + moveDir * moveSpeed * Time.deltaTime);

            OnFootStepEffect(true);
        }
        else if (moveInput.magnitude ==0)
            {
            anim.SetBool("Walk", false);
            anim.SetBool("Idle", true);
            OnFootStepEffect(false);
        }
    }

    private void Attack()
    {
       
            StartCoroutine(StopMoving(0.8f));
        SoundManager.instance.SFXPlay("AttackAX", SFXattack);
        anim.SetTrigger("Attack");

      
    }

    private void ThrowAttack()
    {
        StartCoroutine(StopMoving(0.8f));
        SoundManager.instance.SFXPlay("AttackAX", SFXattack);
        anim.SetTrigger("Throw");
    }

    public void TakeDamage(int damage)
    {
        if (InventoryManager.Instance.shield && damage >= 7)
        {
            damage -= 7;
        }

        playerHP -= damage;
        currentHP -= damage;

        if (damage >= 13)
        {
            StartCoroutine(StopMoving(2.5f));
            // 위로 뜨는 넉백
            Knockback(3f, 0.5f, true);
            anim.SetTrigger("NuckHit");
            StartCoroutine(PlayerInvincibility()); // 유저 무적 상태
        }
        else
        {
            StartCoroutine(StopMoving(0.8f));
            // 평면 넉백
            Knockback(0.4f, 0.2f, false);
            anim.SetTrigger("Hit");
        }

        StartCoroutine(HitColor());
        healthUI.SetHealth(currentHP, maxHP);
    }

    private IEnumerator HitColor()
    {
        spriteRender.color = new Color(155 / 255f, 17 / 255f, 30 / 255f);
        yield return new WaitForSeconds(0.2f);
        spriteRender.color = new Color(200 / 255f, 199 / 255f, 176 / 255f, 1);
    }

    private void SpriteFlipX(float x)
    {
        Vector3 currentScale = spriteRender.transform.localScale;
        currentScale.x = x < 0 ? -1f : 1f;
        
        if (currentScale.x < 0)
        {
            Player_AttackCollision.transform.localPosition = new Vector3 (-5.5f, 4f, 0);
        }
        else if (currentScale.x > 0)
        {
            Player_AttackCollision.transform.localPosition = new Vector3(5.5f, 4f, 0);
        }

        spriteRender.transform.localScale = currentScale;
    }

    private IEnumerator StopMoving(float time)
    {
        moveStop = true;
        yield return new WaitForSeconds(time);
        moveStop = false;
        
    }

    public void Knockback(float knockbackDistance, float knockbackTime, bool includeY = false)
    {
        Vector3 lookDir = (spriteRender.transform.localScale.x > 0) ? Vector3.right : Vector3.left;
        Vector3 knockDir = -lookDir;

        if (includeY)
            knockDir += Vector3.up * 0.5f; // Y값 원하는 만큼 추가

        StartCoroutine(KnockbackRoutine(knockDir, knockbackDistance, knockbackTime));
    }

    public IEnumerator KnockbackRoutine(Vector3 hitDirection, float knockbackDistance, float knockbackTime)
    {
        float elapsed = 0f;
        Vector3 start = transform.position;
        Vector3 end = transform.position + hitDirection.normalized * knockbackDistance;

        while (elapsed < knockbackTime)
        {
            transform.position = Vector3.Lerp(start, end, elapsed / knockbackTime);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = end;
    }

    public IEnumerator PlayerInvincibility()
    {
        gameObject.tag = "Invincibility";

        for (int i = 0; i < 10; i++) 
        {
            spriteRender.color = new Color(200/255f, 199/255f, 176/255f, 0.5f);
            yield return new WaitForSeconds(0.2f);
            spriteRender.color = new Color(200/255f, 199/255f, 176/255f, 1f);
            yield return new WaitForSeconds(0.2f);
            Debug.Log("반복 횟수");
        }
     

        gameObject.tag = "Player";
    }

    public void OnFootStepEffect(bool isMoved)
    {
        footEmission.rateOverTime = isMoved == true ? 20 : 0;
    }

    private IEnumerator FadeOutSceneChanger(string scene)
    {
        yield return new WaitForSeconds(4f);
        FadeOut.SetActive(true);
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(scene);

    }

    public void PlayerIdle()
    {
        anim.SetBool("Idle", true);
        anim.SetBool("Walk", false);
    }

}
