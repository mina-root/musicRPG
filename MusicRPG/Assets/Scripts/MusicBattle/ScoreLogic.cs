using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Scores{
public static class ScoreLogic 
{
    public static float getScorePosition(Scores.ScoreData score,int targettiming){//譜面データとタイミングから無補正の譜面位置を返す
        float pos = 0;
        List <ScoreObjects> objlist = score.ScoreObjectsList.FindAll(obj => obj.GetType()==typeof(SetSpeed));
        List <SetSpeed> turnChangeList = objlist.Cast<SetSpeed>().ToList();
        float baseBPM = turnChangeList[0].BPM;
        int timingcount = score.AudioOffset;
        for (int i=0;i<turnChangeList.Count;i++){
            int tmg;
            if(i+1<turnChangeList.Count)tmg = turnChangeList[i+1].timing;
            else tmg=targettiming;
            if(tmg>targettiming)tmg=targettiming;
            pos+=(tmg-timingcount)*turnChangeList[i].BPM/baseBPM;
            timingcount=tmg;
            if(tmg==targettiming)break;
        }
        return pos;
    }

    public static int getRoopFixedTiming(Scores.ScoreData score,int targettiming){
        long timing=targettiming;
        long rooptime=score.AudioLoopEndSample;
        rooptime=rooptime*1000/score.SamplingFreq;
        long length = score.AudioLoopEndSample-score.AudioLoopStartSample;
        length=length*1000/score.SamplingFreq;
        while(timing>rooptime)
            {
                timing-=length;
            }
        return (int)timing;
    }
}
}