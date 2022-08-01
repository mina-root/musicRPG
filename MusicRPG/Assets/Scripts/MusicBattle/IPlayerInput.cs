using UniRx;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace Player{
public interface IPlayerInput
    {
        IObservable<int[]> OnButtonObservable{
            get;set;
        }
    }
}