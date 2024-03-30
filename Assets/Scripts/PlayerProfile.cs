using System.Collections.Generic;
using DefaultNamespace.Structures;

namespace DefaultNamespace
{
    public class PlayerProfile
    {
        private List<StructureProfile> _builtBuildings = new List<StructureProfile>();
        private int _money;
        private Dictionary<string,int> _resources = new Dictionary<string, int>();

        public void BuildBuilding(StructureProfile profile)
        {
            _builtBuildings.Add(profile);
            foreach (var good in profile._prize)
            {
                ObtainGood(good);
            }
        }

        public void ObtainGood(Good good)
        {
            if (!_resources.ContainsKey(good.ToString())) _resources[good.ToString()] = 0;
            _resources[good.ToString()] += good.GetQuantity();
        }

        public bool SpendGood(Good good)
        {
            if (!_resources.ContainsKey(good.ToString())) return false;
            if (_resources[good.ToString()] < good.GetQuantity()) return false;
            _resources[good.ToString()] -= good.GetQuantity();
            return true;
        }
    }
}