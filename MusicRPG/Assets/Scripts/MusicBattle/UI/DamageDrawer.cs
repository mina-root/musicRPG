using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
namespace UIs{


public class DamageDrawer : MonoBehaviour
{
    //ダメージポップアップを生成するクラス
    //現時点では座標などが固定、位置などは後ほど考え直す必要あり
    [SerializeField]GameObject DamagePopPrefab;
    [SerializeField]float DisplayTime=1.2f;

    public void DamagePop(int target,int value,bool isDamage){
        GameObject pop = GameObject.Instantiate(DamagePopPrefab,new Vector3(0,0,0),Quaternion.identity);
        pop.transform.SetParent(this.transform);
        Vector2 pos =  new Vector2();

        
        switch(target){
            case -1:
                pos = new Vector2(980,900);
                break;
            case 0:
                pos = new Vector2(Random.Range(1300f,1350f),Random.Range(100f,130f));
                break;
            case 1:
                pos = new Vector2(Random.Range(1525f,1575f),Random.Range(100f,130f));
                break;
            case 2:
                pos = new Vector2(Random.Range(1750f,1800f),Random.Range(100f,130f));
                break;
        }
        RectTransform rect = pop.GetComponent<RectTransform>();
        rect.position=pos;
        DamagePop popcomponent = pop.GetComponent<DamagePop>();
        popcomponent.value= value;
        popcomponent.isDamage = isDamage;
        rect.DOMove(pos+new Vector2(0,50),DisplayTime);
        StartCoroutine(WaitAndDestroy(pop));
    }

    IEnumerator WaitAndDestroy(GameObject obj)
    {
        yield return new WaitForSeconds(DisplayTime);
        Destroy(obj);
    }
}
}

