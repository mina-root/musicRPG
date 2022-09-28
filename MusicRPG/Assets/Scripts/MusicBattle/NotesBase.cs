using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
namespace Scores{
namespace Notes{
public abstract class NotesBase : MonoBehaviour
{
    public Player.PlayerNotiferToNotes InputNotifer{get;set;}

    public Player.PlayerNotiferToNotes notiferToNotes;
    public BattleSettings battleSettings;
    public SoundPlayer soundPlayer;
    public int timing{set;get;}//判定の時間
    public float scorePos{set;get;}//譜面位置
    public int laneId;//レーン番号
    public int notesType;//ノーツのアクション種別

    public ScoreData score;
    public JudgeNotifer JudgeNotifer;//判定送信用クラスインスタンス
    
    abstract public void tryJudge(bool Pressed); //押されている時常に呼ばれる　Pressedがtrueなのは押した瞬間

    Renderer renderer ;
    protected virtual void Start()
    {
        renderer=this.gameObject.GetComponent<Renderer>();
        
    }



    // Update is called once per frame
    void Update()
    {
        Move();
        //ノーツ種類によって色を変える
            if(notesType==1){
                renderer.material.color = Color.magenta;
            }else{
                renderer.material.color = Color.cyan;
            }
    }
    protected virtual void Move(){//相対譜面位置を計算し自身を移動　レーンの範囲内なら可視化
        float RelativeScorePos = (float)(scorePos-notiferToNotes.ScorePos)*battleSettings.speedRate;
        RelativeScorePos *= 0.0001f;//スケール変換　しょーみ実装としてはよくない
        this.transform.position = new Vector3(this.transform.position.x,0,RelativeScorePos);
        //Debug.Log(RelativeScorePos);
         
    }

    }

}

public class Judgement{//判定結果を格納するクラス
    public int lane;//レーン番号
    public int judge;//判定結果
    public int notesType;
}

}

