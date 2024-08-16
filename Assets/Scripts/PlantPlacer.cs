using UnityEngine;

public class PlantPlacer : MonoBehaviour
{
    public GameObject plantPrefab; 
    private GameObject placePlant;
    private bool isPlacing = false;

    private GridManager gridManager;

    public int cost; 
    void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
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
                    gridManager.SetCellOccupied(gridPosition, true);
                    placePlant = null;
                    isPlacing = false;
                    gridManager.sun = gridManager.sun - cost; ; 

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
    public void PlacePlantButtonClicked()
    {
        if (!isPlacing && cost <= gridManager.sun)
        {
            placePlant = Instantiate(plantPrefab);
            isPlacing = true;
        }
    }
}
    
