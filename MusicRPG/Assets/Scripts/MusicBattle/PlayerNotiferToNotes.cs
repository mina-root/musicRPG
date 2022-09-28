using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

namespace Player{
    public class PlayerNotiferToNotes : MonoBehaviour
    {
        public float ScorePos{
            get;set;
        }

        [SerializeField]PlayerInput playerInput;
        [SerializeField]GameObject scoreObject;
        [SerializeField]SoundPlayer soundPlayer;
        [SerializeField]BattleSettings settings;

        [SerializeField]Scores.TurnChangeNotifer turnChangeNotifer;
        Scores.ScoreData scoreData;
        public List<Scores.Notes.NotesBase> notesInstancesList = null;

        void Start()
        {
            ScorePos = 0.0f;//譜面位置を初期化
            scoreData = scoreObject.GetComponent<Scores.ScoreUtility>().scoreData;//譜面データを取得
            playerInput.OnButtonObservable.Subscribe(noticeToNotes);//入力を購読
            turnChangeNotifer.OnTurnChangeObservable.Subscribe((int x)=>{if(x==0)notesInstancesList.Clear();} );

        }

        // Update is called once per frame
        void Update()
        {
            //譜面位置を取得
            ScorePos = Scores.ScoreLogic.getScorePosition(scoreData,Scores.ScoreLogic.getRoopFixedTiming(scoreData,soundPlayer.getPlaytime()));
            
        }

        void noticeToNotes(int[] input){//ノーツに判定を依頼

            int tmg =System.Int32.MaxValue;
            Scores.Notes.NotesBase target=null;
             
            for(int i=0;i<input.Length;i++){
                if(input[i]>0){
                    //Debug.Log(Scores.ScoreLogic.getRoopFixedTiming(scoreData,soundPlayer.getPlaytime()));
                    foreach (Scores.Notes.NotesBase notes in notesInstancesList){
                        if(notes.GetType()==typeof(Scores.Notes.TapNotes)){
                            if(notes.timing>Scores.ScoreLogic.getRoopFixedTiming(scoreData,soundPlayer.getPlaytime())-settings.goodRenge&&tmg>notes.timing&&notes.laneId==i){
                                target=notes;
                                tmg=notes.timing;
                            }
                        }
                        if(notes.GetType()==typeof(Scores.Notes.HoldNotes)){
                            Scores.Notes.HoldNotes holdNote=(Scores.Notes.HoldNotes)notes;
                            if(holdNote.endtiming>Scores.ScoreLogic.getRoopFixedTiming(scoreData,soundPlayer.getPlaytime())-settings.goodRenge&&tmg>holdNote.endtiming&&notes.laneId==i){
                                target=notes;
                                tmg=holdNote.endtiming;
                            }
                        }

                    }
                    if(target!=null)target.tryJudge(input[target.laneId]==1);
                }
            }
        }
    }
}
