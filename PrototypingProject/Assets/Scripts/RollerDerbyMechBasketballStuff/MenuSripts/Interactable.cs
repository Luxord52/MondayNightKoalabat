using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public ItemInventory myInventory;

    [System.Serializable]
    public class Element
    {
        public Color color;
        public Sprite icon;
        public string title;
    }

    public List<Element> options = new List<Element>();

    public virtual void OpenMenu()// on command to open menu
    {
        // tell canvas to spawn a menu.
        RadialMenuSpawner.ins.SpawnMenu(this);
        
    }
    public virtual void SelectItem(int val)
    {
    }
    public virtual void DropItem(int val)
    {
    }

    public virtual GameObject GetSelection(int index)
    {
        return myInventory.inventoryList[index].gameObject;//options[index];
    }
}
