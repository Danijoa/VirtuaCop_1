using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpSpawner : MonoBehaviour
{
    // 적 프리팹 연결
    public GameObject helpPrefab;

    // 적 담는 리스트
    public List<GameObject> helps = new List<GameObject>();

    // 저장된 적 위치 받아오기
    HelpPositionCtrl helpPosCtrl;
    public HelpPositionCtrl helpPositionCtrl { get => helpPosCtrl; }
    List<PositionDatas> helpPositions;

    // 카메라 waypoint 찾기
    PlayerMove playerMove;

    CameraCtrl m_CameraCtrl;


    //순서 확인 
    //private int[] moveStore = new int[] { 0, 3, 2, 1, 1, 4, 1, 2 };
    private int[] moveStore = new int[] { 0, 1, 0, 0, 0, 0, 2, 0, 1, 0, 0, 0, 0, 1, 0, 0};
    private int index = 0;
private void Awake()
    {
        helpPosCtrl = GetComponent<HelpPositionCtrl>();
        m_CameraCtrl = GameManager.instance.m_Camera.GetComponent<CameraCtrl>();
    }

    void Start()
    {
        helpPosCtrl.LoadData();
        helpPositions = helpPosCtrl.helpPositionDatas;

        playerMove = GameObject.Find("Player").GetComponent<PlayerMove>();
        //for (int i = 0; i < 3; i++)
        //{
        //    CreateEnemy(i);
        //}
    }

    void Update()
    {
        Invoke("Wait", 4f);
    }

    private void CreateHelp(int index)
    {
        // 적 프리팹 생성
        GameObject help = Instantiate(helpPrefab,
            new Vector3(helpPositions[index].xStart, 0f, helpPositions[index].zStart),
            Quaternion.identity);

        // 위치 잡아주기
        help.GetComponent<HelpMovement>().SetUp(helpPositions[index].xStart, helpPositions[index].zStart, helpPositions[index].xEnd, helpPositions[index].zEnd, helpPositions[index].yPos);

        m_CameraCtrl.m_Targets.Add(help);
        // m_CameraCtrl.m_EnemyMovemrnts.Add(help.GetComponent<HelpMovement>());
    }

    private void Wait()
	{
        if (moveStore[playerMove.index] != 0)
        {
            for (int i = index; i < index + moveStore[playerMove.index]; i++)
            {
                CreateHelp(i);
            }
            index = index + moveStore[playerMove.index];
            moveStore[playerMove.index] = 0;
        }
    }
}

