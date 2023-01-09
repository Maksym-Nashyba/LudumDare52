using System;
using Gameplay.Interactions.GameplayMenus.UpgradeShop;
using UnityEditor.Rendering.Universal;
using UnityEngine;

namespace Gameplay.Interactions.Tools
{
    public class InstrumentHolder : MonoBehaviour
    {
        public event Action<Tool> ToolChanged;
        
        public Tool[] Tools => _tools;
        [SerializeField] private Tool[] _tools;
        [SerializeField] private UpgradeStation _upgrades;
        private Tool _activeTool;

        private void Start()
        {
            PickTool(0);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                PickTool(0);
            }
            else if (Input.GetKeyDown(KeyCode. Alpha2))
            {
                PickTool(1);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                PickTool(2);
            }else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                if(!_upgrades.PickaxeUpgrade.IsCrafted)return;
                PickTool(3);
            }
        }

        private void PickTool(int number)
        {
            if(_activeTool != null) _activeTool.gameObject.SetActive(false);
            _activeTool = _tools[number];
            _activeTool.gameObject.SetActive(true);
            ToolChanged?.Invoke(_activeTool);
        }
    }
}