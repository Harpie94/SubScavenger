using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInventory : MonoBehaviour
{
    public List<itemType> inventoryList;
    public int selectedItem;

    PlayerInput playerInput;
    InputAction takeAction;
    InputAction dropAction;
    InputAction selectAction;


    [SerializeField] GameObject cube_item;
    [SerializeField] GameObject sphere_item;

    private Dictionary<itemType,GameObject> itemSetActive = new Dictionary<itemType, GameObject>() { };

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        //selectAction = playerInput.actions["selection"];
        takeAction = playerInput.actions["takeitem"];
        dropAction = playerInput.actions["dropitem"];
    }

    void Start()
    {  
        itemSetActive.Add(itemType.Cube, cube_item);
        itemSetActive.Add(itemType.Sphere, sphere_item);

        NewItemSelected();
    }

    // Update is called once per frame
    void Update()
    {
        var itemSelection = selectAction.ReadValue<float>();
        Debug.Log(selectAction);
        if (itemSelection == -1f /*&& selectedItem >= 0*/)
        {
            selectedItem--;
            NewItemSelected();
        }
        else if (itemSelection == 1f /*&& selectedItem <= 0*/)
        { 
            selectedItem++;
            NewItemSelected();
        }

    }

    private void NewItemSelected()
    {
        cube_item.SetActive(false);
        sphere_item.SetActive(false);

        GameObject selectedGameObject = itemSetActive[inventoryList[selectedItem]];
        selectedGameObject.SetActive(true);
    }

}
