using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialMenu :MonoBehaviour 
{
    public Interactable myInteractable;

    public RadialButton buttonPrefab;
    RadialButton selected;
    RadialButton prevSelected;
    int SelectionCounter = 0;
    RadialButton[] buttonsList;

    public Color defaultColor;
    public Vector3 defaultScale;
    // Start is called before the first frame update
    public void SpawnButtons(Interactable obj)
    {
        myInteractable = obj;
        buttonsList = new RadialButton[obj.options.Count];
        for (int i = 0; i < obj.options.Count; i++)
        {
            RadialButton newButton = Instantiate(buttonPrefab) as RadialButton;
            newButton.transform.SetParent(transform, false);
            float theta = (2 * Mathf.PI / obj.options.Count) * i;
            float xPos = Mathf.Sin(theta);
            float yPos = Mathf.Cos(theta);
            newButton.transform.localPosition = new Vector3(xPos, yPos, 0f)*75;
            newButton.background.color = obj.options[i].color;
            newButton.icon.sprite = obj.options[i].icon;
            newButton.title = obj.options[i].title;
            newButton.myMenu = this;
            buttonsList[i] = newButton;
            //newButton.transform.localPosition = new Vector3(0f, 100f, 0f);
        }

    }

    void ButtonSelection()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            if (SelectionCounter >= buttonsList.Length-1)
            {
                SelectionCounter = 0;
            }
            else
            {
                SelectionCounter += 1;
            }
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            if (SelectionCounter <= 0)
            {
                SelectionCounter = buttonsList.Length-1;
            }
            else
            {
                SelectionCounter -= 1;
            }
        }
            selected = buttonsList[SelectionCounter];
            if (prevSelected == null)
            {
                prevSelected = buttonsList[buttonsList.Length - 1];
                defaultColor = prevSelected.background.color;
                defaultScale = prevSelected.transform.localScale;
            }
            if (selected != prevSelected)
            {
                prevSelected.background.color = defaultColor;
                prevSelected.transform.localScale = defaultScale;
                defaultColor = selected.background.color;
                defaultScale = selected.transform.localScale;
                selected.background.color = Color.white;
                selected.transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
                prevSelected = selected;
            }
    }
    void Update()
    {
        ButtonSelection();
        if (Input.GetButtonDown("Fire1"))
        {

            myInteractable.SelectItem(SelectionCounter); 

            {
                /*
                // check if a weapon 
                if (myInteractable.GetSelection(SelectionCounter).GetComponent<WeaponData>() != null || myInteractable.GetSelection(SelectionCounter).GetComponent<GrapplingHook>() != null)
                {
                    myInteractable.SelectItem(SelectionCounter);
                    //set weapon visible 
                    myInteractable.ItemVisible(SelectionCounter);
                    //set other weapons invisible 

                    //Debug.Log("selected : " +  myInteractable);
                }
                else
                {
                    // if the object isn't a weapon use it from the menu, whatever USE means
                    Debug.Log("selected : " + myInteractable.GetSelection(SelectionCounter).ToString());
                }
                */
            }
        }
        else if (Input.GetButtonDown("Fire2"))
        {
            myInteractable.DropItem(SelectionCounter);
        }
        // pass out selection value without needing access to the script which needs it. 
        // on mouse click activate action ex. draw weapon
        if (Input.GetKeyUp("f"))
        {  
            // Send and integer to the interact object which this menu was created for and call the selection
            Destroy(gameObject);
        }
    }
}
