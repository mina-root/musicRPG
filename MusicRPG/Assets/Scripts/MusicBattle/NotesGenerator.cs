using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

namespace Scores{

    public class NotesGenerator : MonoBehaviour
    {
        [SerializeField]GameObject scoreObject;
        [SerializeField]BattleSettings battleSettings;
        [SerializeField]Player.PlayerNotiferToNotes notiferToNotes;
        [SerializeField]SoundPlayer soundPlayer;
        [SerializeField]JudgeNotifer judgeNotifer;
        [SerializeField]Notes.TapNotes TapNotesPrefab ;//tapnotes
        [SerializeField]Notes.HoldNotes HoldNotesPrefab ;//Holdnotes
        TurnChangeNotifer turnChangeNotifer;
        Scores.ScoreData scoreData;
        List<Notes.NotesBase> notesInstanceList = new List<Notes.NotesBase>();
        void Start()
        {
            scoreData = this.gameObject.GetComponent<Scores.ScoreUtility>().scoreData;//譜面データを取得
                    //ターンチェンジイベントを購読
            turnChangeNotifer = scoreObject.GetComponent<Scores.TurnChangeNotifer>();
            turnChangeNotifer.OnTurnChangeObservable.Subscribe(turnchange);
            notiferToNotes.notesInstancesList = notesInstanceList;
        }

        // Update is called once per frame
        void Update()
        {
            
        }
        void turnchange(int turntype){//ターンチェンジイベントを受け取ったときに実行される

        if(turntype == 1){//音ゲーターン開始時
            int starttime =ScoreLogic.getRoopFixedTiming(scoreData, soundPlayer.getPlaytime());
            int endtime=0;
            List<Scores.ScoreObjects> scoreObjectsList = scoreData.ScoreObjectsList;
            //ターンチェンジイベントだけ取り出す
            bool f = false;
            for(int i=0;i<scoreObjectsList.Count;i++){
                if (scoreObjectsList[i].GetType()== typeof(Scores.TurnChange)){
                    Scores.TurnChange ev = (Scores.TurnChange)scoreObjectsList[i];
                    if(ev.timing>starttime){
                        f = true;
                    }
                    if(f){
                        endtime=ev.timing;
                        break;
                    }

                }
            }
            //Debug.Log(endtime);
            //まとめてノーツを生成
            for(int i=0;i<scoreObjectsList.Count;i++){
                //Tapを生成する
                
                if (scoreObjectsList[i].GetType()== typeof(Scores.Tap)){
                    
                    Scores.Tap ev = (Scores.Tap)scoreObjectsList[i];
                    if(ev.timing>=starttime&&ev.timing<endtime){
                        Vector3 pos = new Vector3 (0.15f*ev.lane-0.15f,0,-1);
                        //Tapノーツを1つ生成し、タイミングと譜面位置を設定する
                        Scores.Notes.TapNotes note = GameObject.Instantiate( TapNotesPrefab,pos,TapNotesPrefab.transform.rotation);
                        note.timing = ev.timing;
                        note.scorePos = Scores.ScoreLogic.getScorePosition(scoreData,ev.timing);
                        note.notiferToNotes = notiferToNotes;
                        note.soundPlayer = soundPlayer;
                        note.battleSettings = battleSettings;
                        note.laneId = ev.lane;
                        note.notesType=ev.actionType;
                        note.JudgeNotifer=judgeNotifer;
                        note.score=scoreData;
                        notesInstanceList.Add(note);

                    }
                }
                //Holdを生成する
                float bpm=120;
                if(scoreObjectsList[i].GetType()== typeof(Scores.SetSpeed))
                {
                    Scores.SetSpeed ev = (Scores.SetSpeed)scoreObjectsList[i];
                    bpm=ev.BPM;
                }
                if (scoreObjectsList[i].GetType()== typeof(Scores.Hold)){
                    Scores.Hold ev = (Scores.Hold)scoreObjectsList[i];
                    if(ev.timing>=starttime&&ev.timing<endtime){
                        Vector3 pos = new Vector3 (0.15f*ev.lane-0.15f,0,-1);
                        //Holdノーツを1つ生成し、タイミングと譜面位置を設定する
                        Scores.Notes.HoldNotes note = GameObject.Instantiate( HoldNotesPrefab,pos,TapNotesPrefab.transform.rotation);
                        note.timing = ev.timing;
                        note.endtiming=ev.endtiming;
                        note.scorePos = Scores.ScoreLogic.getScorePosition(scoreData,ev.timing);
                        note.endPos = Scores.ScoreLogic.getScorePosition(scoreData,ev.endtiming);
                        note.notiferToNotes = notiferToNotes;
                        note.soundPlayer = soundPlayer;
                        note.battleSettings = battleSettings;
                        note.laneId = ev.lane;
                        note.BPM = bpm;
                        note.JudgeNotifer=judgeNotifer;
                        note.notesType=ev.actionType;
                        note.score=scoreData;
                        notesInstanceList.Add(note);
                    }
                }
            }
        }

    }
    }
}