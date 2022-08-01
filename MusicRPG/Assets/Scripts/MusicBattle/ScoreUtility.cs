using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Scores{
public class ScoreUtility : MonoBehaviour
{
    public ScoreData scoreData ;

    // Start is called before the first frame update
    void Start()
    {   

    }

       // [MenuItem("Create/ScoreData")]
        public static void CreateScore(){
        ScoreData Data =ScriptableObject.CreateInstance<ScoreData>();
        Data.AudioOffset=0;
        Data.AudioLoopEndSample=4658824;
        Data.AudioLoopStartSample=84706;
        Data.SamplingFreq=48000;
        List<ScoreObjects> scoreObjects= new List<ScoreObjects>();
        scoreObjects.Add(new SetSpeed{
                timing=0,
                BPM=100,
                LPB=4.0f
        });
        scoreObjects.Add(new TurnChange{
                timing=0,
                turntype = 0
        });
        scoreObjects.Add(new TurnChange{
                timing=14118,
                turntype = 1
        });
        scoreObjects.Add(new Tap{
                timing=16103,
                lane =0
        });
        scoreObjects.Add(new Tap{
                timing=16324,
                lane =1
        });
        scoreObjects.Add(new Tap{
                timing=16654,
                lane =2
        });
        scoreObjects.Add(new Tap{
                timing=16985,
                lane = 0
        });
        scoreObjects.Add(new Tap{
                timing=17206,
                lane =1
        });
        scoreObjects.Add(new Tap{
                timing=17426,
                lane =2
        });

         scoreObjects.Add(new Hold{
                timing=17647,
                endtiming=18088,
                lane =0
        });
        scoreObjects.Add(new Hold{
                timing=18309,
                endtiming=19413,
                lane =2
        });
        scoreObjects.Add(new Tap{
                timing=19632,
                lane =0
        });
        scoreObjects.Add(new Tap{
                timing=19853,
                lane =1
        });
        scoreObjects.Add(new Tap{
                timing=20184,
                lane =2
        });
        scoreObjects.Add(new Tap{
                timing=20515,
                lane = 0
        });
        scoreObjects.Add(new Tap{
                timing=20735,
                lane =1
        });
        scoreObjects.Add(new Tap{
                timing=20956,
                lane =2
        });
        scoreObjects.Add(new Hold{
                timing=21176,
                endtiming=21397,
                lane =1
        });
        scoreObjects.Add(new Hold{
                timing=21838,
                endtiming=22721,
                lane =0
        });
        scoreObjects.Add(new Tap{
                timing=22941,
                lane =2
        });
                scoreObjects.Add(new Tap{
                timing=23272,
                lane =2
        });
                scoreObjects.Add(new Tap{
                timing=23603,
                lane =1
        });
                scoreObjects.Add(new Tap{
                timing=23824,
                lane =2
        });
                scoreObjects.Add(new Tap{
                timing=24706,
                lane =0
        });
                scoreObjects.Add(new Tap{
                timing=25368,
                lane =0
        });
                scoreObjects.Add(new Tap{
                timing=26029,
                lane =1
        });
                        scoreObjects.Add(new Tap{
                timing=26029,
                lane =1
        });
                scoreObjects.Add(new Tap{
                timing=26360,
                lane =0
        });
        scoreObjects.Add(new Hold{
                timing=26471,
                endtiming=27352,
                lane =1
        });
        scoreObjects.Add(new Hold{
                timing=27354,
                endtiming=28234,
                lane =2
        });
        scoreObjects.Add(new Tap{
                timing=28236,
                lane =0
        });
        scoreObjects.Add(new Tap{
                timing=28456,
                lane =1
        });
        scoreObjects.Add(new Tap{
                timing=28566,
                lane =0
        });
        scoreObjects.Add(new Tap{
                timing=28787,
                lane =1
        });
        scoreObjects.Add(new Tap{
                timing=29007,
                lane = 0
        });
        scoreObjects.Add(new TurnChange{
                timing=30000,
                turntype = 0
        });

        scoreObjects.Add(new TurnChange{
                timing=40588,
                turntype = 1
        });
        scoreObjects.Add(new Tap{
                timing=44338,
                lane = 0
        });
        scoreObjects.Add(new Tap{
                timing=44449,
                lane = 1
        });
        scoreObjects.Add(new Tap{
                timing=44559,
                lane = 0
        });
        scoreObjects.Add(new Tap{
                timing=44669,
                lane = 1
        });
scoreObjects.Add(new Hold{
        timing=44890,
 endtiming=45661,
   lane =0
        });
         scoreObjects.Add(new Tap{
                timing= 45221,
                lane = 1
        });
       scoreObjects.Add(new Tap{
                timing=45441,
                lane = 2
        });
       scoreObjects.Add(new Tap{
                timing=45662,
                lane = 1
        });

        scoreObjects.Add(new Hold{
        timing=45882,
 endtiming=46213,
   lane =0
        });
        scoreObjects.Add(new Hold{
        timing=45882,
 endtiming=46213,
   lane =2
        });
                scoreObjects.Add(new Hold{
        timing=46544,
 endtiming=46984,
   lane =1
        });
        scoreObjects.Add(new Hold{
        timing=46985,
 endtiming=47205,
   lane =0
        });
        scoreObjects.Add(new Hold{
        timing=47206,
 endtiming=47646,

   lane =1
        });

        scoreObjects.Add(new Tap{
                timing=47868,
                lane = 2
        });
        scoreObjects.Add(new Tap{
                timing=47978,
                lane = 1
        });
        scoreObjects.Add(new Tap{
                timing=48088,
                lane = 2
        });
        scoreObjects.Add(new Tap{
                timing=48199,
                lane = 1
        });
                scoreObjects.Add(new Hold{
        timing=48419,
 endtiming=49411,

   lane =2
        });
        scoreObjects.Add(new Tap{
                timing=48750,
                lane = 0
        });
        scoreObjects.Add(new Tap{
                timing=48971,
                lane = 1
        });
        scoreObjects.Add(new Tap{
                timing=49191,
                lane = 0
        });
         scoreObjects.Add(new Hold{
        timing=49412,
 endtiming=49632,
   lane =1
        });
                scoreObjects.Add(new Tap{
                timing=49743,
                lane = 2
        });
        scoreObjects.Add(new Hold{
        timing=50074,
 endtiming=50956,
   lane =1
        });


        scoreObjects.Add(new Tap{
                timing=51176,
                lane = 0
        });
        scoreObjects.Add(new Tap{
                timing=51397,
                lane = 0
        });
         scoreObjects.Add(new Tap{
                timing=51507,
                lane = 1
        });
        scoreObjects.Add(new Tap{
                timing=51728,
                lane = 1
        });
        scoreObjects.Add(new Tap{
                timing=51838,
                lane = 0
        });
        scoreObjects.Add(new Hold{
        timing=52509,
 endtiming=52721,
   lane =2
        });
        scoreObjects.Add(new Tap{
                timing=52279,
                lane = 1
        });
        scoreObjects.Add(new Tap{
                timing=52500,
                lane = 0
        });
        scoreObjects.Add(new Tap{
                timing=52721,
                lane = 1
        });

        scoreObjects.Add(new Tap{
                timing=52941,
                lane = 2
        });
        scoreObjects.Add(new Tap{
                timing=53162,
                lane = 2
        });
        scoreObjects.Add(new Tap{
                timing=53272,
                lane = 1
        });
        scoreObjects.Add(new Tap{
                timing=53493,
                lane = 0
        });
        scoreObjects.Add(new Tap{
                timing=53603,
                lane = 2
        });
        scoreObjects.Add(new Hold{
        timing=53824,
        endtiming=54265,
        lane =0
        });
         scoreObjects.Add(new Tap{
                timing=54485,
                lane = 2
        });
        scoreObjects.Add(new Tap{
                timing=54596,
                lane = 1
        });
        scoreObjects.Add(new Hold{
        timing=54706,
        endtiming=55036,
        lane =0
        });
        scoreObjects.Add(new Hold{
        timing=55037,
        endtiming=55367,
        lane =2
        });
        scoreObjects.Add(new Hold{
        timing=55368,
        endtiming=55587,
        lane =1
        });
        scoreObjects.Add(new Hold{
        timing=55588,
        endtiming=55918,
        lane =2
        });
        scoreObjects.Add(new Hold{
        timing=55919,
        endtiming=56249,
        lane =0
        });
        scoreObjects.Add(new Hold{
        timing=56251,
        endtiming=56470,
        lane =1
        });
        scoreObjects.Add(new Tap{
                timing=56471,
                lane = 0
        });
        scoreObjects.Add(new Tap{
                timing=56691,
                lane = 1
        });
        scoreObjects.Add(new Tap{
                timing=56801,
                lane = 0
        });
        scoreObjects.Add(new Tap{
                timing=57022,
                lane = 2
        });
        scoreObjects.Add(new Tap{
                timing=57243,
                lane = 0
        });
        











scoreObjects.Add(new TurnChange{
                timing=58225,
                turntype = 0
        });

        scoreObjects.Add(new TurnChange{
                timing=70588,
                turntype = 1
        });

        scoreObjects.Add(new Tap{
                timing=71912,
                lane = 2
        });
        scoreObjects.Add(new Tap{
                timing=72132,
                lane = 1
        });
        scoreObjects.Add(new Hold{
        timing=72353,
        endtiming=75441,
        lane =2
        });
         scoreObjects.Add(new Tap{
                timing=72684,
                lane = 0

        });
         scoreObjects.Add(new Tap{
                timing=73015,
                lane = 1
        });
        scoreObjects.Add(new Hold{
        timing=73235,
        endtiming=73676,
        lane =0
        });
        scoreObjects.Add(new Tap{
                timing=73897,
                lane = 1
        });
        scoreObjects.Add(new Hold{
        timing=74118,
        endtiming=75441,
        lane =0
        
        });

        scoreObjects.Add(new Hold{
        timing=75882,
        endtiming=78970,
        lane =0
        });
         scoreObjects.Add(new Tap{
                timing=76213,
                lane = 2

        });
         scoreObjects.Add(new Tap{
                timing=76544,
                lane = 1
        });
        scoreObjects.Add(new Hold{
        timing=76765,
        endtiming=77206,
        lane =2
        });
        scoreObjects.Add(new Tap{
                timing=77426,
                lane = 1
        });
        scoreObjects.Add(new Tap{
                timing=79412,
                lane = 0
        });
        scoreObjects.Add(new Tap{
                timing=79743,
                lane = 1
        });
        scoreObjects.Add(new Tap{
                timing=80074,
                lane = 0
        });
        scoreObjects.Add(new Tap{
                timing=80294,
                lane = 2
        });
        scoreObjects.Add(new Tap{
                timing=80515,
                lane = 1
        });
        scoreObjects.Add(new Tap{
                timing=80735,
                lane = 0
        });
        scoreObjects.Add(new Tap{
                timing=80956,
                lane = 1
        });
        scoreObjects.Add(new Hold{
                timing=81176,
                endtiming=82058,
                lane =2
        });
        scoreObjects.Add(new Hold{
                timing=82059,
                endtiming=82499,
                lane =1
        });
        scoreObjects.Add(new Tap{
                timing=82721,
                lane = 0
        });
        scoreObjects.Add(new Tap{
                timing=82941 ,
                lane = 2
        });
        scoreObjects.Add(new Tap{
                timing=83051 ,
                lane = 1
        });
        scoreObjects.Add(new Tap{
                timing=83162 ,
                lane = 2
        });
        scoreObjects.Add(new Tap{
                timing=83272 ,
                lane = 0
        });
        scoreObjects.Add(new Tap{
                timing=83382 ,
                lane = 1
        });
        scoreObjects.Add(new Tap{
                timing=83493 ,
                lane = 0
        });
        scoreObjects.Add(new Tap{
                timing=83603 ,
                lane = 2
        });
        scoreObjects.Add(new Tap{
                timing=83713 ,
                lane = 1
        });

scoreObjects.Add(new Tap{
                timing=83824 ,
                lane = 2
        });
        scoreObjects.Add(new Tap{
                timing=83934 ,
                lane = 0
        });
        scoreObjects.Add(new Tap{
                timing=84044 ,
                lane = 1
        });
        scoreObjects.Add(new Tap{
                timing=84154 ,
                lane = 0
        });
        scoreObjects.Add(new Tap{
                timing=84265 ,
                lane = 2
        });

        scoreObjects.Add(new Tap{
                timing=84375 ,
                lane = 1
        });
        scoreObjects.Add(new Tap{
                timing=84485 ,
                lane = 2
        });        
        scoreObjects.Add(new Tap{
                timing=84596 ,
                lane = 0
        });
        scoreObjects.Add(new Tap{
                timing=84706 ,
                lane = 2
        });
        scoreObjects.Add(new Tap{
                timing=84816 ,
                lane = 1
        });
        scoreObjects.Add(new Tap{
                timing=84926 ,
                lane = 2
        });
        scoreObjects.Add(new Tap{
                timing=85037,
                lane = 0
        });
        scoreObjects.Add(new Tap{
                timing=85147 ,
                lane = 1
        });
        scoreObjects.Add(new Tap{
                timing=85257 ,
                lane = 0
        });
        scoreObjects.Add(new Tap{
                timing=85368 ,
                lane = 2
        });

scoreObjects.Add(new Tap{
                timing=85478 ,
                lane = 1
        });
        scoreObjects.Add(new Tap{
                timing=85588 ,
                lane = 2
        });
        scoreObjects.Add(new Tap{
                timing=85699 ,
                lane = 0
        });
        scoreObjects.Add(new Tap{
                timing=85809 ,
                lane = 1
        });
        scoreObjects.Add(new Tap{
                timing=85919 ,
                lane = 0
        });

        scoreObjects.Add(new Tap{
                timing=86029 ,
                lane = 2
        });
        scoreObjects.Add(new Tap{
                timing=86140 ,
                lane = 1
        });
         scoreObjects.Add(new Tap{
                timing=86250 ,
                lane = 2
        });
        scoreObjects.Add(new Tap{
                timing=86360 ,
                lane = 0
        });


scoreObjects.Add(new TurnChange{
                timing=97059,
                turntype = 0
        });





        Data.ScoreObjectsList=scoreObjects;
        //AssetDatabase.CreateAsset(Data,"Assets/Scores/testscore.asset");
        }
}}
