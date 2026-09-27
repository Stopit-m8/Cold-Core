using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_explosion : MonoBehaviour
{
    // Start is called before the first frame update
    void FixedUpdate()
    {
        Destroy(gameObject, 0.5f);
    }
}
