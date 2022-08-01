using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using Scores;

public class JudgeNotifer : MonoBehaviour
{
    // Start is called before the first frame update
    public Subject<Judgement> judgeSubject = new Subject<Judgement>();//通知判定用subject 
    public IObservable<Judgement> OnJudgeObservable
    {
        set {judgeSubject=(Subject<Judgement>)value;}
        get {return judgeSubject;}
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    public void JudgeNotice(int judgement,int laneId,int type){
        judgeSubject.OnNext(new Judgement{
            judge=judgement,
            lane=laneId,
            notesType=type
        });
    }
}
