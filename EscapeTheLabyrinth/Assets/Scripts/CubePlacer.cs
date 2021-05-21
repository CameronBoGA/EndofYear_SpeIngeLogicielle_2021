using UnityEngine;

public class CubePlacer : MonoBehaviour
{
    private Grid grid;

    private void Awake()
    {
        grid = FindObjectOfType<Grid>();
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
        GameObject newobj;
    
	    if (itemOption == ItemList.Wall)
	    {
            newobj = Instantiate(wall, finalPosition, Quaternion.identity);
            newobj.layer = 9;
            newobj.tag = "Wall";
            EditorObject eo = newobj.AddComponent<EditorObject>();
            eo.data.pos = newobj.transform.position;
            eo.data.objectType = EditorObject.ObjectType.Wall;
        }
        
        if (itemOption == ItemList.Player)
	    {
            if (ms.playerPlaced == false)
	        {
                newobj = Instantiate(player, finalPosition, Quaternion.identity);
                newobj.layer = 9;
                newobj.tag = "Player";
                ms.playerPlaced = true;
                EditorObject eo = newobj.AddComponent<EditorObject>();
                eo.data.pos = newobj.transform.position;
                eo.data.objectType = EditorObject.ObjectType.Player;
            }
        }
    }
}