using System;
using System.Collections.Generic;
using System.Text;

namespace StarterGame
{
    public interface ISkillContainer : ISkillLevel
    {
        string contents();
    }
    public class SkillContainer : ISkillContainer
    {

        private Dictionary<string, ISkillLevel> level;
        public string Name { get; set; }
        private float _danceSkill;
        public float DanceSkill
        {
            get
            {
                float tdanceSkill = _danceSkill;
                foreach (ISkillLevel level in level.Values)
                {
                    tdanceSkill += level.DanceSkill;
                }
                return tdanceSkill;

            }
            set
            {
                _danceSkill = value;
            }

        }

        private float _singingSkill;
        public float SingingSkill
        {
            get
            {
                float tsingingSkill = _singingSkill;
                foreach (ISkillLevel level in level.Values)
                {
                    tsingingSkill += level.SingingSkill;
                }
                return tsingingSkill;

            }
            set
            {
                _singingSkill = value;
            }

        }

        private float _modelingSkill;
        public float ModelingSkill
        {
            get
            {
                float tmodelingSkill = _modelingSkill;
                foreach (ISkillLevel level in level.Values)
                {
                    tmodelingSkill += level.ModelingSkill;
                }
                return tmodelingSkill;

            }
            set
            {
                _modelingSkill = value;
            }

        }

        private float _groupLevel;
        public float GroupLevel
        {
            get
            {
                float tgroupLevel = _groupLevel;
                foreach (ISkillLevel level in level.Values)
                {
                    tgroupLevel += level.GroupLevel;
                }
                return tgroupLevel;

            }
            set
            {
                _groupLevel = value;
            }

        }

        private float _energy;
        public float Energy
        {
            get
            {
                float tenergy = _energy;
                foreach (ISkillLevel level in level.Values)
                {
                    tenergy += level.Energy;
                }
                return tenergy;

            }
            set
            {
                _energy = value;
            }

        }

        public void add(ISkillLevel level,string skillName)
        {
          
           
        }

        public SkillContainer()
        {
            level = new Dictionary<string, ISkillLevel>();
        }

        private ISkillContainer _decorator;

        public void AddDecorator(ISkillLevel decorator)
        {
            
        }

        public string contents()
        {
            string itemNames = "Skills: \n";
            Dictionary<string, ISkillLevel>.KeyCollection keys = level.Keys;
            foreach (string itemName in keys)
            {
                itemNames += " " + level[itemName].ToString() + "\n";
            }

            return itemNames;

        }

    }
}
