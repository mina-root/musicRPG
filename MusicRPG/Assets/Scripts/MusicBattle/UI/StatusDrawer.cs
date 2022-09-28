using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniRx;
using DG.Tweening;

public class StatusDrawer : MonoBehaviour
//ステータスデータ（キャラのHPなど）を描画するクラス
//現状では固定場所にHPを表示するだけ
//テキストのプレハブを設定してstart時にUIオブジェクト生成してるので、
//あらかじめゲームオブジェクトをヒエラルキーに配置しておく必要はない
//
{
    [SerializeField]GameObject UITextPrefab ;
    [SerializeField]float pivotX;//基準X座標
    [SerializeField]float pivotY;//基準Y座標
    [SerializeField]float XInterval;//キャラごとのX座標の間隔
    [SerializeField]UIs.CommandControl commandControl;//コマンド設定クラス
    [SerializeField]float StatusMoveDuration;
    BattleCharactor.PartyCharactor[] partyCharactors =new BattleCharactor.PartyCharactor[3];
    BattleCharactor.EnemyCore enemy;
    PCUIObjects[] pCUIObjects = new PCUIObjects[3];
    List<RectTransform> rectTransforms = new List<RectTransform>();//RectTranceformまとめて突っ込むリスト
    BattleObjects.PartyStatus partyStatus ;
    GameObject[] StatusParent = new GameObject[3];
    TextMeshProUGUI SPDisplay,enemyHPDisplay;//SP表示用,敵HP表示用
    void Start()
    {
        partyCharactors[0]=GameObject.Find("PartyCharactor0").GetComponent<BattleCharactor.PartyCharactor>();
        partyCharactors[1]=GameObject.Find("PartyCharactor1").GetComponent<BattleCharactor.PartyCharactor>();
        partyCharactors[2]=GameObject.Find("PartyCharactor2").GetComponent<BattleCharactor.PartyCharactor>();
        enemy=GameObject.Find("Enemy").GetComponent<BattleCharactor.EnemyCore>();
        //SP表示用のゲームオブジェクトを生成
        GameObject g = GameObject.Instantiate(UITextPrefab);
        g.transform.SetParent(gameObject.transform);
        g.GetComponent<RectTransform>().position=new Vector3(XInterval*2+pivotX+30,pivotY+30);
        rectTransforms.Add(g.GetComponent<RectTransform>());
        SPDisplay = g.GetComponent<TextMeshProUGUI>();
        //敵のHPを表示するゲームオブジェクトを生成
        g = GameObject.Instantiate(UITextPrefab);
        g.transform.SetParent(gameObject.transform);
        g.GetComponent<RectTransform>().position=new Vector3(900,1000,0);
        rectTransforms.Add(g.GetComponent<RectTransform>());
        enemyHPDisplay = g.GetComponent<TextMeshProUGUI>();
        for(int n=0;n<partyCharactors.Length;n++){
            if(partyCharactors[n]!=null){
                //書き方がよくないけど

                //ひとりぶんのステータス表示オブジェクトを生成
                StatusParent[n] = new GameObject("PartyCharactor"+n+"Status");
                StatusParent[n].transform.SetParent(gameObject.transform);
                RectTransform rectTransform = StatusParent[n].AddComponent<RectTransform>();
                rectTransform.position=new Vector3(XInterval*(n)+pivotX,pivotY,0);
                
                //テキスト表示用のUIをまとめて生成し、場所を指定
                pCUIObjects[n]= new PCUIObjects();
                g = GameObject.Instantiate(UITextPrefab);
                g.transform.SetParent(StatusParent[n].transform);
                //rectTransforms.Add(g.GetComponent<RectTransform>());
                g.GetComponent<RectTransform>().localPosition=new Vector3(60,-32,0);
                
                pCUIObjects[n].MaxHP = g.GetComponent<TextMeshProUGUI>();
                g = GameObject.Instantiate(UITextPrefab);
                g.transform.SetParent(StatusParent[n].transform);
                //rectTransforms.Add(g.GetComponent<RectTransform>());
                g.GetComponent<RectTransform>().localPosition=new Vector3(12,-32,0);
                
                pCUIObjects[n].HP = g.GetComponent<TextMeshProUGUI>();
                g = GameObject.Instantiate(UITextPrefab);
                g.transform.SetParent(StatusParent[n].transform);
                //rectTransforms.Add(g.GetComponent<RectTransform>());
                g.GetComponent<RectTransform>().localPosition=new Vector3(0,0,0);
                pCUIObjects[n].CharactorName = g.GetComponent<TextMeshProUGUI>();
                pCUIObjects[n].CharactorName.text=partyCharactors[n].charactor_name;
                
                g = GameObject.Instantiate(UITextPrefab);
                g.transform.SetParent(StatusParent[n].transform);
                g.GetComponent<RectTransform>().localPosition=new Vector3(0,-64,0);
                //rectTransforms.Add(g.GetComponent<RectTransform>());
                pCUIObjects[n].Skill[0] = g.GetComponent<TextMeshProUGUI>();
                if(partyCharactors[n].skill[0]!=null){
                    pCUIObjects[n].Skill[0].text=partyCharactors[n].skill[0].skillName;
                    if(partyCharactors[n].skill[0].SPCost>0)pCUIObjects[n].Skill[0].text+="("+partyCharactors[n].skill[0].SPCost.ToString()+")";
                    }
                else pCUIObjects[n].Skill[0].text="";


                
                g = GameObject.Instantiate(UITextPrefab);
                g.transform.SetParent(StatusParent[n].transform);
                //rectTransforms.Add(g.GetComponent<RectTransform>());
                g.GetComponent<RectTransform>().localPosition=new Vector3(0,-94,0);
                pCUIObjects[n].Skill[1] = g.GetComponent<TextMeshProUGUI>();
                if(partyCharactors[n].skill[1]!=null){
                    pCUIObjects[n].Skill[1].text=partyCharactors[n].skill[1].skillName;
                    if(partyCharactors[n].skill[1].SPCost>0)pCUIObjects[n].Skill[1].text+="("+partyCharactors[n].skill[1].SPCost.ToString()+")";
                    }
                else pCUIObjects[n].Skill[1].text="";
                
                g = GameObject.Instantiate(UITextPrefab);
                g.transform.SetParent(StatusParent[n].transform);
                g.GetComponent<RectTransform>().localPosition=new Vector3(0,-124,0);
                //rectTransforms.Add(g.GetComponent<RectTransform>());
                pCUIObjects[n].Skill[2] = g.GetComponent<TextMeshProUGUI>();
                if(partyCharactors[n].skill[2]!=null){
                    pCUIObjects[n].Skill[2].text=partyCharactors[n].skill[2].skillName;
                    if(partyCharactors[n].skill[2].SPCost>0)pCUIObjects[n].Skill[2].text+="("+partyCharactors[n].skill[2].SPCost.ToString()+")";
                    }
                else pCUIObjects[n].Skill[2].text="";
                
                g = GameObject.Instantiate(UITextPrefab);
                g.transform.SetParent(StatusParent[n].transform);
                g.GetComponent<RectTransform>().localPosition=new Vector3(0,-154,0);
                //rectTransforms.Add(g.GetComponent<RectTransform>());
                pCUIObjects[n].Skill[3] = g.GetComponent<TextMeshProUGUI>();
                if(partyCharactors[n].skill[3]!=null){
                    pCUIObjects[n].Skill[3].text=partyCharactors[n].skill[3].skillName;
                    if(partyCharactors[n].skill[3].SPCost>0)pCUIObjects[n].Skill[3].text+="("+partyCharactors[n].skill[3].SPCost.ToString()+")";
                    }
                else pCUIObjects[n].Skill[3].text="";
                
            }
        }
        //ターンチェンジ時の動作を登録
        Scores.TurnChangeNotifer turnChangeNotifer= GameObject.Find("Scores").GetComponent<Scores.TurnChangeNotifer>();
        turnChangeNotifer.OnTurnChangeObservable.Subscribe(tt => StartCoroutine(ToggleSkillsDisplay(tt)));
        //パーティ共通ステータス管理クラスを読み込み
        partyStatus = GameObject.Find("Party").GetComponent<BattleObjects.PartyStatus>();
        
        
    }

    // Update is called once per frame
    void Update()
    {   
        for(int n=0;n<pCUIObjects.Length;n++){
            if(pCUIObjects[n]!=null&&partyCharactors[n]!=null){
                //HPの表示を更新
                pCUIObjects[n].HP.text=partyCharactors[n].HP.ToString();
                pCUIObjects[n].MaxHP.text=" / "+partyCharactors[n].MaxHP.ToString();
                //選択中スキルの表示を更新
                for(int i=0;i<pCUIObjects[n].Skill.Length;i++){
                    pCUIObjects[n].Skill[i].color = Color.white;
                }
                pCUIObjects[n].Skill[commandControl.selectedCommand[n]].color=Color.blue;
                //SPを更新
                SPDisplay.text = "SP : "+partyStatus.SP;
                //敵HPを更新
                enemyHPDisplay.text="HP : "+enemy.HP;
            }
        }
    }
    IEnumerator ToggleSkillsDisplay(int turntype){
        //ターン切り替え時にスキル一覧とかの表示を隠す
        //現状では場所を動かしてるだけ
        switch(turntype){
            case 0:
                foreach(RectTransform item in rectTransforms){
                    item.position+=new Vector3(0,120,0);
                }
                for(int i=0;i<StatusParent.Length;i++){
                    StatusParent[i].GetComponent<RectTransform>().DOMove(new Vector3(pivotX+(i)*XInterval,pivotY,0),StatusMoveDuration);
                    yield return new WaitForSeconds(0.1f);
                }
            break;
            case 1:
                foreach(RectTransform item in rectTransforms){
                    item.position+=new Vector3(0,-120,0);
                }
                for(int i=0;i<StatusParent.Length;i++){
                    StatusParent[i].GetComponent<RectTransform>().DOMove(new Vector3(pivotX+(i)*XInterval,pivotY-120,0),StatusMoveDuration);
                    yield return new WaitForSeconds(0.1f);
                }
            break;
        }   
    }
    class PCUIObjects{
        public TextMeshProUGUI MaxHP,HP,CharactorName;
        public TextMeshProUGUI[] Skill=new TextMeshProUGUI[4];
    }
}