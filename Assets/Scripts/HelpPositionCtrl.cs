using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]

public struct PositionDatas
{
    // ���� ��ġ
    public float xStart;
    public float zStart;

    // ���� ��ġ
    public float xEnd;
    public float zEnd;

    // y ��ġ
    public float yPos;
}

public class HelpPositionCtrl : MonoBehaviour
{
    [SerializeField]
    TextAsset posData;

    public List<PositionDatas> helpPositionDatas = new List<PositionDatas>();

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

            PositionDatas data = new PositionDatas();
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
                    case 4: data.yPos = float.Parse(word); break;
                }
                index++;

                if (index >= 5)
                {
                    helpPositionDatas.Add(data);
                    break;
                }
            }
        }
    }
}
