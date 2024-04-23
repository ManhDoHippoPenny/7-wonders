using System;
using System.Collections.Generic;
using DefaultNamespace.Structures;
using Sirenix.OdinInspector;

namespace DefaultNamespace
{
    [Serializable]
    public struct PlayerProfile
    {
        private List<StructureProfile> _builtBuildings;
        private Dictionary<ResourceType, int> _resources;

        public Dictionary<ResourceType, int> Resources
        {
            get => _resources;
            set => _resources = value;
        }

        public PlayerProfile(int gold)
        {
            _resources = new Dictionary<ResourceType, int> { { ResourceType.Gold, gold } };
            _builtBuildings = new List<StructureProfile>();
        }

        public void BuildStruct(StructureProfile profile)
        {
            _builtBuildings.Add(profile);
        }
        
        public bool UpdateResource(ResourceType resourceType, int quantity)
        {
            if (!_resources.ContainsKey(resourceType))
            {
                if (quantity >= 0) _resources.Add(resourceType, quantity);
                else return false;
            }
            else
            {
                if (_resources[resourceType] + quantity >= 0) _resources[resourceType] += quantity;
                else return false;
            }
            return true;
        }

        public bool CheckResource(ResourceType resourceType, int quantity)
        {
            if (!_resources.ContainsKey(resourceType)) return false;
            if (_resources[resourceType] < quantity) return false;
            return true;
        }
    }
}