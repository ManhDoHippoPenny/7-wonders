using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.Values
{
    /// <summary>
    /// enum determine the timing to trigger the effects of the tokens
    /// </summary>
    public enum TimeToTrigger
    {
        Immediately,
        EndGame,
        Future,
    }
    
    [CreateAssetMenu(fileName = "TokenProfile")]
    public class TokenProfile : ScriptableObject
    {
        public List<Effect> _effects;
        public string _name;
        public Sprite _sprite;

        private void OnValidate()
        {
            _name = name;
        }

        [Serializable]
        public class Effect
        {
            public TimeToTrigger _TimeToTrigger;
        }
    }
}