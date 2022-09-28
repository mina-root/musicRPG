using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skills{

[CreateAssetMenu(fileName = "New ActiveSkill", menuName = "BattleSkills/PartyCharactor/ActiveSkill")]
    public class PartyCharactorSkills : ScriptableObject
    {
        public enum SkillType{
        attack,heal,support
        }
        
        public string skillName="new skill";//スキル名
        public SkillType skillType=SkillType.attack;
        public int SPCost = 0;//消費SP
        public int rate = 100;//効果倍率（％）
        public int fixedValue = 0;//固定値
        //コンボ補正蓄積値
        public int ComboAccumlation_Just = 1;
        public int ComboAccumlation_Good = 1;
        public int ComboAccumlation_Miss = 1;
        }



}