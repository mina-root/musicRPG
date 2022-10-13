using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace UIs{
public class DamagePop : MonoBehaviour
{
    [SerializeField]Color32 DamageColor;
    [SerializeField]Color32 HealColor;
    TextMeshProUGUI textMeshPro;
    public int value;
    public bool isDamage;
    // Start is called before the first frame update
    void Start()
    {
        textMeshPro= gameObject.GetComponent<TextMeshProUGUI>();
        textMeshPro.text = value.ToString();
        if(isDamage){
            textMeshPro.faceColor=Color.red;
        }else{
            textMeshPro.faceColor=Color.green;
        }
    }



}
}