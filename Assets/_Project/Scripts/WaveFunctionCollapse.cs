using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveFunctionCollapse : MonoBehaviour
{
    public int gridX = 50;
    public int gridY = 50;
    public GameObject[,] grid;
    [Space(10)]
    public GameObject cell;
    public GameObject[] tilePrefabs;

    void Start() {
        InitializeGrid();
        PickRandomCell();
    }

    private void InitializeGrid() {
        grid = new GameObject[gridX, gridY];

        for (int x = 0; x < gridX; x++) {
            for (int y = 0; y < gridY; y++) {
                grid[x, y] = cell.GetComponent<CellInfo>().PickRandomTileOption2();
                grid[x, y].transform.localPosition = new Vector3(x * 4, 0 , y * 4);
                Instantiate(grid[x, y]);
            }
        }
    }

    private void PickRandomCell() {
        //Instantiate(grid[UnityEngine.Random.Range(0, 49), UnityEngine.Random.Range(0, 49)]);
        //grid[UnityEngine.Random.Range(0, 49), UnityEngine.Random.Range(0, 49)].GetComponent<CellInfo>().PickRandomTileOption();
    }
}
