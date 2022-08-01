using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Scores {


        [Serializable]public class ScoreObjects{}
        [Serializable]public class Tap : ScoreObjects{
            [SerializeReference]public int timing ;
            [SerializeReference]public int lane  ;
            [SerializeReference]public int actionType;
        }

        [Serializable]public class Hold: ScoreObjects{
            [SerializeReference]public int timing ;
           [SerializeReference]public int endtiming ;
            [SerializeReference]public int lane  ;
            [SerializeReference]public int actionType;
        }
       [Serializable]public class SetSpeed: ScoreObjects{
            [SerializeReference]public int timing ;
            [SerializeReference]public float BPM ;
            [SerializeReference]public float LPB  ; //Line Per Ber
        }
        [Serializable]public class TurnChange: ScoreObjects {
            [SerializeReference]public int timing ;
            [SerializeReference]public int turntype ;
        }
        //LoopとCameraは必要が生じたら定義



    [Serializable][CreateAssetMenu]public class ScoreData : ScriptableObject{
        public int AudioOffset;
        public int AudioLoopStartSample;
        public int AudioLoopEndSample;
        public int SamplingFreq;

        [SerializeReference]public List<ScoreObjects> ScoreObjectsList;
    }
}