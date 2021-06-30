using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpMovement : MonoBehaviour
{
    public enum State
    {
        Move,
        Battle,
        Die
    }

    // Component
    Rigidbody myRigid;

    // ī�޶�, �÷��̾�, �� ��ġ �޾ƿ���
    Transform playerPos;
    Transform cameraPos;

    //�÷��̾� ����
    private PlayerHp m_PlayerHp;

    // ī�޶� ����
    private CameraCtrl cameraCtrl;

    //�ù� ����
    //public EnemyState enemyState;
    public State state { get; set; }

    // �� 
    float bodyguardSpeed;
    Vector3 targetPos;

    public bool isDie = false;
    private Animator helpAnimator;


    private void Awake()
    {
        cameraPos = GameObject.Find("Camera").GetComponent<Transform>();
        m_PlayerHp = GameObject.Find("Player").GetComponent<PlayerHp>();
        helpAnimator = gameObject.GetComponent<Animator>();
        //enemyState = GetComponent<EnemyState>();
        state = State.Move;
    }

    public void SetUp(float xS, float zS, float xE, float zE, float yP)
    {
        playerPos = GameObject.Find("Player").GetComponent<Transform>();
        cameraCtrl = GameManager.instance.m_Camera.GetComponent<CameraCtrl>();

        if (yP != 0)
        {
            // �� ���� ��ġ
            transform.position = new Vector3(xS, playerPos.position.y + yP, zS);

            // �� ���� ��ġ
            targetPos = new Vector3(xE, playerPos.position.y + yP, zE);
        }
        else
        {
            transform.position = new Vector3(xS, playerPos.position.y, zS);

            targetPos = new Vector3(xE, playerPos.position.y, zE);
        }
    }

    private void Start()
    {
        myRigid = GetComponent<Rigidbody>();
        bodyguardSpeed = Random.Range(5f, 8f);
        StartCoroutine(MakeMove());

        transform.LookAt(cameraPos.position);
    }

    private void Update()
    {

    }
    private IEnumerator MakeMove()
    {
        while (true)
        {
            // ���� Ÿ�������� �����ϸ� break
            float checkDist = Vector3.Distance(targetPos, transform.position);
            if (checkDist <= 1.5f)
            {
                break;
            }

            // ���� ��ġ���� ī�޶� ������ ���ͼ� ����
            MoveToPosition();

            // ��� ������
            yield return new WaitForSeconds(0.05f);
        }

        // ī�޶� �ٶ󺸴� �������� 
        transform.LookAt(cameraPos.position);
        // �ѹ� ���߱�..
        myRigid.Sleep();
    }

    private void MoveToPosition()
    {
        //���� ����
        state = State.Move;

        // �̵� ���� ���� ���
        var moveDir = (targetPos - transform.position).normalized;
        moveDir.y = 0f;

        // �̵�
        transform.position += moveDir * bodyguardSpeed * Time.deltaTime;
    }

    private IEnumerator MakeSetFalse()
    {
        yield return new WaitForSeconds(10f);
        gameObject.SetActive(false);
    }
    public void Die()
    {
        state = State.Die;
        isDie = true;
        helpAnimator.SetBool("IsDie",isDie);
        //cameraCtrl.m_Targets.Remove(gameObject);
        //gameObject.SetActive(false);
        StartCoroutine(MakeSetFalse());
    }


}

