using System;
using DefaultNamespace.Structures;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class UiHandler: MonoBehaviour
    {
        public static UiHandler Instance;

        [SerializeField] private Button _buttonPrefab;
        
        private void Awake()
        {
            Instance = this;
        }

        public void SpawnButton(Vector3 position, UnityAction<StructureProfile> callback, string name)
        {
            
        }
        
    }
}