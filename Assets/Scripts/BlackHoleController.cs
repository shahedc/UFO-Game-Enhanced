using UnityEngine;
using System.Collections;

public class BlackHoleController : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Rock"))
        {
            var theRock = other.gameObject;
            theRock.SetActive(false);
            Destroy(theRock);
        }
    }
}
