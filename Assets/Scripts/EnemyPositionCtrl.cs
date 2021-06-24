using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]

public struct PositionData
{
    // ���� ��ġ
    public float xStart;
    public float zStart;

    // ���� ��ġ
    public float xEnd;
    public float zEnd;
}

public class EnemyPositionCtrl : MonoBehaviour
{
    [SerializeField]
    TextAsset posData;
    public List<PositionData> enemyPositionDatas = new List<PositionData>();

    // �ؽ�Ʈ���� �� ��ġ �о����
    public void LoadData()
    {
        string text = posData.text;

        string[] lines = text.Split('\n');  // \n ������ �������� �߶� �迭�� ����

        foreach (var line in lines)
        {
            if (line == "") // ������ �� ����
                break;

            string[] words = line.Split('\t');

            PositionData data = new PositionData();
            int index = 0;
            foreach (var word in words)
            {
                if (word[0] == 'X')
                    break;

                switch (index)
                {
                    case 0: data.xStart = float.Parse(word); break;
                    case 1: data.zStart = float.Parse(word); break;
                    case 2: data.xEnd = float.Parse(word); break;
                    case 3: data.zEnd = float.Parse(word); break;
                }
                index++;

                if (index >= 4)
                {
                    enemyPositionDatas.Add(data);
                    break;
                }
            }
        }
    }
}