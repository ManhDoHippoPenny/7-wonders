using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.Structures
{
    [CreateAssetMenu(fileName = "Structure")]
    public class StructureProfile : ScriptableObject
    {
        public string _name;
        public int _age;  // 4-represent Guild card; 5- for wonder
        public StructureType _type;
        public Sprite _sprite;
        public List<Good> _prize;
        public List<Good> _cost;
        
        private void OnValidate()
        {
            _name = name;
        }
    }
}