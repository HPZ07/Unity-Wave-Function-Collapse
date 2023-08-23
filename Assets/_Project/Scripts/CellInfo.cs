using UnityEngine;

public class CellInfo : MonoBehaviour
{
    public int x, y;
    public bool isCollapse;
    public GameObject[] tileOptions;

    public GameObject PickRandomTileOption2() {
        return tileOptions[UnityEngine.Random.Range(0, tileOptions.Length)];
    }

    public GameObject UpdateCell() {
        GameObject temp = PickRandomTileOption2();
        TileInfo tileInfo = temp.GetComponent<TileInfo>();
        this.name = this.name + " " + temp.name;
        this.gameObject.AddComponent<TileInfo>().isCollapsed = true;
        this.GetComponent<TileInfo>().id = tileInfo.id;
        this.GetComponent<TileInfo>().upNeighbours = tileInfo.upNeighbours;
        this.GetComponent<TileInfo>().rightNeighbours = tileInfo.rightNeighbours;
        this.GetComponent<TileInfo>().downNeighbours = tileInfo.downNeighbours;
        this.GetComponent<TileInfo>().leftNeighbours = tileInfo.leftNeighbours;

        MeshFilter sourceMeshFilter = temp.GetComponent<MeshFilter>();
        MeshRenderer sourceMeshRenderer = temp.GetComponent<MeshRenderer>();
        this.gameObject.AddComponent<MeshFilter>().mesh = sourceMeshFilter.sharedMesh;
        this.gameObject.AddComponent<MeshRenderer>().materials = sourceMeshRenderer.sharedMaterials;

        this.isCollapse = true;
        return this.gameObject;

    }

}
