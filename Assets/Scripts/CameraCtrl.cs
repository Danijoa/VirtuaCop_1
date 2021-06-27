using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    [SerializeField]
    Camera m_Cam;
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject canterObj;
    
    List<GameObject> m_Targets = new List<GameObject>();

    List<EnemyMovement> m_EnemyMovemrnts = new List<EnemyMovement>();
    //[SerializeField] 
    //GameObject pivot;


    private float m_CamSpeed = 10f;
    //private float m_CamZoomSpeed = 20f;
    private float m_CamMax = 20f;
    private float m_CamMin;

    private float dist;
    private float targetFocalLength;
    private bool isZoom = false;
    //private float m_CamPreviousLocation = 0f;
    //private float time;

    public int index { get; private set; }

	private void Awake()
	{
        //���ӿ�����Ʈ�� �����ö�

        //���� ������Ʈ �̸����� �����ö�
        //player = GameObject.Find("Player");

        //���� ������Ʈ Ÿ������ �����ö�
        //player = GameObject.FindGameObjectsWithTag("Player");

        //�ڽ� ������Ʈ���� �θ� ������Ʈ�� �����ö�
        player = transform.parent.gameObject;
        index = 0;
    }
	// Start is called before the first frame update
	void Start()
    {
        m_Cam.focalLength = 20f;
        dist = Vector3.Distance(m_Cam.transform.position, m_Targets[index].transform.position);
        targetFocalLength = dist * 4f;
    }

    // Update is called once per frame
    void Update()
    {
        m_Cam.transform.position = (canterObj.transform.position + new Vector3(0.5f, 1.5f, 0.5f));
       
        if (m_Targets != null)
        {
            TargetRotate();

            if (Input.GetMouseButtonDown(0))
            {
                if (m_Targets.Count - 1 > index)
                {
                    //m_Targets[index].SetActive(false);
                    index++;
                  

                   
                    isZoom = true;
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                if (0 < index)
                {
                    //m_Targets[index].SetActive(false);
                    index--;
                    dist = Vector3.Distance(m_Cam.transform.position, m_Targets[index].transform.position);

                    targetFocalLength = dist * 4f;
                    isZoom = true;
                }
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
        m_Cam.focalLength = m_Cam.focalLength - (Time.deltaTime * m_CamSpeed);

        if (m_Cam.fieldOfView >= m_CamMax)
        {
            m_Cam.fieldOfView = m_CamMax;
        }
    }

    private void TargetRotate()
	{ 
        if(m_EnemyMovemrnts[index].state == EnemyMovement.State.Battle)
		{
            // (dic > 1)
            {
                ZoomIn();
            }
            //              ����ġ - ī�޶���ġ�� ���� ���� �� ��ġ���� ����ġ�� ������ ��Ÿ����.
            Vector3 dir = m_Targets[index].transform.position - m_Cam.transform.position;

            //Quaternion.Lerp�Լ��� ����ϸ� �ε巴�� ȸ���� �Ѵ� �׸��� Quaternion.LookRotation(dir)�� dir���࿡ ���� ���ʹϾ� ��ȸ���� �ϰ� ���ش�
            m_Cam.transform.rotation = Quaternion.Lerp(m_Cam.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 5f);
        }
    }

    private void PivotRotate()
    {
        //Quaternion.Lerp�Լ��� ����ϸ� �ε巴�� ȸ���� �Ѵ� �׸��� Quaternion.LookRotation(dir)�� dir���࿡ ���� ���ʹϾ� ��ȸ���� �ϰ� ���ش�
        m_Cam.transform.rotation = Quaternion.Lerp(m_Cam.transform.rotation, player.transform.rotation, Time.deltaTime * 5f);
        ZoomOut();
    }

    public void AddTarget(GameObject target)
	{
        if(target != null)
		{
            m_Targets.Add(target);
            m_EnemyMovemrnts.Add(m_Targets[index].GetComponent<EnemyMovement>());
        }
        else
		{
            Debug.Log("Ÿ���� �����...");
		}
	}

    public void EnemyHit(bool isHit)
	{
        if(isHit)
		{
            m_Targets[index].SetActive(false);
            m_EnemyMovemrnts[index].Die();
            //index++;
            m_Targets.RemoveAt(index);
            dist = Vector3.Distance(m_Cam.transform.position, m_Targets[index].transform.position);
            targetFocalLength = dist * 4f;
		}
	}

}
