using System;
using DefaultNamespace.Structures;
using DefaultNamespace.Values;
using UnityEngine;
using Object = UnityEngine.Object;

namespace DefaultNamespace
{
    [Serializable]
    public class Good
    {
        [SerializeField] private ResourceType _type;
        [SerializeField] private int _quantity;
        [SerializeField] private TokenProfile _link;
        //0 - for symbol of conflict and will be default

        public Good(ResourceType type, int quantity)
        {
            _type = type;
            _quantity = quantity;
        }

        public Good(ResourceType type)
        {
            _type = type;
            _quantity = 1;
        }

        public ResourceType GetGoodType()
        {
            return _type;
        }

        public int GetQuantity()
        {
            return _quantity;
        }

        public override string ToString()
        {
            if (GetGoodType() != ResourceType.Link 
                //&& GetGoodType() != ResourceType.Symbol)
                )
            {
                return GetGoodType().ToString();
            }
            else
            {
                return GetGoodType().ToString();
            }
        }

        public override bool Equals(object obj)
        {
            return this.GetGoodType() == ((Good)obj).GetGoodType();
        }
    }
}