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
    public enum ItemList {Wall, Caltrop, Player, Mud, End};
    [HideInInspector]
    public ItemList itemOption = ItemList.Wall;

    // Item That can be placed -> Prefabs

    // Managing Avaibility for object placement
    private Vector3 mousePos;
    private bool colliding;
    [HideInInspector]
    //public MeshRenderer mr;
    
    //public Materials to check avaibility -> Prefabs
    public Material goodPlace;
    public Material badPlace;

    // Reference to the Manager Script for overall information
    public Manager prog;

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
                        prog.playerPlaced = false;
                    Destroy(hitInfo.collider.gameObject);
                }
                else if (colliding == true && manipulateOption == LevelManipulation.Destroy)
                {
                    if (hitInfo.collider.gameObject.name.Contains("PlayerModel"))
                        prog.playerPlaced = false;
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
        Vector3 dummy = new Vector3(0.0f, 0, 0);
        GameObject newobj;
        GameObject temp;
    
	    if (itemOption == ItemList.Wall)
	    {
            newobj = Instantiate(prog.wall, finalPosition, Quaternion.identity);
            newobj.layer = 9;
            newobj.tag = "Wall";
            EditorObject eo = newobj.AddComponent<EditorObject>();
            eo.data.pos = newobj.transform.position;
            eo.data.objectType = EditorObject.ObjectType.Wall;
        }
        else if (itemOption == ItemList.Player)
	    {
            if (prog.playerPlaced == false)
	        {
                prog.StartPT.transform.position = finalPosition;
                Debug.Log("In Placer, Start: "+finalPosition);
                //prog.StartPT.transform.position = finalPosition;
                temp = Instantiate(prog.StartPT, finalPosition, Quaternion.identity);
                newobj = Instantiate(prog.player, dummy, Quaternion.identity);
                prog.playerPlaced = true;
                EditorObject eo = newobj.AddComponent<EditorObject>();
                eo.data.pos = newobj.transform.position;
                eo.data.objectType = EditorObject.ObjectType.Player;
                Destroy(newobj);
            }
        }
        else if (itemOption == ItemList.End)
	    {
            Debug.Log("In Placer, End: "+finalPosition);
            prog.EndPT.transform.position = finalPosition;
            //prog.EndPT.transform.position = finalPosition;
            newobj = Instantiate(prog.EndPT, finalPosition, Quaternion.identity);
            //newobj = Instantiate(prog.EndPT, dummy, Quaternion.identity);
            EditorObject eo = newobj.AddComponent<EditorObject>();
            eo.data.pos = newobj.transform.position;
            eo.data.objectType = EditorObject.ObjectType.End;
            //destroy(newobj);
        }
        else if (itemOption == ItemList.Mud)
	    {
            newobj = Instantiate(prog.mud, finalPosition, Quaternion.identity);
            newobj.layer = 9;
            EditorObject eo = newobj.AddComponent<EditorObject>();
            eo.data.pos = newobj.transform.position;
            eo.data.objectType = EditorObject.ObjectType.Mud;
        }
        else if (itemOption == ItemList.Caltrop)
	    {
            newobj = Instantiate(prog.caltrop, finalPosition, Quaternion.identity);
            newobj.layer = 9;
            EditorObject eo = newobj.AddComponent<EditorObject>();
            eo.data.pos = newobj.transform.position;
            eo.data.objectType = EditorObject.ObjectType.Caltrop;
        }
    }
}