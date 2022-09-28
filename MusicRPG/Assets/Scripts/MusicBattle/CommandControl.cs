using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace UIs{
public class CommandControl : MonoBehaviour
{
    // コマンド入力を司るクラス
    //画面への表示はここでは行わない
    //キー入力で対応するキャラのコマンドを順送りする
    //ターン切り替え時（音ゲーターン開始時）に選択されているコマンドを決定して対応するキャラのステータス管理に反映

    //現状だと「その時点でのSPが消費量以上なら選択可」だから、複数キャラが同時にSPを消費するとSPがマイナスになりうる
    //ここはちょっと考える必要があるな.......
    BattleCharactor.PartyCharactor[] partyCharactors = new BattleCharactor.PartyCharactor[3];
    bool commandable = false;
    public int[] selectedCommand = {0,0,0};
    BattleObjects.PartyStatus partyStatus;
    void Start()
    {
        partyCharactors[0]=GameObject.Find("PartyCharactor0").GetComponent<BattleCharactor.PartyCharactor>();
        partyCharactors[1]=GameObject.Find("PartyCharactor1").GetComponent<BattleCharactor.PartyCharactor>();
        partyCharactors[2]=GameObject.Find("PartyCharactor2").GetComponent<BattleCharactor.PartyCharactor>();
        partyStatus = GameObject.Find("Party").GetComponent<BattleObjects.PartyStatus>();
        gameObject.GetComponent<Player.PlayerInput>().OnButtonObservable.Subscribe(CommandSelect);
        GameObject.Find("Scores").GetComponent<Scores.TurnChangeNotifer>().OnTurnChangeObservable.Subscribe(tt => commandable = (tt==0));
        GameObject.Find("Scores").GetComponent<Scores.TurnChangeNotifer>().OnTurnChangeObservable.Subscribe(
            (int tt) => {
                if(tt==1){
                    for(int i=0;i<partyCharactors.Length;i++){
                       partyCharactors[i].selectedCommand=selectedCommand[i];
                       partyStatus.SP-=partyCharactors[i].skill[selectedCommand[i]].SPCost;
                    }   
                }
            }
        );
    }

    // Update is called once per frame
    void CommandSelect(int[] input)
    {
        //Debug.Log(commandable);
        if(commandable){
            for(int i=0;i<input.Length;i++){
                if(input[i]==1){
                    if(partyCharactors[i]!=null){
                        while(true){
                        selectedCommand[i]++;
                        if(selectedCommand[i]>3||partyCharactors[i].skill[selectedCommand[i]]==null){
                            selectedCommand[i]=0;
                            break;
                        }
                        if(partyCharactors[i].skill[selectedCommand[i]].SPCost<partyStatus.SP){
                            break;
                        }
                        }

                    //Debug.Log("Command : "+selectedCommand[i]);
                    }
                }
            }
        }
    }
}
}