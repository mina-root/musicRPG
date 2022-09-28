using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using DG.Tweening;
using TMPro;
public class JudgeDrawer : MonoBehaviour
{
    // すると
    [SerializeField]JudgeNotifer judgeNotifer;
    [SerializeField]float DrawTime = 0.5f;
    [SerializeField]GameObject JudgeDrawingPrefab;
    [SerializeField]Camera maincamera;
    [SerializeField]Canvas canvas;
    void Start()
    {
        judgeNotifer.OnJudgeObservable.Subscribe(judge => StartCoroutine(JudgeDraw(judge)));
    }
    IEnumerator JudgeDraw(Scores.Judgement judge){
        //判定ラインの位置に
        Vector2 screenPos = maincamera.WorldToScreenPoint(new Vector3(judge.lane*0.125f-0.125f,0f,0f));
        //RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(),screenPos,null,out Vector2 UIPos);
        GameObject judgeDrawing = GameObject.Instantiate(JudgeDrawingPrefab,new Vector3(0,0,0),Quaternion.identity);
        judgeDrawing.transform.SetParent(this.transform);
        switch(judge.judge){
            case -1:
                judgeDrawing.GetComponent<TextMeshProUGUI>().text="Miss";
                break;
            case 0:
                judgeDrawing.GetComponent<TextMeshProUGUI>().text="Just";
                break;
            case 1:
                judgeDrawing.GetComponent<TextMeshProUGUI>().text="Good";
                break;
        }
        RectTransform rect = judgeDrawing.GetComponent<RectTransform>();
        rect.position = screenPos;
        //Debug.Log(UIPos);
        rect.DOMove(screenPos+new Vector2(0,100),DrawTime);
        yield return new WaitForSeconds(DrawTime);
        Destroy(judgeDrawing);
    }



}
