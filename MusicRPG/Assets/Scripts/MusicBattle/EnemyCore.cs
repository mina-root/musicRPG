using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace BattleCharactor{
    public class EnemyCore : MonoBehaviour,Damage.IDamageApplicatable
{
//基本的に敵のデータとか処理とかを記述しておくクラス
//ただ基本的にはデータを揃えておく形で、実際に敵の攻撃処理を行うのは攻撃対象になったパーティキャラ側

//特殊な行動については、正直なところまだ考えてないし、
//行動パターンは譜面側に記述なのでマジでここはステータスデータぐらいかもしれんね
//↑ぜんぜんそんなことなかった
//グラフィックとかの処理もまた別のコンポーネントが行うはずです

//敵用スキルとかぜんぜんつくってないので、あとでつくる必要があります
public int MaxHP=3000;
public int HP=3000;
public int atk=15;
public int def=2;
BattleCharactor.PartyCharactor[] partyCharactors=new BattleCharactor.PartyCharactor[3];
int[] ComboFix = {0,0,0};
    public void  ApplyDamage(Damage.Damage damage){
        int BaseDamage=Damage.DamageLogic.BaseDamage(damage.atk,def,damage.rate,damage.fixedValue);
        int totalDamage=(int)(BaseDamage*Damage.DamageLogic.ComboRate(ComboFix[damage.attacker],damage.ComboAccumlation));
        HP-=totalDamage;
        ComboFix[damage.attacker]+=damage.ComboAccumlation;
    }
    public void DoAction(int target,int combo){
        partyCharactors[target].ApplyDamage(new Damage.Damage(atk,100,0,0,combo));
        //ここハードコーディングしてるからあとで直す
    }
    private void Start() {
        partyCharactors[0]=GameObject.Find("PartyCharactor0").GetComponent<BattleCharactor.PartyCharactor>();
        partyCharactors[1]=GameObject.Find("PartyCharactor1").GetComponent<BattleCharactor.PartyCharactor>();
        partyCharactors[2]=GameObject.Find("PartyCharactor2").GetComponent<BattleCharactor.PartyCharactor>();
        GameObject.Find("Scores").GetComponent<Scores.TurnChangeNotifer>().OnTurnChangeObservable.Subscribe(
            (int tt)=>{
                if(tt==0)for(int i=0;i<ComboFix.Length;i++){
                    ComboFix[i]=0;
                }
            }
        );
        //アクション実行を登録
        GameObject.Find("BattleManager").GetComponent<BattleObjects.ActionTargetModifier>().ActionEventObserbable.Subscribe(
            (BattleObjects.ActionEvent act)=> {
                int c=10;
                    if(act.judge==0)c=1;
                    else if(act.judge==1)c=4;
                    if(act.notesType==0){
                        DoAction(act.target,c);
                    }
                }
        );
    }
}

}
