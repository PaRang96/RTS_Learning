using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class UnitSelectionManager : MonoBehaviour
{
    public static UnitSelectionManager Instance { get; set; }

    public List<GameObject> allUnitsList = new List<GameObject>();
    public List<GameObject> unitsSelected = new List<GameObject>();

    public LayerMask clickable;
    public LayerMask ground;
    public GameObject groundMarker;
    
    private Camera _cam;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        _cam = Camera.main;
    }
    
    private void Update()
    {
        // left click
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);

            // if hitting a clickable object
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, clickable))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    MultipleSelect(hit.collider.gameObject);
                }
                else
                {
                    SelectUnit_Click(hit.collider.gameObject);
                }
            }
            // if NOT hitting a clickable object
            else
            {
                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    DeselectAll();
                }
            }
        }
    }

    private void MultipleSelect(GameObject targetUnit)
    {
        if (unitsSelected.Contains(targetUnit) == false)
        {
            unitsSelected.Add(targetUnit);
            EnableUnitMovement(targetUnit, true);
        }
        else
        {
            EnableUnitMovement(targetUnit, false);
            unitsSelected.Remove(targetUnit);
        }
    }

    private void DeselectAll()
    {
        foreach (var unit in allUnitsList)
        {
            EnableUnitMovement(unit, false);
        }
        
        unitsSelected.Clear();
    }

    private void SelectUnit_Click(GameObject targetUnit)
    {
        DeselectAll();
        
        unitsSelected.Add(targetUnit);

        EnableUnitMovement(targetUnit, true);
    }

    private void EnableUnitMovement(GameObject targetUnit, bool trigger)
    {
        targetUnit.GetComponent<UnitMovement>().enabled = trigger;
    }
}
