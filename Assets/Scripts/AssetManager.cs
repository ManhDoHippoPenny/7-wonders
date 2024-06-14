using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace.Structures;
using DefaultNamespace.Values;
using UnityEngine;

namespace DefaultNamespace
{
    public class AssetManager : MonoBehaviour
    {
        #region Const Asset
        [SerializeField] private ResourceTypeDictionary _Resources;
        [SerializeField] private LinkDictionary _Links;
        
        #endregion
        private static AssetManager _instance;
        
        public static AssetManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<AssetManager>();

                    if (_instance == null)
                    {
                        _instance = new GameObject("AssetManager").AddComponent<AssetManager>();
                    }
                }

                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
        }

        public TokenProfile SearchResource(int intValue)
        {
            ResourceType type = intValue is ResourceType ? (ResourceType)intValue : ResourceType.Clay;
            return _Resources[type];
        }

        public TokenProfile SearchResource(Good good)
        {
            if (good.GetGoodType() == ResourceType.Link)
            {
                return good.GetLink();
            }
            return _Resources[good.GetGoodType()];
        }
    }

    [Serializable]
    public class ResourceTypeDictionary : UnitySerializedDictionary<ResourceType, TokenProfile>
    {
        
    }

    [Serializable]
    public class LinkDictionary : UnitySerializedDictionary<TokenProfile, StructureProfile>
    {
        
    }
    
    public abstract class UnitySerializedDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [SerializeField, HideInInspector]
        private List<TKey> keyData = new List<TKey>();
	
        [SerializeField, HideInInspector]
        private List<TValue> valueData = new List<TValue>();

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            this.Clear();
            for (int i = 0; i < this.keyData.Count && i < this.valueData.Count; i++)
            {
                this[this.keyData[i]] = this.valueData[i];
            }
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            this.keyData.Clear();
            this.valueData.Clear();

            foreach (var item in this)
            {
                this.keyData.Add(item.Key);
                this.valueData.Add(item.Value);
            }
        }
    }
}