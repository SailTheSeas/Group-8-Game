using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public Vector2 gridSize = new Vector2(9, 5);  
    public float cellSize = 1.0f;  
    public Vector2 gridOrigin = new Vector2(-4f, -2f);

    //public int sun = 10;

    private HashSet<Vector2Int> occupiedCells = new HashSet<Vector2Int>();

    public Vector3 SnapToGrid(Vector3 originalPosition)
    {
        Vector2 gridPosition = new Vector2(
            Mathf.Round((originalPosition.x - gridOrigin.x) / cellSize),
            Mathf.Round((originalPosition.y - gridOrigin.y) / cellSize)
        );

        gridPosition.x = Mathf.Clamp(gridPosition.x, 0, gridSize.x - 1);
        gridPosition.y = Mathf.Clamp(gridPosition.y, 0, gridSize.y - 1);

        Vector3 snappedPosition = new Vector3(
            gridPosition.x * cellSize + gridOrigin.x,
            gridPosition.y * cellSize + gridOrigin.y,
            0f
        );

        return snappedPosition;
    }

    public Vector2Int WorldToGrid(Vector3 position)
    {
        Vector2 gridPosition = new Vector2(
            Mathf.Round((position.x - gridOrigin.x) / cellSize),
            Mathf.Round((position.y - gridOrigin.y) / cellSize)
        );

        return new Vector2Int(
            Mathf.RoundToInt(gridPosition.x),
            Mathf.RoundToInt(gridPosition.y)
        );
    }

    public bool IsCellOccupied(Vector2Int gridPosition)
    {
        return occupiedCells.Contains(gridPosition);
    }

    public void SetCellOccupied(Vector2Int gridPosition, bool occupied)
    {
        if (occupied)
        {
            occupiedCells.Add(gridPosition);
        }
        else
        {
            occupiedCells.Remove(gridPosition);
        }
    }
}

