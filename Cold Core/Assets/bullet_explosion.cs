using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_explosion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(destroy());
    }

    // Update is called once per frame

    IEnumerator destroy()
    {
        yield return new WaitUntil(() => Boom == true);
        destroy(gameObject);
    }
}
