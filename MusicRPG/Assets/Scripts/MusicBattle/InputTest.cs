using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;


public class InputTest : MonoBehaviour
{
    [SerializeField] Player.PlayerInput playerInput;
    [SerializeField] SoundPlayer sp;
    
    // Start is called before the first frame update
    void Start()
    {
        //playerInput.OnButtonObservable.Subscribe((int[] frameCount) => 
        //{if(frameCount[0]>0)Debug.Log( sp.getPlaytime());} );

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}