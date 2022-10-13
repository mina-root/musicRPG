using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;
using TMPro;

public class JudgeTest : MonoBehaviour
{
    // Start is called before the first frame update

    IObservable<Scores.Judgement> obsjudge;
    [SerializeField]JudgeNotifer judgeNotifer;
    TextMeshProUGUI txt;
    int combo=0;
    void Start()
    {
       judgeNotifer.OnJudgeObservable.Subscribe(displayJudge);
       txt = this.GetComponent<TextMeshProUGUI>();
       if(txt==null)Debug.Log("fail to load");
    }

    // Update is called once per frame
    void displayJudge(Scores.Judgement judgement){
        switch(judgement.judge){
            case -1:
                txt.text = "Combo:"+combo;
                combo=0;
                //Debug.Log("Miss");
                
                break;
        case 0:
                txt.text = "Combo:"+combo;
                combo+=1;
                //Debug.Log("Just");
                break;
        case 1:
                txt.text = "Combo:"+combo;
                combo+=1;
                //Debug.Log("Good");
                break;
        }
        
    }
}
