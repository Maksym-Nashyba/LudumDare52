using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Misc
{
    public abstract class MenuDisplay : MonoBehaviour
    {
        [SerializeField] protected VerticalLayoutGroup LayoutGroupTable;
        [SerializeField] protected GameObject MenuItemPrefab;
        protected List<GameObject> MenuItems;

        public void ToggleInventoryShown()
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
        protected virtual void BuildLayoutGroupTable()
        {
            if (MenuItems != null) ClearLayoutGroup();
            MenuItems = new List<GameObject>();
        }
        protected virtual void ClearLayoutGroup()
        {
            foreach (GameObject item in MenuItems)
            {
                Destroy(item);
            }
        }
    }
}