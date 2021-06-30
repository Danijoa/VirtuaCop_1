using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]

public struct PositionDatas
{
    // 생성 위치
    public float xStart;
    public float zStart;

    // 도착 위치
    public float xEnd;
    public float zEnd;

    // y 위치
    public float yPos;
}

public class HelpPositionCtrl : MonoBehaviour
{
    [SerializeField]
    TextAsset posData;

    public List<PositionDatas> helpPositionDatas = new List<PositionDatas>();

    // 텍스트에서 적 위치 읽어오기
    public void LoadData()
    {
        string text = posData.text;

        string[] lines = text.Split('\n');  // \n 만나는 기준으로 잘라서 배열에 저장

        foreach (var line in lines)
        {
            if (line == "") // 마지막 줄 도착
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
