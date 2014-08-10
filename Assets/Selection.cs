using UnityEngine;
using System.Collections;

public class Selection : MonoBehaviour
{

    public GameObject Box;
    private GameObject BoxCopy;
    public int cursorHeight;
    private const int cursorHeightDefault = 10;
    public GameObject allUnits;
    public Terrain terrain;
    private bool Selecting;
    private Vector3 BoxPosition;
    float x1;   //start click
    float z1;
    float x2;   //current/end click
    float z2;
    public static Team selectableTeam;
    public GameObject goToRing;
    private GameObject goToRingGO;

    float timerGoToRing;
    float timerGoToRingLimit = 5f;

    public GameObject cursor;
    public GameObject cursorProject;

    private GameObject cursorGO;
    private GameObject cursorProjectGO;

    void Start()
    {
        if (cursorHeight == null)
        {
            cursorHeight = cursorHeightDefault;
        }

        cursorGO = (GameObject)Instantiate(cursor, Input.mousePosition, Quaternion.identity);
        cursorProjectGO = (GameObject)Instantiate(cursorProject, Input.mousePosition, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3? hitPoint = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray);
        foreach (var hit in hits)
        {
            if (hit.collider is TerrainCollider)
            {
                hitPoint = hit.point;
                cursorGO.transform.position = hit.point;
                break;
                //cursorProjectGO.transform.position = hit.point;
            }
        }

        Vector3 worldPos = GetWorldPositionAtHeight(Input.mousePosition, terrain.transform.position.y);
        //float tHeight = terrain.SampleHeight(worldPos) + 5f;
        //Vector3 worldPosAbove = new Vector3(worldPos.x, tHeight, worldPos.z);
        //cursorGO.transform.position = worldPosAbove;
        //cursorProjectGO.transform.position = worldPosAbove;


        if (Input.GetMouseButtonDown(0))
        {
            ///no
            x1 = worldPos.x;
            z1 = worldPos.z;
            Selecting = true;
            BoxCopy = (GameObject)Instantiate(Box);

            for (int i = 0; i < allUnits.transform.childCount; i++)
            {
                allUnits.transform.GetChild(i).GetComponent<Unit>().Selected = false;

            }
            //everything happens the one frame the mouse is pressed down
        }

        if (Selecting)
        {
            x2 = worldPos.x;
            z2 = worldPos.z;

            float width = x2 - x1;
            float height = z2 - z1;
            BoxPosition = new Vector3(width / 2 + x1, 0, height / 2 + z1);

            BoxCopy.transform.localScale = new Vector3(width, cursorHeight, height);
            BoxCopy.transform.position = new Vector3(BoxPosition.x, 0, BoxPosition.z);

        }

        if (BoxCopy && Input.GetMouseButtonUp(0))
        {
            Selecting = false;
            for (int i = 0; i < allUnits.transform.childCount; i++)
            {
                GameObject Child = allUnits.transform.GetChild(i).gameObject;
                if ((((Child.transform.position.x > x1 && Child.transform.position.x < x2) ||
                    (Child.transform.position.x < x1 && Child.transform.position.x > x2)) &&
                    ((Child.transform.position.z > z1 && Child.transform.position.z < z2) ||
                    (Child.transform.position.z < z1 && Child.transform.position.z > z2))) &&
                        (selectableTeam == Child.GetComponent<Unit>().team))
                {
                    Child.GetComponent<Unit>().Selected = true;
                }
            }
            Destroy(BoxCopy);

        }

        if (Input.GetMouseButtonDown(1))
        {
            if (goToRing != null && hitPoint != null)    //goToRingGO == null && 
            {
                //Vector3 worldPos = GetWorldPositionAtHeight(new Vector2(x, z), 0);
                
                //Vector3 vPos = GetWorldPositionAtHeight(Input.mousePosition, plane.position.y);

                goToRingGO = (GameObject)Instantiate(goToRing, hitPoint.Value, Quaternion.Euler(-90, 0, 0));
                timerGoToRing = Time.time;
            }
            else if (goToRing == null)
            {
                Debug.LogWarning("goToRing not attached to " + gameObject.name + ", please attach.");
            }
        }

        //if (timerGoToRing > timerGoToRingLimit)
        //{
        //    Debug.Log("YA");
        //}
        //
        //if (goToRingGO != null && timerGoToRing > timerGoToRingLimit)
        //{
        //    Destroy(goToRingGO);
        //    timerGoToRing = 0;
        //}


    }

    //GetWorldPos for Perspective camera when looking for above
    public static Vector3 GetWorldPositionAtHeight(Vector3 screenPosition, float yHeight)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        Debug.DrawRay(ray.origin, ray.direction, Color.red);
        Plane xy = new Plane(Vector3.up, new Vector3(0, yHeight, 0));   //creates a new mathematical plane at yth height to raycast into successfully.
        float distance;
        xy.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }

    public static Vector3 GetPlaneXZAtHeight(Vector3 screenPosition, float y)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        Plane xz = new Plane(Vector3.down, new Vector3(0, y, 0));
        //Plane xy = new Plane(new Vector3(4, 0, 4), new Vector3(-4,0,4), new Vector3(-4,0,-4));
        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100, Color.green);
        float distance;
        xz.Raycast(ray, out distance);
        Vector3 v = new Vector3(ray.GetPoint(distance).x, y, ray.GetPoint(distance).z);

        return v;
    }
}


