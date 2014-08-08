using UnityEngine;
using System.Collections;

public class Selection : MonoBehaviour
{

    public GameObject Box;
    private GameObject BoxCopy;
    public GameObject allUnits;
    private bool Selecting;
    private Vector3 BoxPosition;
    float x1;   //start click
    float z1;
    float x2;   //current/end click
    float z2;
    public Team selectableTeam;

    public Transform plane;


    // Update is called once per frame
    void Update()
    {
        float x = GetWorldPositionAtHeight(Input.mousePosition, plane.position.y).x;
        float z = GetWorldPositionAtHeight(Input.mousePosition, plane.position.y).z;



        if (Input.GetMouseButtonDown(0))
        {

            
            x1 = x;
            z1 = z;
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
            x2 = x;
            z2 = z;

            float width = x2 - x1;
            float height = z2 - z1;
            BoxPosition = new Vector3(width / 2 + x1, 0, height / 2 + z1);

            BoxCopy.transform.localScale = new Vector3(width, 10, height);
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


    }

    //GetWorldPos for Perspective camera when looking for above
    public static Vector3 GetWorldPositionAtHeight(Vector3 screenPosition, float y)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        Plane xy = new Plane(Vector3.down, new Vector3(0, y, 0));
        //Plane xy = new Plane(new Vector3(4, 0, 4), new Vector3(-4,0,4), new Vector3(-4,0,-4));
        float distance;
        xy.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }
}


