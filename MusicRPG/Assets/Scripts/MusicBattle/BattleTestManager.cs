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
    TextMeshProUGUI txt;

    // Start is called before the first frame update
    IObservable<int> turnChengeSubject;
    void Start()
    {
        turnChangeNotifer.OnTurnChangeObservable.Subscribe(turnChenger);
        txt = this.GetComponent<TextMeshProUGUI>();
       if(txt==null)Debug.Log("fail to load");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void turnChenger(int turntype){
        if(turntype==0){
            txt.text="(Command Time)";
        }else if(turntype==1){
            txt.text="";
        }


    }
}
