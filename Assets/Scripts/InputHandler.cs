using System;
using DefaultNamespace.Structures;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DefaultNamespace
{
    public class InputHandler : MonoBehaviour
    {
        private Camera _mainCamera;

        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        public void OnClick(InputAction.CallbackContext context)
        {
            if (!context.started) return;

            var rayHit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
            if (!rayHit.collider || !rayHit.collider.gameObject.GetComponent<StructureScript>()) return;
            
            rayHit.collider.gameObject.GetComponent<StructureScript>().OnClickEvent.Invoke();
        }
    }
}