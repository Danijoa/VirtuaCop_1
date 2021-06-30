using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Getscore : MonoBehaviour
{

    public Text scoreText;

    int score;

    // Start is called before the first frame update
    void Start()
    {
        score = PlayerInformation.getScore;
        scoreText.text = "SCORE " + score;
    }

}
