using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialMenuSpawner : MonoBehaviour
{

    public static RadialMenuSpawner ins;
    public RadialMenu menuPrefab;


    void Awake()
    {
        ins = this;
    }

    public void SpawnMenu(Interactable obj)
    {
        if (obj.options.Count >0)
        {
            RadialMenu newMenu = Instantiate(menuPrefab) as RadialMenu;
            newMenu.transform.SetParent(transform, false);// drops in the middle of the screen
                                                          //newMenu.transform.position = Input.mousePosition; // drops the menu on the mouse position
            newMenu.SpawnButtons(obj);
        }
    }
}
