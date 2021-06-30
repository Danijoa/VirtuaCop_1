using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    [SerializeField]
    Camera m_Cam;
    [SerializeField]
    GameObject canterObj;
    [SerializeField]
    PlayerInformation m_PlayerInfo;
    
    public List<GameObject> m_Targets = new List<GameObject>();

    public List<EnemyMovement> m_EnemyMovemrnts = new List<EnemyMovement>();
    public List<HelpMovement> m_HelpMovements = new List<HelpMovement>();
    //[SerializeField] 
    //GameObject pivot;


    private float m_CamSpeed = 60f;
    private float m_CamMax = 20f;
    private float m_CamMin;

    private float dist;
    private float targetFocalLength;
    private bool isZoom = false;
    private bool isDestroy = false;
    private float timer = 0f;
    //private float m_CamPreviousLocation = 0f;
    //private float time;

    public int index { get; set; }

	private void Awake()
	{
        //게임오브젝트를 가져올때

        //게임 오브젝트 이름으로 가져올때
        //player = GameObject.Find("Player");

        //게임 오브젝트 타입으로 가져올때
        //player = GameObject.FindGameObjectsWithTag("Player");

        //자식 오브젝트에서 부모 오브젝트를 가져올때
        m_PlayerInfo = transform.parent.gameObject.GetComponent<PlayerInformation>();
        index = 0;
    }
	// Start is called before the first frame update
	void Start()
    {
        m_Cam.focalLength = m_CamMax;
        
    }

    // Update is called once per frame
    void Update()
    {
        m_Cam.transform.position = (canterObj.transform.position + new Vector3(0.5f, 1.5f, 0.5f));

        if(m_Targets.Count != 0)
        {
            if(m_Targets[index].tag == "Help")
			{
                dist = Vector3.Distance(m_Cam.transform.position, m_Targets[index].transform.position);
                targetFocalLength = dist * 4f;

                isZoom = true;
                TargetRotate();

                timer += Time.deltaTime;

                if (timer>=1.0f)
				{
                    DestroyTarget();
                }

            }

            else
			{

                if(timer > 0)
				{
                    timer = 0;
				}

                dist = Vector3.Distance(m_Cam.transform.position, m_Targets[index].transform.position);
                targetFocalLength = dist * 4f;

                isZoom = true;
                TargetRotate();
            }

        }
        else
        {
            PivotRotate();
        }


		//TrueForAll()함수는 모든 요소가 조건에 맞으면 true로 불값을 리턴해주는 함수
		if (m_EnemyMovemrnts.TrueForAll(x => x.state == EnemyMovement.State.Die))
        {
            if (m_Targets.Count == 0 && m_PlayerInfo.state != PlayerInformation.State.Die)
			{
                m_PlayerInfo.isMove = true;
            }
        }
	}

	private void ZoomIn()
	{
        if (isZoom == false) return;

        if (targetFocalLength - (Mathf.Epsilon) < m_Cam.focalLength &&
            m_Cam.focalLength < targetFocalLength + (Mathf.Epsilon))
		{
            m_Cam.focalLength = targetFocalLength;
            isZoom = false;
            return;
        }        
        
        m_Cam.focalLength = Mathf.Lerp(m_Cam.focalLength, targetFocalLength, (Time.deltaTime * m_CamSpeed));
	}

    private void ZoomOut()
	{
        //cam.transform.rotation = Quaternion.Lerp(target.transform.rotation, pivot.transform.rotation, m_CamSpeed);
        if(m_Cam.focalLength > m_CamMax)
		{
            m_Cam.focalLength = m_Cam.focalLength - (Time.deltaTime * m_CamSpeed);

            if (m_Cam.focalLength <= m_CamMax)
            {
                m_Cam.focalLength = m_CamMax;
            }
        }
        
    }

    private void TargetRotate()
	{ 
            //              적위치 - 카메라위치를 빼면 현재 내 위치에서 적위치의 방향을 나타낸다.
            Vector3 dir = m_Targets[index].transform.position - m_Cam.transform.position;

            //Quaternion.Lerp함수를 사용하면 부드럽게 회전을 한다 그리고 Quaternion.LookRotation(dir)은 dir방행에 따른 쿼터니언 축회전을 하게 해준다
            m_Cam.transform.rotation = Quaternion.Lerp(m_Cam.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 10f);
        
            ZoomIn();
    }

    private void PivotRotate()
    {
        //Quaternion.Lerp함수를 사용하면 부드럽게 회전을 한다 그리고 Quaternion.LookRotation(dir)은 dir방행에 따른 쿼터니언 축회전을 하게 해준다
        m_Cam.transform.rotation = Quaternion.Lerp(m_Cam.transform.rotation, m_PlayerInfo.transform.rotation, Time.deltaTime * 10f);
        ZoomOut();
    }

    public void AddTarget(GameObject target)
	{
        if(target != null)
		{
            m_Targets.Add(target);
        }
        else
		{
            Debug.Log("타겟이 없어요...");
		}
	}
    private void DestroyTarget()
	{
       if( m_Targets[index].tag == "Help")
		{
            m_Targets[index].tag = "Dead";
            //Destroy(m_Targets[index]);
            m_Targets.Remove(m_Targets[index]);
            timer = 0;
            //Destroy(m_Targets[index]);
        }

    }
  

}
