using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movSpikes : MonoBehaviour
{
    public float min;
    public float max;

    // Start is called before the first frame update
    void Start()
    {

        min = transform.position.y;
        max = transform.position.y + 3;

    }

    // Update is called once per frame
    void Update()
    {


        transform.position = new Vector3(transform.position.x, Mathf.PingPong(Time.time * 2, max - min) + min, transform.position.z);

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        col.collider.transform.SetParent(transform);
    }
    void OnCollisionExit2D(Collision2D col)
    {
        col.collider.transform.SetParent(null);
    }
}
