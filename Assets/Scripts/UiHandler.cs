using System;
using DefaultNamespace.Structures;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class UiHandler: MonoBehaviour
    {
        public static UiHandler Instance;

        public UnityAction<string> ErrorAppearEvent;
        public UnityAction InforCanvasAppearEvent;
        
        private StructureProfile _currentStructure;
        private StructureScript _currentScript;
        
        [SerializeField] private Image _image;
        [SerializeField] private Button _buildButton;
        [SerializeField] private Button _discardButton;
        [SerializeField] private HorizontalLayoutGroup _requirements;
        [SerializeField] private HorizontalLayoutGroup _prizes;
        [SerializeField] private Image _prefabGood;
        
        private void Awake()
        {
            Instance = this;
        }

        private void OnDisable()
        {
            _buildButton.onClick.RemoveAllListeners();
            _discardButton.onClick.RemoveAllListeners();
        }

        private void OnEnable()
        {
            _buildButton.onClick.AddListener(() =>
            {
                GameController.Instance.BuyStructure(_currentStructure);
                _currentScript.Disappear();
            });
            _discardButton.onClick.AddListener(() =>
            {
                GameController.Instance.DiscardStructure(_currentStructure);
                _currentScript.Disappear();
            });
        }

        public void UpdateInforPanel(StructureProfile profile, StructureScript script)
        {
            _image.sprite = profile._sprite;
            _currentStructure = profile;
            _currentScript = script;
            int count = 0;
            foreach (var requirement in profile._cost)
            {
                //If child count displayed is less than number of requirement then instantiate new child 
                if (_requirements.transform.childCount <= count)
                {
                    var image = Instantiate(_prefabGood, _requirements.transform);
                    image.sprite = AssetManager.Instance.SearchResource(requirement)._sprite;
                    image.GetComponentInChildren<TextMeshProUGUI>().text = requirement.GetQuantity() > 1 ? requirement.GetQuantity().ToString() : "";
                }
                else
                {
                    var image = _requirements.transform.GetChild(count).GetComponent<Image>();
                    image.gameObject.SetActive(true);
                    image.sprite = AssetManager.Instance.SearchResource(requirement)._sprite;
                    image.GetComponentInChildren<TextMeshProUGUI>().text = requirement.GetQuantity() > 1 ? requirement.GetQuantity().ToString() : "";
                }

                count++;
                Debug.Log("Displayed cost");

            }

            if (count < _requirements.transform.childCount)
            {
                for (int i = count; i < _requirements.transform.childCount; i++)
                {
                    _requirements.transform.GetChild(count).gameObject.SetActive(false);
                }
            }
            count = 0;
            foreach (var prize in profile._prize)
            {
                if (_prizes.transform.childCount <= count)
                {
                    var image = Instantiate(_prefabGood, _prizes.transform);
                    image.sprite = AssetManager.Instance.SearchResource(prize)._sprite;
                    image.GetComponentInChildren<TextMeshProUGUI>().text = 
                        prize.GetQuantity() > 1 ? prize.GetQuantity().ToString() : "";
                }
                else
                {
                    var image = _prizes.transform.GetChild(count).GetComponent<Image>();
                    image.gameObject.SetActive(true);
                    image.sprite = AssetManager.Instance.SearchResource(prize)._sprite;
                    image.GetComponentInChildren<TextMeshProUGUI>().text = prize.GetQuantity() > 1 ? prize.GetQuantity().ToString() : "";
                }
                count++;
                Debug.Log("Displayed prize");
            }
            if (count < _prizes.transform.childCount)
            {
                for (int i = count; i < _prizes.transform.childCount; i++)
                {
                    _prizes.transform.GetChild(count).gameObject.SetActive(false);
                }
            }
        }
        
    }
}