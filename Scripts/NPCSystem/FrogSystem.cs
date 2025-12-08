using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class FrogSystem : MonoBehaviour
{

    public bool frogTriggerCheck = false;
    public GameObject[] frogs;
    public GameObject[] frogTextBox;
    public TextMeshPro[] frogTalk;
    private string[] frogText = { "있잖여, 저번에 공주 찾아 돌아댕기다가 죽을 뻔했다 그랬잖어",
    "응, 그랬지.",
    "너 그때 장난도 아니었슈! 개구리 가죽이 다 터져 가지구!",
    "아이, 근데 숲 안쪽 들어가니께 뭐 특효약이라고 좋은 걸 팔드라고?",
    "어엉.",
    "으응?",
    "그거 쭈우우욱...",
    "쭈우우욱?",
    "쭉? 쭉?",
    "쭈우우우우우욱...",
    "쭈욱?",
    "마시다 날밤 깐 겨?",
    "들이켜니까 바로 눈앞이 화해지는 게 기운이 넘치드라고!",
    "사기 아녀유?",
    "그거 사기여 이 개구리야!",
    "아녀어! 속고만 살았나!"};

    public bool froghint = false;
    public bool checkFrog = false;

    private string[] wolfFrogText =
    {
        "저어어기 보닝께 누가 음식을 버려놓고 갔드만.",
"누가 칠칠치 못하게 저거를 다 안 치우고 간 겨.",
"저런 거 다 먹으려면 덩치가 다 커야 할겨.",
"저 퍼런 놈이 우리 말고 길바닥에 널린 것들이나 좀 먹어주면 좋겠구먼유!",
"음식 좀 누가 안 주워가는겨. 아, 저거도 다 먹을 수 있는 건디.",
"요즘 애덜은 음식 소중한 줄 모르는겨. 저 널린 음식들 좀 보셔유.",
"저 음식들은 누가 먹으려고 저렇게 있는지 몰러.",
"그거 알간? 저 음식 함부로 먹으면 위험하다는 거.",
"개구리가 쿠키를 먹으면 꿱 해. 꿱.",
"저건 우리 먹으라고 있는 거 아녀. 다른 놈이지…. 퍼렇거나, 퍼렇거나, 퍼런 놈.",
"흙 묻은 음식을 좋아할 놈은 배고픈 놈 밖에 없을 겨.",
"저 퍼런 놈은 쿠키도 먹는다는데? 아주 난 놈이여!",
"배는 고픈디 길에 떨어진 거는 영 별로라 먹고 싶지가 않단 말이여….",
"거기 네버 양반은 음식은 안 받유? 코인은 없고 가는 길에 음식을 주워가야겄슈.",
"그니께 저 음식들이 주인이 없다는 말이여유?",
"그렇다니께."
    };

    private Animator[] anims = new Animator[3];

    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            anims[i] = frogs[i].GetComponent<Animator>();

        }


    }

    public void GetForgText()
    {
        froghint = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && froghint)
        {
            StartCoroutine(FrogAnoTalkBasic());
            froghint = false;
            checkFrog = true;
        }


        if (other.CompareTag("Player") && !frogTriggerCheck)
        {
           
            frogTriggerCheck = true;

            if (checkFrog)
            {
                return;
            }

            StartCoroutine(FrogTalkBasic());
        }

      
    }

    private IEnumerator FrogTalkBasic()
    {
        var firstFrog = 0;
        var secondFrog = 1;
        var thirdFrog = 2;

        for (int i = 0; i < frogText.Length; i++)
        {
            if (i == 0)
            {
                frogTalk[firstFrog].text = frogText[i];
                frogTextBox[firstFrog].SetActive(true);
                anims[firstFrog].SetBool("Talk", true);
                continue;
            }

            if (i % 3 == 0)
            {
                yield return new WaitForSeconds(3f);
                AllFrogDown();
                frogTalk[firstFrog].text = frogText[i];
                frogTextBox[firstFrog].SetActive(true);
                anims[firstFrog].SetBool("Talk", true);
                continue;

            }

            if (i % 2 != 0)
            {
                yield return new WaitForSeconds(3f);
                AllFrogDown();
                frogTalk[thirdFrog].text = frogText[i];
                frogTextBox[thirdFrog].SetActive(true);
                anims[thirdFrog].SetBool("Talk", true);
                continue;

            }

            if (i % 2 == 0)
            {
                yield return new WaitForSeconds(3f);
                AllFrogDown();
                frogTalk[secondFrog].text = frogText[i];
                frogTextBox[secondFrog].SetActive(true);
                anims[secondFrog].SetBool("Talk", true);
                continue;

            }



        }


        yield return new WaitForSeconds(3f);
        AllFrogDown();
        yield return null;

    }

    private IEnumerator FrogAnoTalkBasic()
    {
        var firstFrog = 0;
        var secondFrog = 1;
        var thirdFrog = 2;

        for (int i = 0; i < wolfFrogText.Length; i++)
        {
            if (i == 0)
            {
                frogTalk[firstFrog].text = wolfFrogText[i];
                frogTextBox[firstFrog].SetActive(true);
                anims[firstFrog].SetBool("Talk", true);
                continue;
            }

            if (i % 3 == 0)
            {
                yield return new WaitForSeconds(3f);
                AllFrogDown();
                frogTalk[firstFrog].text = wolfFrogText[i];
                frogTextBox[firstFrog].SetActive(true);
                anims[firstFrog].SetBool("Talk", true);
                continue;

            }

            if (i % 2 != 0)
            {
                yield return new WaitForSeconds(3f);
                AllFrogDown();
                frogTalk[thirdFrog].text = wolfFrogText[i];
                frogTextBox[thirdFrog].SetActive(true);
                anims[thirdFrog].SetBool("Talk", true);
                continue;

            }

            if (i % 2 == 0)
            {
                yield return new WaitForSeconds(3f);
                AllFrogDown();
                frogTalk[secondFrog].text = wolfFrogText[i];
                frogTextBox[secondFrog].SetActive(true);
                anims[secondFrog].SetBool("Talk", true);
                continue;

            }



        }


        yield return new WaitForSeconds(3f);
        AllFrogDown();
        yield return null;

    }



    private void AllFrogDown()
    {
        for (int i = 0; i < 3; i++)
        {
            frogTextBox[i].SetActive(false);
            frogTalk[i].text = "";
            anims[i].SetBool("Talk", false);
        }
    }








}
