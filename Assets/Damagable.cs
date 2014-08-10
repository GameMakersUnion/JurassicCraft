using UnityEngine;
using System.Collections;

public class Damagable : MonoBehaviour{

    public Team team;
    public int health;
    private Color defaultColor;
    private const int flashTime = 10;
    private int flashTimer;



    public virtual void Start()
    {
        flashTimer = 0;

        defaultColor = transform.GetChild(0).renderer.material.color;


    }

    public virtual void Update()
    {
        if (flashTimer > 0)
        {
            flashTimer--;
        }
        else
        {
            transform.GetChild(0).renderer.material.color = defaultColor;
        }
    }


    public virtual void Damage()
    {

        if (health - 10 >= 0)
        {
            health -= 10;
            flashRed();
        }
        else
        {
            Destroy(gameObject);
        }
    }



    public virtual void flashRed()
    {
        transform.GetChild(0).renderer.material.color = Color.red;
        flashTimer = flashTime;

    }

}
