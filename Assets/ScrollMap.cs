using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScrollMap : MonoBehaviour
{

    public GameObject edgeScrollerIcon;
    public GUISkin emptySkin;
    public Terrain terrain;


    bool highlightScrollRegions = true;
    float controlHeight;
    Vector2 screenAvail;
    private GameObject edgeScrollerIconActive;
    bool active = false;
    List<Quaternion> quat;
    bool GUIinit = false;
    const float cursorDepth = 10f;
    const float moveMagnitude = 1f;
    bool initGUIdone = false;
    GUISkin defaultSkin;


    enum Sections { TopLeft, Top, TopRight, Right, BottomRight, Bottom, BottomLeft, Left };
    Vector2 thickness;
    List<Rect> rectSect;
    List<Vector3> dirWorld;


    //control
    Rect rectControl;


    // Use this for initialization
    void Start()
    {
        float fractionRegion = 0.1f;
        controlHeight = Utils.PercentToPixel(0.2f, Screen.height);
        screenAvail = new Vector2(Screen.width, Screen.height - controlHeight); //
        thickness = new Vector2(Utils.PercentToPixel(fractionRegion, screenAvail.y), Utils.PercentToPixel(fractionRegion, screenAvail.y));

        //determine screen sizes
        rectSect = new List<Rect>();
        rectSect.Add(new Rect(0, 0, thickness.x, thickness.y));
        rectSect.Add(new Rect(thickness.x, 0, screenAvail.x - thickness.x * 2, thickness.y));
        rectSect.Add(new Rect(screenAvail.x - thickness.x, 0, thickness.x, thickness.y));
        rectSect.Add(new Rect(screenAvail.x - thickness.x, thickness.y, thickness.x, screenAvail.y - thickness.y * 2));
        rectSect.Add(new Rect(screenAvail.x - thickness.x, screenAvail.y - thickness.y, thickness.x, thickness.y));
        rectSect.Add(new Rect(thickness.x, screenAvail.y - thickness.y, screenAvail.x - thickness.x * 2, thickness.y));
        rectSect.Add(new Rect(0, screenAvail.y - thickness.y, thickness.x, thickness.y));
        rectSect.Add(new Rect(0, thickness.y, thickness.x, screenAvail.y - thickness.y * 2));



        quat = new List<Quaternion>();
        Dictionary<Sections, Quaternion> cursorRotations = new Dictionary<Sections, Quaternion>();
        int s = System.Enum.GetNames(typeof(Sections)).Length;
        for (int i = 0; i < s; i++ )
        {
            quat.Add( Quaternion.Euler( 0, (i*45)-45, 0 ) );
        }

        dirWorld = new List<Vector3>();
        dirWorld.Add(new Vector3(-1, 0, 1));
        dirWorld.Add(new Vector3(0, 0, 1));
        dirWorld.Add(new Vector3(1, 0, 1));
        dirWorld.Add(new Vector3(1, 0, 0));
        dirWorld.Add(new Vector3(1, 0, -1));
        dirWorld.Add(new Vector3(0, 0, -1));
        dirWorld.Add(new Vector3(-1, 0, -1));
        dirWorld.Add(new Vector3(-1, 0, 0));

        

        //control
        rectControl = new Rect(0, screenAvail.y, screenAvail.x, Screen.height - screenAvail.y);


    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {
        //one time
        if (!initGUIdone)
        {
            initGUI();
            initGUIdone = true;
        }
        

        if (highlightScrollRegions)
        {
            if (emptySkin != null) 
            {
                GUI.skin = emptySkin;    //this empty skin kinda invalidates the preceding condition 
            }

            if (terrain != null)
            {
                Vector3 v = Selection.GetWorldPositionAtHeight(Camera.main.transform.position, terrain.SampleHeight(Camera.main.transform.position));
                //Debug.Log(v);

            }
            


            bool inAnyScrollRegions = false;
            for (int i = 0; i < rectSect.Count; i++)
            {
                
                GUI.Box(rectSect[i], "");
                if (rectSect[i].Contains(Event.current.mousePosition))  //mouseover
                {
                    inAnyScrollRegions = true;

                    //calculations
                    Vector2 mousePos = Input.mousePosition;
                    Vector2 fractionWithin = new Vector2((mousePos.x - rectSect[i].position.x) / rectSect[i].size.x, (Screen.height - mousePos.y - rectSect[i].position.y) / rectSect[i].size.y);

                    // reverse direction in half the cases
                    if (i == 0 || i == 1 || i == 2) { fractionWithin.y = 1 - fractionWithin.y; }
                    if (i == 0 || i == 6 || i == 7) { fractionWithin.x = 1 - fractionWithin.x; }

                    Vector3 movementAmount =  Utils.vMult(dirWorld[i], new Vector3(fractionWithin.x, 0, fractionWithin.y)) * moveMagnitude;
                    Camera.main.transform.position += movementAmount;
                    
                    float y = Camera.main.ScreenPointToRay(mousePos).origin.y - cursorDepth;
                    Vector3 putCursorHere = Selection.GetPlaneXZAtHeight(mousePos, y);






                    if (active) 
                    {
                        edgeScrollerIconActive.transform.position = putCursorHere;
                        edgeScrollerIconActive.transform.rotation = quat[i];
                        
                    }

                    if (!active)
                    {
                        edgeScrollerIconActive = (GameObject)Instantiate(edgeScrollerIcon, putCursorHere, quat[i]);
                        active = true;
                    }

                }


            }

            if (!inAnyScrollRegions)
            {
                //?? why isn't coding like this?? edgeScrollerIconActive = Destroy(edgeScrollerIconActive);
                Destroy(edgeScrollerIconActive);
                active = false;
            }

        }

        //control
        GUI.skin = defaultSkin;
        GUI.Box(rectControl, ""); //new Rect(100,100,100,100)
    }

    void initGUI()
    {
        defaultSkin = GUI.skin;
    }


}
