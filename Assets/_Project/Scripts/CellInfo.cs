using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellInfo : MonoBehaviour
{
    public GameObject[] tileOptions;

    public void PickRandomTileOption() {
        Instantiate(tileOptions[UnityEngine.Random.Range(0, tileOptions.Length)]);
    }

    public GameObject PickRandomTileOption2() {
        return tileOptions[UnityEngine.Random.Range(0, tileOptions.Length)];
    }
}
