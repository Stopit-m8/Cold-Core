using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class col : MonoBehaviour
{
    public Tank_Patrol Tank;
    // Start is called before the first frame update
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("works");
    }
}
