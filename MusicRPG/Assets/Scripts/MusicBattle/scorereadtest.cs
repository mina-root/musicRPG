using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scores;
public class scorereadtest : MonoBehaviour
{
    // Start is called before the first frame update



    ScoreData scoreData = new ScoreData();
    void Start()
    //ダミーデータ
    {
        scoreData.AudioOffset=0;
        List<ScoreObjects> scoreObjects= new List<ScoreObjects>();
        scoreObjects.Add(new SetSpeed{
                timing=0,
                BPM=100,
                LPB=4.0f
        });
        scoreObjects.Add(new SetSpeed{
                timing=5000,
                BPM=50,
                LPB=4.0f
        });
        scoreData.ScoreObjectsList=scoreObjects;
        float pos = ScoreLogic.getScorePosition(scoreData,10000);
        Debug.Log(pos);
        string jsonString = JsonUtility.ToJson(scoreData, true);
        
    }


    

    // Update is called once per frame
    void Update()
    {
        
    }
}
