using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
namespace Scores{
    public class TurnChangeNotifer : MonoBehaviour
    {
        //ターン切り替えイベント発行する
        //
        [SerializeField] SoundPlayer soundPlayer;
        [SerializeField] Scores.ScoreUtility scoreUtility;
        Subject<int> OnTurnChengeSubject = new Subject<int>();
        public IObservable<int> OnTurnChangeObservable{
            set {this.OnTurnChengeSubject  = (Subject<int>)value;}
            get {return OnTurnChengeSubject ;}
        }
        private int pTime=0;//前フレームの再生時間
        ScoreData scoreData;
        // Start is called before the first frame update
        void Start()
        {
            scoreData=scoreUtility.scoreData;
        }

        // Update is called once per frame
        void Update()
        {
            List<ScoreObjects>scoreObjectsList =scoreData.ScoreObjectsList;
            int time = ScoreLogic.getRoopFixedTiming( scoreData,soundPlayer.getPlaytime());
            for(int i=0;i<scoreObjectsList.Count;i++){
                //ターンチェンジイベントだけ取り出す
                if (scoreObjectsList[i].GetType()== typeof(TurnChange)){
                    TurnChange ev = (TurnChange)scoreObjectsList[i];
                    if(ev.timing<=time&&pTime<ev.timing){
                        //前フレーム以降に通過済みのターンチェンジイベントがあるときイベントを発行
                        OnTurnChengeSubject.OnNext(ev.turntype);
                        Debug.Log("turnChenged! type:" +ev.turntype);
                    }
                }
            }
            pTime = time;
        }
    }
}
