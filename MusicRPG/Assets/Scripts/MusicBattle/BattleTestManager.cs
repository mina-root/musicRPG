using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using TMPro;
using Scores;

public class BattleTestManager : MonoBehaviour
{
    [SerializeField]TurnChangeNotifer turnChangeNotifer;
    [SerializeField]Camera maincamera;
    [SerializeField]GameObject partyCharactorPrefab;
    TextMeshProUGUI txt;

    // Start is called before the first frame update
    IObservable<int> turnChengeSubject;
    void Awake() 
    {
        turnChangeNotifer.OnTurnChangeObservable.Subscribe(turnChenger);
        txt = this.GetComponent<TextMeshProUGUI>();
       if(txt==null)Debug.Log("fail to load");
        //仮のパーティキャラたちを3人分配置
        GameObject partychara_0 = GameObject.Instantiate(partyCharactorPrefab,new Vector3(0,0,0),Quaternion.identity);
        partychara_0.name="PartyCharactor0";
        BattleCharactor.PartyCharactor PC0=partychara_0.GetComponent<BattleCharactor.PartyCharactor>();
        PC0.charactor_name="Player0";
        PC0.charactor_num=0;
        PC0.MaxHP=1500;
        PC0.HP=1500;
        PC0.atk=16;
        PC0.def=1;
        PC0.mgk=8;

        PC0.skill[0]= Resources.Load($"Data/Skills/PartyCharactorSkills/たたかう") as Skills.PartyCharactorSkills;

        GameObject partychara_1 = GameObject.Instantiate(partyCharactorPrefab,new Vector3(0.1f,0,0),Quaternion.identity);
        partychara_1.name="PartyCharactor1";
        BattleCharactor.PartyCharactor PC1=partychara_1.GetComponent<BattleCharactor.PartyCharactor>();
        PC1.charactor_name="Player1";
        PC1.charactor_num=1;
        PC1.MaxHP=1200;
        PC1.HP=1200;
        PC1.atk=22;
        PC1.def=0;
        PC1.mgk=9;
        PC1.skill[0]= Resources.Load($"Data/Skills/PartyCharactorSkills/たたかう") as Skills.PartyCharactorSkills;
        PC1.skill[1]= Resources.Load($"Data/Skills/PartyCharactorSkills/つよいこうげき") as Skills.PartyCharactorSkills;

        GameObject partychara_2 = GameObject.Instantiate(partyCharactorPrefab,new Vector3(0.2f,0,0),Quaternion.identity);
        partychara_2.name="PartyCharactor2";
        BattleCharactor.PartyCharactor PC2=partychara_2.GetComponent<BattleCharactor.PartyCharactor>();
        PC2.charactor_name="Player2";
        PC2.charactor_num=2;
        PC2.MaxHP=900;
        PC2.HP=900;
        PC2.atk=12;
        PC2.def=1;
        PC2.mgk=15;
        PC2.skill[0]= Resources.Load($"Data/Skills/PartyCharactorSkills/たたかう") as Skills.PartyCharactorSkills;
        PC2.skill[1]= Resources.Load($"Data/Skills/PartyCharactorSkills/かいふく") as Skills.PartyCharactorSkills;
    }

    void turnChenger(int turntype){
        if(turntype==0){
            txt.text="(Command Time)";
        }else if(turntype==1){
            txt.text="";
        }


    }
}
