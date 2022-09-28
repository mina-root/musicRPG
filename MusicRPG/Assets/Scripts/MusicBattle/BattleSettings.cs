using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSettings : MonoBehaviour
{
    public enum LaneMode{
        Lanes_3,
        Lanes_6
    };
    [SerializeField] public int justRenge = 50;//Just判定の幅(ms)
    [SerializeField] public int goodRenge = 120;//Good判定の幅(ms)
    [SerializeField] public float speedRate = 8;//ノーツが降ってくる速度の倍率
    [SerializeField] public float LengthOfLane = 20;//レーンの長さ
    [SerializeField] public int offsettime = 0;//ノーツのタイミング補正
    [SerializeField] public LaneMode laneMode = LaneMode.Lanes_3; 

 
}
