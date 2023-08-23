using UnityEngine;

public class TileInfo : MonoBehaviour
{
    public bool isCollapsed;
    public int id;
    [Space(10)]
    public GameObject[] upNeighbours;
    public GameObject[] rightNeighbours;
    public GameObject[] downNeighbours;
    public GameObject[] leftNeighbours;
}
 