using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Misc
{
    public abstract class MenuDisplay : MonoBehaviour
    {
        [SerializeField] protected LayoutGroup LayoutGroupTable;
        [SerializeField] protected GameObject MenuItemPrefab;
        protected List<GameObject> MenuItems;
        
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