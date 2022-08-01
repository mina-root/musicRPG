using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

namespace Player{
public class PlayerInput : MonoBehaviour, IPlayerInput
{
    //キー入力に対してOnButtonObservableを通してイベントを発行する
    //購読はOnButtonObservable.subscribe(なにかしらメソッド)で行える
    //引数はint[]で中身はそれぞれのキーに対する入力フレーム数
    private Subject<int[]> buttonSubject = new Subject<int[]>();


    public IObservable<int[]> OnButtonObservable{
        set {this.buttonSubject = (Subject<int[]>)value;}
        get {return buttonSubject;}
    }


    int[] Button_PressedFrame = new int[3];
    // Start is called before the first frame update
    void Start()
    {
        Array.Clear(Button_PressedFrame,0,Button_PressedFrame.Length);//初期化
    }

    // Update is called once per frame
    void Update()
    {
        //押されているキーのフレーム数を引数にしてイベントを発行
        if(Input.GetKey (KeyCode.Z))Button_PressedFrame[0]+=1;
        else Button_PressedFrame[0]=0;
        if(Input.GetKey (KeyCode.X))Button_PressedFrame[1]+=1;
        else Button_PressedFrame[1]=0;
        if(Input.GetKey (KeyCode.C))Button_PressedFrame[2]+=1;
        else Button_PressedFrame[2]=0;

        buttonSubject.OnNext(Button_PressedFrame);
    }
}
}