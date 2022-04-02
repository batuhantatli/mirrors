using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlaceObjectOnGrid : MonoSingleton<PlaceObjectOnGrid>
{
    public Transform gridCellPrefab;
    public Transform cube;
    public Transform prisma;
    public Transform onMousePrefabe;
    public Transform onMousePrefabePrisma;
    public Vector3 smoothMousePosition;

    public GameObject mirrorText;


    [SerializeField] public List<GameObject> grids = new List<GameObject>();
    [SerializeField] public List <GameObject> unPlacable = new List<GameObject>();
    public int mirrorsCount;
    [SerializeField] public List<GameObject> mirrors = new List<GameObject>();

    [SerializeField] private int height;
    [SerializeField] int width;

    Vector3 mousePosition;
    private Node[,] nodes;
    private Plane plane;

    void Start()
    {
        CreateGrid();
        plane = new Plane(Vector3.up , transform.position);
        mirrorText.GetComponent<TMPro.TextMeshProUGUI>().text = ""+mirrorsCount ;
    }

    // Update is called once per frame
    void Update()
    {
        GetMousePositionOnGrid(); 
    }
    void GetMousePositionOnGrid()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(plane.Raycast(ray,out var enter))
        {
            mousePosition = ray.GetPoint(enter);
            smoothMousePosition = mousePosition;
            mousePosition.y = 0;
            mousePosition = Vector3Int.RoundToInt(mousePosition);
            foreach(var node in nodes)
            {
                
                if(node.cellPosition == mousePosition && node.isPlaceable)
                {
                    /* for (int i = 0; i < unPlacable.Count; i++)
                    {
                        if(node.cellPosition == new Vector3(unPlacable[i].transform.position.x ,0 , unPlacable[i].transform.position.z) )
                        {
                            node.isPlaceable = false;
                        }
                    } */
                    if(Input.GetMouseButtonUp(0) && onMousePrefabe != null)
                    {

                        node.isPlaceable = false;
                        onMousePrefabe.transform.GetChild(0).GetComponent<BoxCollider>().enabled = true;
                        onMousePrefabe.GetComponent<ObjFollowMouse>().isOnGrid = true;
                        onMousePrefabe.position = node.cellPosition + new Vector3(0f,.8f,0f);
                        onMousePrefabe = null;
                    }
                }
            }
        }
    }

    public void MirrorCreator()
    {
        if(onMousePrefabe == null && mirrorsCount -1 >= mirrors.Count)
        {
            onMousePrefabe = Instantiate(cube,mousePosition,Quaternion.identity);
            onMousePrefabe.transform.GetChild(0).GetComponent<BoxCollider>().enabled = false;
            mirrors.Add(onMousePrefabe.gameObject);
            mirrorText.GetComponent<TMPro.TextMeshProUGUI>().text = ""+ (mirrorsCount-mirrors.Count) ;

            
        }
        else if (mirrorsCount - mirrors.Count == 0)
        {
            Debug.Log("game Over");
        }
    }

    private void CreateGrid()
    {
        nodes = new Node[width,height];
        var name = 0;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0;j < height; j++)
            {
                Vector3 worldPosition = new Vector3(i,0,j);
                Transform obj = Instantiate(gridCellPrefab,worldPosition,gridCellPrefab.transform.rotation);
                obj.transform.parent = transform;
                obj.name = "Cell " + name;
                nodes[i,j] = new Node(true,worldPosition,obj);
                for (int k = 0; k < unPlacable.Count; k++)
                    {
                        if(worldPosition == new Vector3(unPlacable[k].transform.position.x ,0 , unPlacable[k].transform.position.z) )
                        {
                            nodes[i,j].isPlaceable = false;
                        }
                    }
                name++;
                grids.Add(obj.gameObject);
            }
        }
    }


}




public class Node
{
    public bool isPlaceable;
    public Vector3 cellPosition; 
    public Transform obj;

    public Node(bool isPlacable, Vector3 cellPosition,Transform obj)
    {
        this.isPlaceable = isPlacable;
        this.cellPosition = cellPosition;
        this.obj = obj;
    }

}
