using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace.Structures;

namespace DefaultNamespace
{
    [Serializable]
    public class PlayerProfile
    {
        private List<StructureProfile> _builtBuildings;
        private Dictionary<ResourceType, int> _resources;
        public Dictionary<ResourceType, int> Resources
        {
            get => _resources;
            set => _resources = value;
        }
        
        public PlayerProfile()
        {
            _resources = new Dictionary<ResourceType, int> { { ResourceType.Gold, 7 } };
            _builtBuildings = new List<StructureProfile>();
        }
        
        private bool UpdateResource(ResourceType resourceType, int quantity)
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

        public void BuyStructure(StructureProfile structureProfile)
        {
            foreach (var good in structureProfile._cost)
            {
                if (good.GetGoodType() == ResourceType.Gold)
                {
                    UpdateResource(good.GetGoodType(), -good.GetQuantity());
                }
            }
            _builtBuildings.Add(structureProfile);
            foreach (var good in structureProfile._prize)
            {
                UpdateResource(good.GetGoodType(), good.GetQuantity());
            }
        }

        public void DiscardStructure(StructureProfile profile)
        {
            UpdateResource(ResourceType.Gold, 2 + _builtBuildings.Count(builder => builder._type == StructureType.MARKET));
        }
    }
}