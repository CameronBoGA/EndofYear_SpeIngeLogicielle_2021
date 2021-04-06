using UnityEngine;
using UnityEngine.EventSystems;

public class CubePlacer : MonoBehaviour
{
    private Grid grid;

    // List of Actions That can be taken by the user
    public enum LevelManipulation {Create, Destroy};
    [HideInInspector]
    public LevelManipulation manipulateOption = LevelManipulation.Create;

    // List of Items That can be placed
    public enum ItemList {Wall, Touret, Player, Swamp};
    [HideInInspector]
    public ItemList itemOption = ItemList.Wall;

    // Item That can be placed -> Prefabs
    public GameObject wall;
    public GameObject player;

    // Managing Avaibility for object placement
    private Vector3 mousePos;
    private bool colliding;
    [HideInInspector]
    //public MeshRenderer mr;
    
    //public Materials to check avaibility -> Prefabs
    public Material goodPlace;
    public Material badPlace;

    // Reference to the Manager Script for overall information
    public Manager ms;

    private void Awake()
    {
        grid = FindObjectOfType<Grid>();
    }

    void Start()
    {
        //mr = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        // Get the info of what the mouse in pointing to
        RaycastHit hitInfo;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        colliding = false;
        // Enable the object selected to follow the mouse cursor
        /*mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);*/
        
        // Check if the mouse is pointing to something
        if (Physics.Raycast(ray, out hitInfo))
        {
            if (hitInfo.collider.gameObject.layer == 9)
            {
                colliding = true;
                //mr.material = badPlace;
            }
            else
            {
                colliding = false;
                //mr.material = goodPlace;
            }
        }

        // Actions to be taken if the user left clicks in the game
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
	        {
                if (colliding == false && manipulateOption == LevelManipulation.Create)
                    CreateObject(hitInfo.point);
                else if (colliding == true && manipulateOption == LevelManipulation.Destroy)
                {
                    if (hitInfo.collider.gameObject.name.Contains("PlayerModel"))
                        ms.playerPlaced = false;
                    Destroy(hitInfo.collider.gameObject);
                }
                else if (colliding == true && manipulateOption == LevelManipulation.Destroy)
                {
                    if (hitInfo.collider.gameObject.name.Contains("PlayerModel"))
                        ms.playerPlaced = false;
                    Destroy(hitInfo.collider.gameObject);
                }
            }
            /*if (Physics.Raycast(ray, out hitInfo))
            {
                PlaceCubeNear(hitInfo.point);
            }*/
        }
    }

    private void CreateObject(Vector3 clickPoint)
    {
        var finalPosition = grid.GetNearestPointOnGrid(clickPoint);
    
	    if (itemOption == ItemList.Wall)
	    {
            Instantiate(wall, finalPosition, Quaternion.identity);
            wall.layer = 9;
            wall.tag = "Wall";
        }
        if (itemOption == ItemList.Player)
	    {
            if (ms.playerPlaced == false)
	        {
                Instantiate(player, finalPosition, Quaternion.identity);
                player.layer = 9;
                player.tag = "Player";
                ms.playerPlaced = true;
                // Adding Components to player
                /*player.AddComponent<CapsuleCollider>();
                player.GetComponent<CapsuleCollider>().center = new Vector3(0, 1, 0);
                player.GetComponent<CapsuleCollider>().height = 2;*/
            }
        }
    }

    private void PlaceCubeNear(Vector3 clickPoint)
    {
        var finalPosition = grid.GetNearestPointOnGrid(clickPoint);
        //GameObject.CreatePrimitive(PrimitiveType.Cube).transform.position = finalPosition;
        Instantiate(wall, finalPosition, Quaternion.identity);
    }
}