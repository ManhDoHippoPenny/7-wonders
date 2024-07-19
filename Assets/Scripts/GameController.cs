using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace.Structures;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{

    public class GameController : MonoBehaviour
    {
        #region CONSTANT

        private const float SpaceColumnStructure = 0.65f;
        private const float SpaceRowStructure = 0.85f;
        private static readonly int[,] StructureAge1 = { { 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1 }, {  0, -1, 0, -1, 0, -1, 0, -1, 0, -1, 0 }, { 0, 0, 1, 0, 1, 0, 1, 0, 1, 0, 0 },{  0, 0, 0, -1, 0, -1, 0, -1, 0, 0, 0 },{ 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0 } };
        public Sprite Age1Back;
        public Sprite Age2Back;
        public Sprite Age3Back;
        public Sprite GuildBack;
        public Sprite Wonderback;
        
        #endregion
        
        private static GameController _instance;

        [SerializeField] private StructureScript prefabCard;
        private PlayerProfile _player1;
        private PlayerProfile _player2;
        private bool _turnFirstPlayer = true;
        private List<StructureScript> _cards = new List<StructureScript>();
        private List<StructureProfile> _age1;
        private StructureProfile[] _age2;
        private StructureProfile[] _age3;
        [SerializeField] private GameObject _holder;
        [SerializeField] private float _offsetX;
        [SerializeField] private float _offsetY;

        #region Public Variant

        public string pathToStructureProfileAge1;
        public string pathToStructureProfileAge2;
        public string pathToStructureProfileAge3;
        public AssetReference PrefabCardReference;

        #endregion
        
        #region Unity Methods
        public static GameController Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<GameController>();

                    if (_instance == null)
                    {
                        _instance = new GameObject("GameController").AddComponent<GameController>();
                    }
                }

                return _instance;
            }
        }
        public void Awake()
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

        public void Start()
        {
            SetupGame();
        }
        #endregion
        public void SetupGame()
        {
            if (!PrefabCardReference.RuntimeKeyIsValid())
            {
                return;
            }
            AsyncOperationHandle<GameObject> handler = PrefabCardReference.LoadAssetAsync<GameObject>();
            _age2 = Resources.LoadAll<StructureProfile>(pathToStructureProfileAge2);
            _age3 = Resources.LoadAll<StructureProfile>(pathToStructureProfileAge3);
            handler.Completed += handle =>
            {
                prefabCard = handle.Result.GetComponent<StructureScript>();
            };
            _player1 = new PlayerProfile();
            _player2 = new PlayerProfile();
            
            LoadAge1();
            
        }

        private void LoadAge1()
        {
            var result = Addressables.LoadAssetsAsync<StructureProfile>(pathToStructureProfileAge1, (profile =>
            {
                Debug.Log(profile._name);
            }));
            result.Completed += handle =>
            {
                Debug.Log(handle.Status);
                LoadStructure(handle.Result.ToList(), StructureAge1);
            };
            //_age1 = Resources.LoadAll<StructureProfile>(pathToStructureProfileAge1).ToList();
            //Debug.Log(_age1.Count);
            //LoadStructure(_age1,StructureAge1);
        }

        private void LoadStructure(List<StructureProfile> list, int[,] structure)
        {
            for (int i = 0; i < structure.GetLength(0); i++)
            {
                for (int j = 0; j < structure.GetLength(1); j++)
                {
                    if (structure[i, j] == 1)
                    {
                        int random = Random.Range(0, list.Count);
                        StructureScript card = Instantiate(prefabCard, _holder.transform);
                        card.transform.localPosition = new Vector3(j * SpaceColumnStructure, i * SpaceRowStructure, 0);
                        card.SetProfile(list[random]);
                        card.SetStatus(StatusCard.Faceup, -i);
                        _cards.Add(card);
                        list.RemoveAt(random);
                    } else if (structure[i,j] == -1)
                    {
                        int random = Random.Range(0, list.Count);
                        StructureScript card = Instantiate(prefabCard, _holder.transform);
                        card.transform.localPosition = new Vector3(j * SpaceColumnStructure, i * SpaceRowStructure, 0);
                        card.SetProfile(list[random]);
                        card.SetStatus(StatusCard.Facedown,-i);   
                        _cards.Add(card);
                        list.RemoveAt(random);
                    }
                }
            }
        }

        public void RemoveStructureList()
        {
            Collider2D[] colliders = new Collider2D[24];
            foreach (var card in _cards)
            {
                int n = Physics2D.OverlapAreaNonAlloc(card.GetCollider().bounds.min,
                    card.GetCollider().bounds.max, colliders);
                bool flag = true;
                for (int i=0; i<n;i++ )
                {
                    var collider = colliders[i];
                    if (collider.gameObject.GetComponent<StructureScript>().Order > card.Order) flag = false;
                }
                if(flag) card.SetStatus(StatusCard.Faceup,0);
            }
        }

        public ResourceType CheckSpendGood(StructureProfile profile)
        {
            ResourceType result = ResourceType.None;
            foreach (var resource in profile._cost)
            {
                if ((_turnFirstPlayer && _player1.CheckResource(resource.GetGoodType(), resource.GetQuantity())) ||
                    (!_turnFirstPlayer && _player2.CheckResource(resource.GetGoodType(), resource.GetQuantity())))
                    continue;
                result = resource.GetGoodType();
                break;
            }
            if(result != ResourceType.None) Debug.Log($"{(_turnFirstPlayer? "First player": "Second player")} need more {result.ToString()}");
            return result;
        }

        public void BuyStructure(StructureProfile profile)
        {
            var type = Instance.CheckSpendGood(profile);
            if (type != ResourceType.None)
            {
                UiHandler.Instance.ErrorAppearEvent?.Invoke(type.ToString());
                return;
            }
            if (_turnFirstPlayer) _player1.BuyStructure(profile);
            else _player2.BuyStructure(profile);
            EndTurn();
        }

        public void DiscardStructure(StructureProfile profile)
        {
            if(_turnFirstPlayer) _player1.DiscardStructure(profile);
            else _player2.DiscardStructure(profile);
            EndTurn();
        }

        public void EndTurn()
        {
            _turnFirstPlayer = !_turnFirstPlayer;
        }
        
        #region Addressable

        [SerializeField] private string m_Address;
        private ResourceRequest m_hatLoadingRequest;
        private AsyncOperationHandle<GameObject> m_hatLoadOpHandle;


        #endregion
    }
}