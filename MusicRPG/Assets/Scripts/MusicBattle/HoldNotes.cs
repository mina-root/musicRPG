using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scores.Notes;

namespace Scores.Notes{
public class HoldNotes : NotesBase  
{
    // Start is called before the first frame update
    bool active = false;
    bool pushed= false;
    public float BPM = 120;
    public int endtiming=0;
    public float endPos=0;
    List<int> timingList=new List<int>();
    List<int> timingList_dummy=new List<int>();
    MeshFilter meshFilter;
    Mesh mesh;
    bool judged=false;
    protected override void Start() {
        base.Start();//とてもきたないコードの書き方　かなしい
        mesh = new Mesh();
        meshFilter = gameObject.GetComponent<MeshFilter>();
        meshFilter.mesh=mesh;
        //判定ポイントを生成してリスト化
        //判定間隔を算出
        float interval;
        if(BPM<120)interval= 30000/BPM;
        else if(BPM<240)interval= 60000/BPM;
        else interval=120000/BPM;
        ;
        int count =1;
        
        while((timing+count*interval)<endtiming){
            timingList.Add((int)(timing+count*interval));
            count+=1;
        }
        //ノーツの長さに応じた長方形のメッシュを生成
        float RelativeEndPos = (float)(endPos-scorePos)*battleSettings.speedRate*0.0001f;

        Vector3[] vertices= {
            new Vector3(1,0,0),
            new Vector3(1,RelativeEndPos,0),
            new Vector3(-1,0,0),
            new Vector3(-1,RelativeEndPos,0),

        };
        int[] triangles = new int[6] {0, 2, 1, 3, 1, 2};
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

    }
    public override void tryJudge(bool Pressed)
    {
        if(Pressed){
            int deltatime= Scores.ScoreLogic.getRoopFixedTiming(score,soundPlayer.getPlaytime())-timing;
            if(System.Math.Abs(deltatime)<battleSettings.justRenge){
                JudgeNotifer.JudgeNotice(0,laneId,notesType);
                judged=true;
            }else if(System.Math.Abs(deltatime)<battleSettings.goodRenge){
                JudgeNotifer.JudgeNotice(1,laneId,notesType);
                judged=true;
            }
            if(deltatime>-battleSettings.goodRenge){
                //先頭の判定範囲以後に入力があった場合、レイヤーを移動
                this.gameObject.layer=8;//ロングノーツ用レイヤーに移動
            }
            
        }
        pushed=true;
        timingList_dummy= new List<int>(timingList);
        foreach(int judgetiming in timingList_dummy){
            int deltatime= Scores.ScoreLogic.getRoopFixedTiming(score,soundPlayer.getPlaytime())-judgetiming;
            if(System.Math.Abs(deltatime)<battleSettings.justRenge){
                JudgeNotifer.JudgeNotice(0,laneId,notesType);
                timingList.Remove(judgetiming);
            }
        }

    }

    override protected void Move(){
        if(timingList.Count>0){//レイヤー移動　押されてないなら通常レイヤーに移動
            if(pushed==false&&Scores.ScoreLogic.getRoopFixedTiming(score,soundPlayer.getPlaytime())<timingList[timingList.Count-1])this.gameObject.layer=7;
        }
        pushed=false;
        //位置更新
        float RelativeScorePos = (float)(scorePos-notiferToNotes.ScorePos)*battleSettings.speedRate;
        RelativeScorePos *= 0.0001f;//スケール変換　しょーみ実装としてはよくない
        this.transform.position = new Vector3(this.transform.position.x,0,RelativeScorePos);
        //過ぎた判定があるならmissを通知
        if(judged==false&&Scores.ScoreLogic.getRoopFixedTiming(score,soundPlayer.getPlaytime())-timing>battleSettings.goodRenge){
            JudgeNotifer.JudgeNotice(-1,laneId,notesType);
            judged=true;
        }
        timingList_dummy=new List<int>(timingList);
        foreach(int holdJudgetiming in timingList_dummy){
            if(Scores.ScoreLogic.getRoopFixedTiming(score,soundPlayer.getPlaytime())-holdJudgetiming>battleSettings.goodRenge){
                JudgeNotifer.JudgeNotice(-1,laneId,notesType);
                timingList.Remove(holdJudgetiming);
            }
        }

        //相対譜面位置が大きくマイナスかつタイミングを過ぎているなら破棄
            if((float)(endPos-notiferToNotes.ScorePos)*battleSettings.speedRate<-5&&Scores.ScoreLogic.getRoopFixedTiming(score,soundPlayer.getPlaytime())-endtiming>battleSettings.goodRenge){
                Destroy( this.gameObject );
            }
        }


}
}