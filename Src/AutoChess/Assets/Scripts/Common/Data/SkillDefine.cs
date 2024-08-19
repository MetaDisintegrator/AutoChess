using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Data
{
    public enum SkillCondition
    {
        None = 0,
        MPCD = 1,
    }
    public enum SkillFunction
    {
        None = 0,
        AddBuff = 1,
        AddBuffLv = 2,
    }

    public class SkillDefine
    {
        public int ID { get; set; }
        public int Lv { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<int> DescriptionParams { get; set; }
        public SkillCondition Condition { get; set; }
        public List<int> CD {  get; set; }
        public string Anim {  get; set; }
        public string Sound { get; set; }
        public SkillFunction Func { get; set; }
        public int Param {  get; set; }
        public List<int> Params { get; set; }

        public override string ToString()
        {
            return string.Format("Skill({0}({1}):{2},{3})", ID, Lv, Name, Description);
        }
    }
}

