using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // �� ������ ����
    public GameObject enemyPrefab;

    // �� ��� ����Ʈ
    public List<GameObject> enemies = new List<GameObject>();

    // ����� �� ��ġ �޾ƿ���
    EnemyPositionCtrl enemyPosCtrl;
    public EnemyPositionCtrl enemyPositionCtrl { get => enemyPosCtrl; }
    List<PositionData> enemyPositions;

    // ī�޶� waypoint ã��
    PlayerMove playerMove;

    CameraCtrl m_CameraCtrl;
  

    //���� Ȯ�� 
    //private int[] moveStore = new int[] { 0, 3, 2, 1, 1, 4, 1, 2 };
    private int[] moveStore = new int[] { 0, 3, 0, 0, 1, 0, 3, 1, 3, 0, 0, 0, 1, 3, 0, 0 };
    private int index = 0;

    private void Awake()
    {
        enemyPosCtrl = GetComponent<EnemyPositionCtrl>();
        m_CameraCtrl = GameManager.instance.m_Camera.GetComponent<CameraCtrl>();
    }

    void Start()
    {
        enemyPosCtrl.LoadData();
        enemyPositions = enemyPosCtrl.enemyPositionDatas;

        playerMove = GameObject.Find("Player").GetComponent<PlayerMove>();
        //for (int i = 0; i < 3; i++)
        //{
        //    CreateEnemy(i);
        //}
    }

    void Update()
    {
        if (moveStore[playerMove.index] != 0)
        {
            for (int i = index; i < index + moveStore[playerMove.index]; i++)
            {
                CreateEnemy(i);
            }
            index = index + moveStore[playerMove.index];
            moveStore[playerMove.index] = 0;
        }
    }

    private void CreateEnemy(int index)
    {
        // �� ������ ����
        GameObject enemy = Instantiate(enemyPrefab,
            new Vector3(enemyPositions[index].xStart, 0f, enemyPositions[index].zStart),
            Quaternion.identity);

        // ��ġ ����ֱ�
        enemy.GetComponent<EnemyMovement>().SetUp(enemyPositions[index].xStart, enemyPositions[index].zStart, enemyPositions[index].xEnd, enemyPositions[index].zEnd, enemyPositions[index].yPos);

        //m_CameraCtrl.m_Targets.Add(enemy);
        m_CameraCtrl.m_EnemyMovemrnts.Add(enemy.GetComponent<EnemyMovement>());
    }
}
