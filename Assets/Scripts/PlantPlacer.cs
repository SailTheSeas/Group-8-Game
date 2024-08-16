using UnityEngine;

public class PlantPlacer : MonoBehaviour
{
    public GameObject plantPrefab; 
    private GameObject placePlant;
    private bool isPlacing = false;

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
        plantPrefab = newPlantPrefab;
        //placePlant = newPlantPrefab;
        placePlant = Instantiate(plantPrefab);
        isPlacing = true;
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

            if (Input.GetMouseButtonDown(0))
            {
                if (!gridManager.IsCellOccupied(gridPosition))
                {
                    Debug.Log("Placed");
                    gridManager.SetCellOccupied(gridPosition, true);
                    //placePlant = null;
                    placePlant.GetComponent<PlantBehaviours>().SetRow(gridPosition.y);
                    isPlacing = false;
                    sunController.SpendSun(cost);

                }
                else
                {
                    Destroy(placePlant);
                    placePlant = null;
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
    /*public void PlacePlantButtonClicked()
    {
        if (!isPlacing && cost <= gridManager.sun)
        {
            placePlant = Instantiate(plantPrefab);
            isPlacing = true;
        }
    }*/
}
    
