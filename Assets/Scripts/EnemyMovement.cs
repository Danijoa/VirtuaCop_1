using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public enum State
	{
        Move,
        Battle,
        Die
    }

    // Component
    Rigidbody myRigid;

    // 카메라, 플레이어, 총 위치 받아오기
    Transform playerPos;
    Transform cameraPos;
    public Transform gunPos;



    // 카메라 제어
    private CameraCtrl cameraCtrl;
    
	// 사용할 총
    public GameObject gun;

    public ItemDrop m_ItemDrop;

    //적 상태
    public EnemyState enemyState;
    public State state { get; set; }

    // 적 
    float bodyguardSpeed;
    Vector3 targetPos;

    bool isItemDrop = false;

    private void Awake()
    {
        cameraPos = GameObject.Find("Camera").GetComponent<Transform>();
        
        enemyState = GetComponent<EnemyState>();

        m_ItemDrop = GetComponent<ItemDrop>();

        state = State.Move;
    }

    public void SetUp(float xS, float zS, float xE, float zE, float yP)
    {
        playerPos = GameObject.Find("Player").GetComponent<Transform>();
        cameraCtrl = GameManager.instance.m_Camera.GetComponent<CameraCtrl>();

        if (yP != 0)
        {
            // 적 생성 위치
            transform.position = new Vector3(xS, playerPos.position.y + yP, zS);

            // 적 도착 위치
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
        // 적이 죽으면
        if (enemyState.isDie == true)
        {
            Die();

            if(isItemDrop == false)
            {
                m_ItemDrop.Drop(this.transform);
                isItemDrop = true;  
            }
        }

    }
    private IEnumerator MakeMove()
    {
        while(true)
        {
            // 적이 타겟지점에 도착하면 break
            float checkDist = Vector3.Distance(targetPos, transform.position);
            if (checkDist <= 1.5f)
            {

                // 배틀 상태로 변경 후
                state = State.Battle;
                enemyState.shoot = true;
                // 카메라한테 자신을 target에 넣어주기
                if(state == State.Battle)
				{
                    cameraCtrl.AddTarget(gameObject);
                    //cameraCtrl.m_EnemyMovemrnts.Add(gameObject.GetComponent<EnemyMovement>());
                }

                break;
            }

            // 생성 위치에서 카메라 안으로 들어와서 정지
            MoveToPosition();

            // 잠깐 멈추자
            yield return new WaitForSeconds(0.05f);
        }

        // 카메라를 바라보는 방향으로 
        transform.LookAt(cameraPos.position);
        // 한번 멈추기..
        myRigid.Sleep();

        // (조준) 사격
        while (enemyState.shoot == true)
        {
            // 한번 발사하고
            gun.GetComponent<EnemyGun>().Fire();

       

            // 잠깐 멈추자
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void MoveToPosition()
    {
    	//상태 변경
        state = State.Move;
        
        // 이동 방향 벡터 잡기
        var moveDir = (targetPos - transform.position).normalized;
        moveDir.y = 0f;

        // 이동
        transform.position += moveDir * bodyguardSpeed * Time.deltaTime;


    }

    public void Die()
	{
       

        enemyState.shoot = false;
        state = State.Die;
        enemyState.isDie = true;
       
        //cameraCtrl.m_Targets.Remove(gameObject);
        //gameObject.SetActive(false);
        StartCoroutine(MakeSetFalse());
    }

    private IEnumerator MakeSetFalse()
    {
        yield return new WaitForSeconds(10f);
        gameObject.SetActive(false);        
    }
}
