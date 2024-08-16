using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantPlacer : MonoBehaviour
{
    [SerializeField] private PlantStats plantStats;
    public GameObject plantPrefab; 
    private GameObject placePlant;
    private int plantType;
    private bool isPlacing = false;
    private Button currentPlantPressed;

    private GridManager gridManager;
    private SunController sunController;
    public int cost; 
    void Start()
    {
        gridManager = this.GetComponent<GridManager>();
        sunController = this.GetComponent<SunController>();
    }

    public void SetPlantPrefab(GameObject newPlantPrefab)
    {
        
        
        Destroy(placePlant);
        plantPrefab = newPlantPrefab;                
        //placePlant = newPlantPrefab;
        placePlant = Instantiate(plantPrefab);
        isPlacing = true;
    }

    public void SetPlantButton(Button buttonPressed)
    {
        currentPlantPressed = buttonPressed;
    }

    public void SetPlantIndex(int newPlantType)
    {
        plantType = newPlantType;
    }

    void Update()
    {
        if (isPlacing)
        {

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f; 

            Vector3 snappedPosition = gridManager.SnapToGrid(mousePosition);
            
            placePlant.transform.position = snappedPosition;

            Vector2Int gridPosition = gridManager.WorldToGrid(snappedPosition);

            if (Input.GetMouseButtonDown(0) && sunController.GetSun() >= plantStats.plantCost[plantType])
            {
                if (!gridManager.IsCellOccupied(gridPosition))
                {
                    Debug.Log("Placed");
                    gridManager.SetCellOccupied(gridPosition, true);
                    currentPlantPressed.interactable = false;
                    //placePlant = null;
                    placePlant.GetComponent<PlantBehaviours>().SetRow(gridPosition.y);
                    placePlant.GetComponent<PlantBehaviours>().EnablePlant();
                    isPlacing = false;
                    sunController.SpendSun(plantStats.plantCost[plantType]);
                    StartCoroutine(PlantCooldown(plantStats.rechargeTime[plantType], currentPlantPressed));
                    currentPlantPressed = null;
                    placePlant = null;
                }
                else
                {
                    Destroy(placePlant);
                    placePlant = null;
                    currentPlantPressed = null;
                    isPlacing = false;
                }

            }
            else if (Input.GetMouseButtonDown(1))
            {
                Destroy(placePlant);
                placePlant = null;
                isPlacing = false;
            }

        }
    }

    IEnumerator PlantCooldown(float coolDown, Button plantOnCooldown)
    {
        yield return new WaitForSeconds(coolDown);
        plantOnCooldown.interactable = true;
    }
    /*public void PlacePlantButtonClicked()
    {
        if (!isPlacing && cost <= gridManager.sun)
        {
            placePlant = Instantiate(plantPrefab);
            isPlacing = true;
        }
    }*/
}
    
