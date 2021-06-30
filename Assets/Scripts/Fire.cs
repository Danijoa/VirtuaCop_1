using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fire : MonoBehaviour
{

    public Animator animt;
    public RectTransform effect;
    public ParticleSystem flashEffect; // 총구 화염 효과
    public GameObject bullet;
    public GameObject effectobj;
    public GameObject reloadText;
    private CameraCtrl m_Cam;

    Image image;

    public bool fire = false;
    public bool reload = false;
    public bool isEnemy = false;
    public int bullet_cnt = 6;

    public GameManager gameManager;

    // 체력
    public BossHp bossHp;
    public GameObject boss;

    private void Awake()
    {
        m_Cam = GameObject.Find("Camera").GetComponent<CameraCtrl>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        bossHp = GameObject.Find("BossHp").GetComponent<BossHp>();
        boss.SetActive(false);

        bullet = GameObject.Find("Bullet");
        animt = bullet.GetComponent<Animator>();
        image = bullet.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 mousePos = Input.mousePosition;

        animt.SetBool("Reload", reload);

        effect.anchoredPosition = new Vector2(mousePos.x, mousePos.y);

        if (bullet_cnt == 0)
        {
            reloadText.SetActive(true);
        }

        if (Input.GetMouseButtonDown(0) && bullet_cnt > 0)
        {
            RaycastHit m_Hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out m_Hit, Mathf.Infinity))
            {
                if (m_Hit.collider.gameObject.tag == "Enemy")
                {
                    Debug.Log("애너미 마졌어");

                    gameManager.totalScore += 150;

                    //m_Hit.collider.gameObject.SetActive(false);
                    m_Cam.m_Targets.Remove(m_Hit.collider.gameObject);
                    //m_Cam.m_EnemyMovemrnts.Remove(m_Hit.collider.gameObject.GetComponent<EnemyMovement>());

                    EnemyMovement[] enemyMovement = m_Hit.collider.gameObject.GetComponents<EnemyMovement>();
                    for (int i = 0; i < enemyMovement.Length; i++)
                    {
                        enemyMovement[i].Die();
                    }

                    //m_Cam.index++;
                    //m_Cam.m_Targets.Remove(m_Hit.collider.gameObject);
                    //m_Cam.EnemyHit(isEnemy);
                }

                if (m_Hit.collider.gameObject.tag == "Help")
                {
                    Debug.Log("시민 마졌어");

                    gameManager.totalScore -= 300;
                    m_Cam.m_Targets.Remove(m_Hit.collider.gameObject);

                    HelpMovement[] helpMovement = m_Hit.collider.gameObject.GetComponents<HelpMovement>();
                    for (int i = 0; i < helpMovement.Length; i++)
                    {
                        helpMovement[i].Die();
                    }
                }
                else
                {
                    isEnemy = false;
                }

                if (m_Hit.collider.gameObject.tag == "Boss")
                {
                    Debug.Log("보스 마졌어");
                    //bossHp = GameObject.Find("BossHp").GetComponent<BossHp>();
                    bossHp.HpDelete();
                }
                else
                {
                    isEnemy = false;
                }                
            }

            if (bullet_cnt > 0)
            {
                animt.SetInteger("Cnt", bullet_cnt);
                bullet_cnt--;
                fire = true;
                animt.SetBool("Fire", fire);
                effectobj.SetActive(true);
                flashEffect.Play();
                image.sprite = bullet.GetComponent<SpriteRenderer>().sprite;
            }
        }


        //if (Input.GetKeyDown("f") && bullet_cnt > 0)
        //{
        //    bullet_cnt--;
        //    fire = true;
        //    animt.SetBool("Fire", fire);
        //    effectobj.SetActive(true);
        //    flashEffect.Play();
        //    image.sprite = bullet.GetComponent<SpriteRenderer>().sprite;
        //}

        if (Input.GetKeyDown("r"))
        {
            bullet_cnt = 6;
            reloadText.SetActive(false);
            reload = true;
            animt.SetBool("Reload", reload);
            image.sprite = bullet.GetComponent<SpriteRenderer>().sprite;
        }

        image.sprite = bullet.GetComponent<SpriteRenderer>().sprite;

        if (Input.GetMouseButtonUp(0))
        {
            fire = false;
            animt.SetBool("Fire", fire);
        }

        //if (Input.GetKeyUp("f"))
        //{
        //    fire = false;
        //    animt.SetBool("Fire", fire);
        //}

        reload = false;

    }
}