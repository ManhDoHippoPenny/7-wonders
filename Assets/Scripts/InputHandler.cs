using System;
using DefaultNamespace.Structures;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DefaultNamespace
{
    public class InputHandler : MonoBehaviour
    {
        private Camera _mainCamera;
        private StructureScript _currentStructureScript;

        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        public void OnClick(InputAction.CallbackContext context)
        {
            if (!context.started) return;

            var rayHit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
            if(_currentStructureScript != null) _currentStructureScript.MissClickEvent?.Invoke();
            if (!rayHit.collider || !rayHit.collider.gameObject.GetComponent<StructureScript>() || rayHit.collider.gameObject.GetComponent<SpriteRenderer>().sortingOrder != 0) return;
            _currentStructureScript = rayHit.collider.gameObject.GetComponent<StructureScript>();
            _currentStructureScript.OnClickEvent.Invoke();
        }
    }
}