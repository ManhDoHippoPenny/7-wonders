using System;
using System.Linq;
using DefaultNamespace.Structures;
using UnityEditor;
using UnityEngine;

namespace DefaultNamespace
{
    [CustomEditor(typeof(StructureProfile))]
    public class StructureProfileEditor : Editor
    {
        private StructureProfile _structureSO;

        private void OnEnable()
        {
            _structureSO = target as StructureProfile;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            switch (_structureSO._type)
            {
                case StructureType.CONFLICT:
                {
                    if(_structureSO._prize.FirstOrDefault(prize => prize.GetGoodType() == ResourceType.Conflict) == null)
                        _structureSO._prize.Add(new Good(ResourceType.Conflict));
                    break;
                }
            }
        }
        
    }
}