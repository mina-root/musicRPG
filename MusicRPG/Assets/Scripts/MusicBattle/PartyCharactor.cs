using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
namespace BattleCharactor{


public class PartyCharactor : MonoBehaviour,Damage.IDamageApplicatable
{
    //ここの情報は「戦闘中にのみ使用するもの」
    //戦闘開始時に読み取ってきて格納する

    //名前などのメタ情報
    public string charactor_name="sample";
    public int charactor_num=0;

    //基本ステータスなど
    public int MaxHP=1000;//最大HP 初期値は適当
    public int HP=1000;//現在HP
    public int atk=20;//攻撃力
    public int def=10;
    public int mgk=5;

    //状態異常フラグ
        //現時点で状態異常は用意せず
    //状態異常フラグここまで

    //戦闘中に変動する隠し系パラメータ    
        public int ComboFix{get;set;}=0;//コンボ補正
        public int[] ComboFixHeal=new int[3]; 
        public bool is_active{get;set;}=true;//行動可能か、ターゲットになるかのフラグ　

    //各種パッシブスキル系フラグ

    //パッシブスキル系フラグここまで

    //セットしてあるアクティブスキル
        public Skills.PartyCharactorSkills[] skill = new Skills.PartyCharactorSkills[4];//スキル数は変化するかも

    //セット中アクティブスキルここまで

    //選択したスキル
    public int selectedCommand;

    //敵のオブジェクトの参照
    BattleCharactor.EnemyCore enemy;

    //パーティ全体のデータへの参照
    BattleObjects.PartyStatus partyStatus;

    //パーティキャラクターそれぞれへの参照
    PartyCharactor[] partyCharactors;
    GameObject UIDrawer;

    //ダメージを受けるメソッド
    public void  ApplyDamage(Damage.Damage damage){
        int BaseDamage=Damage.DamageLogic.BaseDamage(damage.atk,def,damage.rate,damage.fixedValue);
        int totalDamage=(int)(BaseDamage*Damage.DamageLogic.ComboRate(ComboFix,damage.ComboAccumlation));
        HP-=totalDamage;
        ComboFix+=damage.ComboAccumlation;
        //ダメージポップアップを表示
        UIDrawer.GetComponent<UIs.DamageDrawer>().DamagePop(charactor_num,totalDamage,true);
    }

    //回復を受けるメソッド
    public void ApplyHeal(Damage.Heal heal){
        int BaseHeal = Damage.DamageLogic.BaseDamage(heal.mgk,0,heal.rate,heal.fixedValue);
        int totalvalue = (int)(BaseHeal*Damage.DamageLogic.ComboRate(ComboFixHeal[heal.healer],heal.ComboAccumlation));
        HP+=totalvalue;
        if(HP>MaxHP)HP=MaxHP;
        ComboFixHeal[heal.healer]+=heal.ComboAccumlation;
        //回復ポップアップを表示
        UIDrawer.GetComponent<UIs.DamageDrawer>().DamagePop(charactor_num,totalvalue,false);
    }

    //判定を受けてアクションを実行するメソッド
    void DoAction(int judge){
        //たんま
        //
        Debug.Log("アクション発生；判定"+judge+" / セットスキルタイプ；"+skill[selectedCommand].skillType);
        int ComboAccumlation=1;
        switch(skill[selectedCommand].skillType){
            case Skills.PartyCharactorSkills.SkillType.attack:
            //攻撃スキルの場合、Damageを発生させて敵にApplyDamage()する
                switch(judge){
                    case -1://Miss
                        ComboAccumlation=skill[selectedCommand].ComboAccumlation_Miss;
                    break;
                    case 0://just
                        ComboAccumlation=skill[selectedCommand].ComboAccumlation_Just;
                        partyStatus.SP+=1;
                    break;
                    case 1://Good
                        ComboAccumlation=skill[selectedCommand].ComboAccumlation_Good;
                    break;
                }
                enemy.ApplyDamage(new Damage.Damage(atk,skill[selectedCommand].rate,skill[selectedCommand].fixedValue,charactor_num,ComboAccumlation));
                break;
            case Skills.PartyCharactorSkills.SkillType.heal:
            //回復スキルの場合
            //処理これから作るぜ～～～～～

            //思ったんだけどこのコンボ補正蓄積の処理スキル種類ごとに分けててええんやろか
                switch(judge){
                    case -1://Miss
                        ComboAccumlation=skill[selectedCommand].ComboAccumlation_Miss;
                    break;
                    case 0://just
                        ComboAccumlation=skill[selectedCommand].ComboAccumlation_Just;
                        partyStatus.SP+=1;
                    break;
                    case 1://Good
                        ComboAccumlation=skill[selectedCommand].ComboAccumlation_Good;
                    break;
                }
                foreach(PartyCharactor chara in partyCharactors){
                    chara.ApplyHeal(new Damage.Heal(mgk,skill[selectedCommand].rate,skill[selectedCommand].fixedValue,charactor_num,ComboAccumlation));
                }
                break;
            case Skills.PartyCharactorSkills.SkillType.support:
                break;
            //サポートスキルの場合
            //こっちも処理考えてない
        }
    }
    //音ゲーターン終了時の処理
    void EndOfTurn(){
        //コンボ補正を0に戻す
        ComboFix=0;
        ComboFixHeal= new int[]{0,0,0};
        //HPが0以下の場合、戦闘不能にする
        if(HP<=0){
            HP=0;
            is_active=false;
        }else{
            is_active=true;
        }
    }
    private void Start() {
        enemy=GameObject.Find("Enemy").GetComponent<BattleCharactor.EnemyCore>();
        partyStatus=GameObject.Find("Party").GetComponent<BattleObjects.PartyStatus>();
        UIDrawer=GameObject.Find("UIDrawer");
        partyCharactors = new PartyCharactor[3];
        partyCharactors[0]= GameObject.Find("PartyCharactor0").GetComponent<BattleCharactor.PartyCharactor>();
        partyCharactors[1]= GameObject.Find("PartyCharactor1").GetComponent<BattleCharactor.PartyCharactor>();
        partyCharactors[2]= GameObject.Find("PartyCharactor2").GetComponent<BattleCharactor.PartyCharactor>();
        //ターン終了時処理を登録
        GameObject.Find("Scores").GetComponent<Scores.TurnChangeNotifer>().OnTurnChangeObservable.Subscribe(
            (int tt)=>{
                if(tt==0){
                    EndOfTurn();
                }
            }
        );
        //アクションイベントをトリガにアクションを実行
        GameObject.Find("BattleManager").GetComponent<BattleObjects.ActionTargetModifier>().ActionEventObserbable.Subscribe(
             (BattleObjects.ActionEvent act)=>{if(act.notesType==1&&act.target==charactor_num){
                DoAction(act.judge);
             }}
             
        );        }
    }
}

