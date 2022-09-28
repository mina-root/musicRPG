using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

namespace BattleObjects{
public class ActionTargetModifier : MonoBehaviour
//判定を受け取り、アクションの実行対象を指定するクラス
//アクションを実行可能・アクションを受けることができるかどうかの情報を持つ
//アクションの実行可能判定について
//　　　　戦闘開始時にそもそもオブジェクトが存在しない場合＝パーティの人数が少ない場合は常にその位置のキャラの行動は不可能と判定
//　　　　存在する場合、戦闘不能などの理由で行動を受け付けるかどうかは各パーティキャラオブジェクトの「active」フラグを見る　これはターン切り替え時に更新
//　　　　その他スキルなどのフラグがある場合も都度見る

//現状、存在確認とアクティブフラグのチェックのみを行っている（スキルについては処理が未実装）
{
    [SerializeField] JudgeNotifer judgeNotifer;//判定通知を発行するクラスのインスタンスを設定する
    [SerializeField] Scores.TurnChangeNotifer turnChangeNotifer;//判定通知を発行するクラスのインスタンスを設定する

    BattleCharactor.PartyCharactor[] player = new BattleCharactor.PartyCharactor[3];//各プレイヤーのゲームオブジェクトを格納
    BattleSettings battleSettings;//主にレーン数モードを取得するために設定を管理するコンポーネント
    bool[] player_isactive = new bool[3];//最終的に使うアクティブかどうかのフラグ

    Subject<ActionEvent> ActionEventSubject = new Subject<ActionEvent>();
    public IObservable<ActionEvent> ActionEventObserbable{
        get {return ActionEventSubject;}
        set {this.ActionEventSubject=(Subject<ActionEvent>) value;}
    }

    //アクションを実行する主体
    //アクションの種類（攻撃か被弾か）
    private void Start() {
        turnChangeNotifer.OnTurnChangeObservable.Subscribe(update_isactive);//ターン切り替え時にアクティブ状態を更新
        judgeNotifer.OnJudgeObservable.Subscribe(ActionTargetModify);//イベントを購読　判定通知をトリガーに、アクション実行のイベントを飛ばす
        battleSettings=GameObject.Find("Setting").GetComponent<BattleSettings>();//設定を管理するコンポーネントを取得
    }

    void ActionTargetModify(Scores.Judgement judgement){
        int target=0;
        if(battleSettings.laneMode==BattleSettings.LaneMode.Lanes_3){
            //3レーンモードのとき（現状ではこちらのみ）
            if(player_isactive[judgement.lane]==false)
            //レーンのターゲットが非アクティブのとき、ターゲットを切り替える
            {
                for(int n=0;n<player_isactive.Length;n++){
                    //最も左（番号が若い）キャラクターにターゲットを変更
                    if(player_isactive[n]==true){
                        target=n;
                        break;
                    }
                }
            }else{
                target=judgement.lane;
            }
        }
        else if(battleSettings.laneMode==BattleSettings.LaneMode.Lanes_6){
            //6レーンモードのとき　（現状で使用予定はない）
           if(player_isactive[judgement.lane/2]==false)
           //レーンのターゲットが非アクティブのとき、ターゲットを切り替える
           {
                for(int n=0;n<player_isactive.Length;n++){
                    //最も左（番号が若い）キャラクターにターゲットを変更
                    if(player_isactive[n]==true){
                        target=n;
                        break;
                    }
                }
            }else{
                target=judgement.lane/2;
            }
        }

        //ひとまずデバッグログで対象キャラクター番号を出力しておく
        Debug.Log("Target : "+target);
        //以下に実際に攻撃などのアクションを起こす処理を追加する予定　が、
        //　①ここから直接キャラクターの各種アクションを実行する
        //　②イベント発行を通して実行する（各プレイヤーがすべてのイベントを見て自分に関係のあるやつだけ実行する）
        //のどっちの処理がいいんだろう
        //　さすがに②のほうが変更が効きそう
        ActionEventSubject.OnNext(new ActionEvent(judgement.judge,target,judgement.notesType));
    }

    void update_isactive(int turntype){//アクティブかどうかをチェックし更新   
        player[0] = GameObject.Find("PartyCharactor0").GetComponent<BattleCharactor.PartyCharactor>();
        player[1] = GameObject.Find("PartyCharactor1").GetComponent<BattleCharactor.PartyCharactor>();
        player[2] = GameObject.Find("PartyCharactor2").GetComponent<BattleCharactor.PartyCharactor>();
        for(int num = 0;num<player.Length;num++){
            if(player[num]==null)player_isactive[num] = false;
            else player_isactive[num]=player[num].is_active;
        }
    }
}
}