using System.Collections.Generic;
using System.Linq;
using DefaultNamespace.Structures;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{

    public class GameController : MonoBehaviour
    {
        #region CONSTANT

        private const float SpaceColumnStructure = 0.5f;
        private const float SpaceRowStructure = 0.5f;
        private static readonly int[,] StructureAge1 = { { 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1 }, {  0, -1, 0, -1, 0, -1, 0, -1, 0, -1, 0 }, { 0, 0, 1, 0, 1, 0, 1, 0, 1, 0, 0 },{  0, 0, 0, -1, 0, -1, 0, -1, 0, 0, 0 },{ 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0 } };
        public Sprite Age1Back;
        public Sprite Age2Back;
        public Sprite Age3Back;
        public Sprite GuildBack;
        public Sprite Wonderback;
        
        #endregion
        
        private static GameController _instance;

        private PlayerProfile _player1;
        private PlayerProfile _player2;
        private List<StructureProfile> _age1;
        private StructureProfile[] _age2;
        private StructureProfile[] _age3;
        [SerializeField] private GameObject _holder;

        #region Public Variant

        public string pathToStructureProfileAge1;
        public string pathToStructureProfileAge2;
        public string pathToStructureProfileAge3;
        public StructureScript prefabCard;

        #endregion
        
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

        public void SetupGame()
        {
            _age2 = Resources.LoadAll<StructureProfile>(pathToStructureProfileAge2);
            _age3 = Resources.LoadAll<StructureProfile>(pathToStructureProfileAge3);

            LoadAge1();
        }

        private void LoadAge1()
        {
            _age1 = Resources.LoadAll<StructureProfile>(pathToStructureProfileAge1).ToList();
            Debug.Log(_age1.Count);
            LoadStructure(_age1,StructureAge1);
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
                        list.RemoveAt(random);
                    } else if (structure[i,j] == -1)
                    {
                        int random = Random.Range(0, list.Count);
                        StructureScript card = Instantiate(prefabCard, _holder.transform);
                        card.transform.localPosition = new Vector3(j * SpaceColumnStructure, i * SpaceRowStructure, 0);
                        card.SetProfile(list[random]);
                        card.SetStatus(StatusCard.Facedown,-i);   
                        list.RemoveAt(random);
                    }
                }
            }
        }
    }
}