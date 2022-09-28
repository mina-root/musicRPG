using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleCharactor{


public class IActionable 
{
    //アクションを実行できる　を表すインターフェース
    void DoAction(int judge){}
}
}
namespace BattleObjects {
    public class ActionEvent{
        public int notesType;
        public int target;
        public int judge;
        public ActionEvent(int j,int tar,int type){
            judge=j;target=tar;notesType=type;
        }
        }
    }
