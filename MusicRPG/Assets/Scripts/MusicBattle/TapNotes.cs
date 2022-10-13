using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace Scores.Notes{
    
    public class TapNotes : NotesBase
    {
        bool judged=false;
        public override void tryJudge(bool Pressed){

            if(!Pressed)return;
            
            int deltatime= Scores.ScoreLogic.getRoopFixedTiming(score,soundPlayer.getPlaytime())-timing;
            //Debug.Log(deltatime);
            if(System.Math.Abs(deltatime)<battleSettings.justRenge){
                JudgeNotifer.JudgeNotice(0,laneId,notesType);
                Destroy( this.gameObject );
            }else if(System.Math.Abs(deltatime)<battleSettings.goodRenge){
                JudgeNotifer.JudgeNotice(1,laneId,notesType);
                Destroy( this.gameObject );
            }
        }

        
    override protected void Move(){
        base.Move();
        //判定範囲を過ぎていたらmiss判定を通知
        if(judged==false&&Scores.ScoreLogic.getRoopFixedTiming(score,soundPlayer.getPlaytime())-timing>battleSettings.goodRenge){
            JudgeNotifer.JudgeNotice(-1,laneId,notesType);
            judged=true;
        }
        //相対譜面位置が大きくマイナスかつタイミングを過ぎているなら破棄
        float RelativeScorePos = (float)(scorePos-notiferToNotes.ScorePos)*battleSettings.speedRate;
        RelativeScorePos *= 0.0001f;
        if(RelativeScorePos<-1&&Scores.ScoreLogic.getRoopFixedTiming(score,soundPlayer.getPlaytime())-timing>battleSettings.goodRenge){
            Destroy( this.gameObject );
        }
    }
    }

}
