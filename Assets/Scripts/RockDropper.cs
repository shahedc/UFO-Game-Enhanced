using UnityEngine;
using System.Collections;

public class RockDropper : MonoBehaviour {
    public GameObject rockSpawner;
    public GameObject rock;
    public GameObject background;


    private float screenLeft = 0;
    private float screenRight = 0;
    private float spawnerHeight = 0;
    private float offset = 5;
    private float dropInterval = 1.0F;

    // Use this for initialization
    void Start ()
    {
        float bgWidth = background.GetComponent<Renderer>().bounds.size.x;
        screenLeft = -bgWidth / 2.0f + offset;
        screenRight = bgWidth / 2.0f - offset;
        spawnerHeight = rockSpawner.transform.position.y;
        StartCoroutine(DropRock(dropInterval));

    }


    private IEnumerator DropRock(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);

            Vector3 newPosition = new Vector3(Random.Range(screenLeft, screenRight), spawnerHeight);

            GameObject rockInstance = (GameObject)Instantiate(rock);
            rockInstance.transform.position = rockSpawner.transform.position;
            rockSpawner.transform.position = newPosition;
        }
    }

    // Update is called once per frame
    void Update () {
	
	}
}
