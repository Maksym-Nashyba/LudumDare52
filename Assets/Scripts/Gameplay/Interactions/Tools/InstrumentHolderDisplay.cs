using Misc;
using UnityEngine;

namespace Gameplay.Interactions.Tools
{
    public class InstrumentHolderDisplay : MenuDisplay
    {
        [SerializeField] private InstrumentHolder _instrumentHolder;
        private InstrumentItem _activeInstrumentItem;

        private void Awake()
        {
            _instrumentHolder.ToolChanged += OnToolChanged;
        }

        private void Start()
        {
            BuildLayoutGroupTable();
        }
        
        private void OnDestroy()
        {
            _instrumentHolder.ToolChanged -= OnToolChanged;
        }

        protected override void BuildLayoutGroupTable()
        {
            base.BuildLayoutGroupTable();
            foreach (Tool tool in _instrumentHolder.Tools)
            {
                DisplayItem();
            }        
        }

        private void OnToolChanged(Tool tool)
        {
            
        }
        
        private void DisplayItem()
        {
            GameObject instrumentItemGameObject = Instantiate(MenuItemPrefab, LayoutGroupTable.transform);
            instrumentItemGameObject.GetComponent<InstrumentItem>().Display();
            MenuItems.Add(instrumentItemGameObject);
        }
    }
}