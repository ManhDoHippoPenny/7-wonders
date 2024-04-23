using System;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace.Structures
{
    public enum StatusCard
    {
        Faceup,
        Facedown,
        Taken
    }
    
    public enum ResourceType
    {
        Clay = 0,
        Stone = 1,
        Wood = 2,
        Gold = 3,
        Link = 4,
        Papyrus = 7,
        Glass= 6,
        Conflict = 5,
        VictoryPoint = 8,
        None = -1
        
         
    }
    
    public class StructureScript : MonoBehaviour
    {
        [SerializeField] private StructureProfile _profile;
        [SerializeField] private StatusCard _status;
        [SerializeField] private SpriteRenderer _renderer;
        
        private BoxCollider2D _collider2D;
        private Collider2D[] results;
        private Vector3 extent;
        
        public UnityAction OnClickEvent;
        
        #region Properties

        public StatusCard Status
        {
            get
            { 
                return _status;
            }
            private set
            {
                _status = value;
            }
        }

        public int Order
        {
            get
            {
                return _renderer.sortingOrder;
            }
            private set
            {
                _renderer.sortingOrder = value;
            }
        }

        #endregion

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _collider2D = GetComponent<BoxCollider2D>();
        }

        private void OnEnable()
        {
            OnClickEvent += TryToPurchaseEvent;
        }

        private void OnDisable()
        {
            OnClickEvent -= TryToPurchaseEvent;
        }

        public void SetProfile(StructureProfile profile)
        {
            _profile = profile;
            name = profile._name;
        }

        public void SetStatus(StatusCard status, int renderOrder)
        {
            _status = status;
            if (_status == StatusCard.Facedown)
            {
                switch (_profile._age)
                {
                    case 1:
                    {
                        _renderer.sprite = GameController.Instance.Age1Back;
                        break;
                    }
                    case 2:
                    {
                        _renderer.sprite = GameController.Instance.Age2Back;
                        break;
                    }
                    case 3:
                    {
                        _renderer.sprite = GameController.Instance.Age3Back;
                        break;
                    }
                    case 4:
                    {
                        _renderer.sprite = GameController.Instance.GuildBack;
                        break;
                    }
                    default:
                    {
                        _renderer.sprite = GameController.Instance.Wonderback;
                        break;
                    }
                }
            }
            else _renderer.sprite = _profile._sprite;
            _renderer.sortingOrder = renderOrder;
            extent = _collider2D.bounds.extents;
        }

        public void TryToPurchaseEvent()
        {
            var type = GameController.Instance.CheckSpendGood(_profile);
            if (type != ResourceType.None)
            {
                Debug.Log(type);
                return;
            }
            GameController.Instance.BuyStructure(_profile);
            GameController.Instance.ReceiveGood(_profile);
            if (_renderer.sortingOrder != 0) return;
            gameObject.SetActive(false);
            GameController.Instance.RemoveStructureList();
        }

        public BoxCollider2D GetCollider()
        {
            return _collider2D;
        }
        
        private (Vector2, Vector2) GetAreaFromBoxCollider(BoxCollider2D boxCollider2D)
        {
            return (boxCollider2D.bounds.min, boxCollider2D.bounds.max);
        }
    }
}