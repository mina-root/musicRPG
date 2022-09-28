using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ダメージを扱うクラス群
//monobehaviorを継承していないのでファイル名はわかりやすいようにDamageにした

namespace Damage{
    public interface IDamageApplicatable
    //[ダメージを受けることができる]を表すインターフェース
    {
        void ApplyDamage(Damage damage);

    }

    public class Damage{
        //ダメージそのものを表すオブジェクト
        public int atk=0;
        public int rate=100;
        public int fixedValue=0;
        public int attacker=0;
        public int ComboAccumlation;
        public Damage(int at,int ra,int fix,int atker,int combo){
            atk=at;rate=ra;fixedValue=fix;attacker=atker;ComboAccumlation=combo;
        }
        public Damage(){}
    }

    public static class DamageLogic{
        //ダメージ関連のロジックをまとめておくクラス
        public static float ComboRate(int ComboAccumlationBase,int ComboAccumlation){
        //コンボ補正を考慮したダメージ合計倍率を算出する
            float finalRate=0;
                for(int i=ComboAccumlationBase;i<ComboAccumlation+ComboAccumlationBase;i++){
                    finalRate+=BaseComboRate(i);
                }
            Debug.Log("FinalRate="+finalRate);
            return finalRate;

        }
        public static float BaseComboRate(int ComboAccumlation){
        //単発コンボ補正倍率を算出する
            int n = (100-ComboAccumlation)* (100-ComboAccumlation);
            if(ComboAccumlation>68)n=1000;
            return (float)n/10000;
        }
        public static int BaseDamage(int atk,int def,int rate,int fixedValue){
            //ダメージ計算式
            Debug.Log("BaseDamage : "+((atk-def)*rate/100+fixedValue+1).ToString());
            return (atk-def)*rate/100+fixedValue+1;
        }
    }
}


