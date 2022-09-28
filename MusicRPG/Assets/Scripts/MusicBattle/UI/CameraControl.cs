using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using DG.Tweening;

public class CameraControl : MonoBehaviour
{
    // Start is called before the first frame update
    //[SerializeField]GameObject MainCamera;
    [SerializeField]Vector3 SoundBattlePosition=new Vector3(0f,0.3f,-0.5f);
    [SerializeField]Vector3 SoundBattleDirection =new Vector3(20f,0f,0f);
    [SerializeField]Vector3 CommandTurnPosition;
    [SerializeField]Vector3 CommandTurnDirection;
    [SerializeField]float MoveTime=0.6f;
    int timecount;
    float TargetCameraPos,CameraPos;
    void Start()
    {
        GameObject.Find("Scores").GetComponent<Scores.TurnChangeNotifer>().OnTurnChangeObservable.Subscribe(CameraMoveAtTurnChange);
    }

    // Update is called once per frame
    void CameraMoveAtTurnChange(int turntype){
        if(turntype==0){
            this.transform.DOMove(CommandTurnPosition,MoveTime);
            this.transform.DORotate(CommandTurnDirection,MoveTime);
        }
        else if(turntype==1){
            this.transform.DOMove(SoundBattlePosition,MoveTime);
            this.transform.DORotate(SoundBattleDirection,MoveTime);
        }
    }
}
