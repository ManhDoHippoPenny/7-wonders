using System;
using UnityEngine;

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
    }
    
    public class StructureScript : MonoBehaviour
    {
        [SerializeField] private StructureProfile _profile;
        [SerializeField] private StatusCard _status;
        [SerializeField] private SpriteRenderer _renderer;

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

        #endregion

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
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
        }
    }
}