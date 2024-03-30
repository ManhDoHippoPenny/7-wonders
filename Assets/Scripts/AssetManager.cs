using System;
using DefaultNamespace.Values;
using UnityEngine;

namespace DefaultNamespace
{
    public class AssetManager : MonoBehaviour
    {
        #region Const Asset

        public  TokenProfile ClayToken;
        public  TokenProfile StoneToken;
        public  TokenProfile WoodToken;
        public  TokenProfile GlassToken;
        public  TokenProfile PapyrusToken;
        public  TokenProfile GoldToken1;
        public  TokenProfile GoldToken3;
        public  TokenProfile GoldToken6;
        public  TokenProfile ConflictToken;
        public  TokenProfile VPToken;
        
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
        
        
    }
}