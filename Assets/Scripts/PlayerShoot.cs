using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour {
    public float speed;
	// Use this for initialization
	void Start () {
        speed = 8f;
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 position = transform.position;
        position = new Vector2(position.x, position.y + speed * Time.deltaTime);

        transform.position = position;
        
        Vector2 topRight = Camera.main.ViewportToWorldPoint(new Vector2(1,1));

        if (transform.position.y > topRight.y)
        {
            Destroy(gameObject);
        }

	}
}
