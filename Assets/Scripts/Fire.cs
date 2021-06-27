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
    public GameObject text;
    private CameraCtrl m_Cam;

    Image image;

    public bool fire = false;
    public bool reload = false;
    public bool isEnemy = false;
    public int bullet_cnt = 6;

	private void Awake()
	{

        m_Cam = GameObject.Find("Camera").GetComponent<CameraCtrl>();

    }
	// Start is called before the first frame update
	void Start()
    {
        bullet = GameObject.Find("Bullet");
        animt = bullet.GetComponent<Animator>();
        image = bullet.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            RaycastHit m_Hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out m_Hit, Mathf.Infinity))
            {
                if (m_Hit.collider.gameObject.tag == "Enemy")
                {
                    Debug.Log("애너미 마졌어");
                    isEnemy = true;
                    m_Cam.EnemyHit(isEnemy);
                }
            }
        }

        Vector2 mousePos = Input.mousePosition;

        animt.SetBool("Reload", reload);

        effect.anchoredPosition = new Vector2(mousePos.x, mousePos.y);

        if (bullet_cnt == 0)
        {
            text.SetActive(true);
        }

        if (Input.GetKeyDown("f") && bullet_cnt > 0)
        {
            bullet_cnt--;
            fire = true;
            animt.SetBool("Fire", fire);
            effectobj.SetActive(true);
            flashEffect.Play();
            image.sprite = bullet.GetComponent<SpriteRenderer>().sprite;
        }

        if (Input.GetKeyDown("r"))
        {
            bullet_cnt = 6;
            text.SetActive(false);
            reload = true;
            animt.SetBool("Reload", reload);
            image.sprite = bullet.GetComponent<SpriteRenderer>().sprite;
        }

        image.sprite = bullet.GetComponent<SpriteRenderer>().sprite;

        if (Input.GetKeyUp("f"))
        {
            fire = false;
            animt.SetBool("Fire", fire);
        }

        reload = false;

    }
}
