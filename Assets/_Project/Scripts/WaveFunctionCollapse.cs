using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveFunctionCollapse : MonoBehaviour
{
    public int gridX = 50;
    public int gridY = 50;
    [SerializeField] private float delay = 0.005f;
    public GameObject[,] grid;
    [Space(10)]
    public GameObject cell;
    [SerializeField]
    private List<GameObject> neighbour;
    private List<GameObject> sortedGrid = new List<GameObject>();

    void Start() {
        int x = UnityEngine.Random.Range(0, gridX);
        int y = UnityEngine.Random.Range(0, gridY);
        InitializeGrid(0.0f);
        PickFirstCell(x,y);
        CollapseCells(x,y);
    }

    private void InitializeGrid(float delay) {
        grid = new GameObject[gridX, gridY];
        for (int x = 0; x < gridX; x++) {
            for (int y = 0; y < gridY; y++) {
                grid[x, y] = Instantiate(cell);
                grid[x, y].GetComponent<CellInfo>().x = x;
                grid[x, y].GetComponent<CellInfo>().y = y;
                grid[x, y].name = "[" + x + "," + y + "]";
                grid[x, y].transform.localPosition = new Vector3(x * 4, 0, y * 4);
            }
        }
    }

    private void PickFirstCell(int x, int y) {
        Debug.Log("First Cell : [" + x + "," + y + "]");
        grid[x, y].GetComponent<CellInfo>().UpdateCell();
    }

    private void CollapseCells(int x, int y) { 
        UpdateNeighboursTileOptions(x, y);
        while (neighbour.Count > 0) {
            neighbour.Sort((c1, c2) => c1.GetComponent<CellInfo>().tileOptions.Length.CompareTo(c2.GetComponent<CellInfo>().tileOptions.Length));
            neighbour[0].GetComponent<CellInfo>().UpdateCell();
            int x1 = neighbour[0].GetComponent<CellInfo>().x;
            int y1 = neighbour[0].GetComponent<CellInfo>().y;
            neighbour.Clear();
            UpdateNeighboursTileOptions(x1, y1);
        }
        FindNewCell();
    }

    private IEnumerator CollapseCellsWithDelay(int x, int y) { //with delay
        UpdateNeighboursTileOptions(x, y);
        while (neighbour.Count > 0) {
            neighbour.Sort((c1, c2) => c1.GetComponent<CellInfo>().tileOptions.Length.CompareTo(c2.GetComponent<CellInfo>().tileOptions.Length));
            neighbour[0].GetComponent<CellInfo>().UpdateCell();
            int x1 = neighbour[0].GetComponent<CellInfo>().x;
            int y1 = neighbour[0].GetComponent<CellInfo>().y;
            neighbour.Clear();
            UpdateNeighboursTileOptions(x1, y1);
            yield return new WaitForSeconds(delay);
        }
        FindNewCell();
    }

    private void FindNewCell() {
        sortedGrid.Clear();
        sortedGrid = ConvertArrayToList(grid);
        if(sortedGrid.Count > 0) {
            int x = sortedGrid[0].GetComponent<CellInfo>().x;
            int y = sortedGrid[0].GetComponent<CellInfo>().y;
            grid[x, y].GetComponent<CellInfo>().UpdateCell();
            CollapseCells(x, y);
        }
        else {
            Debug.Log("Generation Completed");
        }
    }

    private void UpdateNeighboursTileOptions(int x, int y) {
        int up = y - 1;
        int right = x + 1;
        int down = y + 1;
        int left = x - 1;

        if (up >= 0 && up <= (gridY - 1)) {
            if (grid[x, up].TryGetComponent<CellInfo>(out CellInfo cellUp)) {
                if (!cellUp.isCollapse) {
                    cellUp.tileOptions = RemoveNotAllowedTiles(grid[x, y].GetComponent<TileInfo>().upNeighbours, grid[x, up].GetComponent<CellInfo>().tileOptions);
                    neighbour.Add(cellUp.gameObject);
                }
            }
        }

        if (right >= 0 && right <= (gridX - 1)) {
            if (grid[right, y].TryGetComponent<CellInfo>(out CellInfo cellRight)) {
                if (!cellRight.isCollapse) {
                    cellRight.tileOptions = RemoveNotAllowedTiles(grid[x, y].GetComponent<TileInfo>().rightNeighbours, grid[right, y].GetComponent<CellInfo>().tileOptions);
                    neighbour.Add(cellRight.gameObject);
                }
            }
        }

        if (down >= 0 && down <= (gridY - 1)) {
            if (grid[x, down].TryGetComponent<CellInfo>(out CellInfo cellDown)) {
                if (!cellDown.isCollapse) {
                    cellDown.tileOptions = RemoveNotAllowedTiles(grid[x, y].GetComponent<TileInfo>().downNeighbours, grid[x, down].GetComponent<CellInfo>().tileOptions);
                    neighbour.Add(cellDown.gameObject);
                }
            }
        }

        if (left >= 0 && left <= (gridX - 1)) {
            if (grid[left, y].TryGetComponent<CellInfo>(out CellInfo cellLeft)) {
                if (!cellLeft.isCollapse) {
                    cellLeft.tileOptions = RemoveNotAllowedTiles(grid[x, y].GetComponent<TileInfo>().leftNeighbours, grid[left, y].GetComponent<CellInfo>().tileOptions);
                    neighbour.Add(cellLeft.gameObject);
                }
            }
        }
    }

    private List<GameObject> ConvertArrayToList(GameObject[,] array) {
        List<GameObject> list = new List<GameObject>();

        int rowCount = array.GetLength(0);
        int colCount = array.GetLength(1);

        for (int row = 0; row < rowCount; row++) {
            for (int col = 0; col < colCount; col++) {
                GameObject currentObject = array[row, col];
                if (!currentObject.GetComponent<CellInfo>().isCollapse) {
                    list.Add(currentObject);
                }
            }
        }

        list.Sort((c1, c2) => c1.GetComponent<CellInfo>().tileOptions.Length.CompareTo(c2.GetComponent<CellInfo>().tileOptions.Length));

        return list;
    }

    private GameObject[] RemoveNotAllowedTiles(GameObject[] main, GameObject[] neighbourCell) {
        List<GameObject> lmain = new List<GameObject>(main);
        List<GameObject> lneighbourCell = new List<GameObject>(neighbourCell);

        for (int i = lneighbourCell.Count - 1; i >= 0; i--) {
            GameObject neighbourGameObject = lneighbourCell[i];

            if (!lmain.Contains(neighbourGameObject)) {
                lneighbourCell.RemoveAt(i);
            }
        }

        return lneighbourCell.ToArray();
    }
}
