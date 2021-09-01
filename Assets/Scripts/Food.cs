using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Food : MonoBehaviour
{
    public GameObject text;
    private int foodeat = 0;
    Text text1;
    public Rigidbody2D rb;
    public GameObject foodi;
    public float Size = 50;
    private float lifetime=300;
    public float D(float Size)
    {
        return 2 * (float)System.Math.Sqrt(Size / System.Math.PI);
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Body")
        {
            if (coll is BoxCollider2D)
            {
                foodeat++;
                coll.GetComponent<Body>().size += Size;
                float bufD = D(coll.GetComponent<Body>().size);
                coll.transform.localScale = new Vector3(bufD, bufD, 1);

                Destroy(this.gameObject);
            }
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        text1 = text.GetComponent<Text>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
            Destroy(gameObject);
    }
}
