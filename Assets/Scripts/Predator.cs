using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Predator : MonoBehaviour
{
    //Properties
    public SpriteRenderer sr;
    public GameObject bodyi;
    public Rigidbody2D rb;
    public bool find = false;
    PopulationControl population = new PopulationControl();
    public ArrayList body = new ArrayList();
    public float devidesize = 1000.0f;
    GameObject rbf = null;
    public float kadd;//коэф поглощения
    public float kv;//коэф скорости
    public float size;

    public float lifetime;

    public float deadtime;

    public float genV;
    public float mutagen;


    void Start()
    {

    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Body")
        {
            if (coll is BoxCollider2D)
            {
                size += kadd*coll.gameObject.GetComponent<Body>().size;
                float bufD = D(size);
                transform.localScale = new Vector3(bufD, bufD, 1);
                Debug.Log("eat");
                Destroy(coll.gameObject);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.tag == "Body")
        {
            if (coll is CircleCollider2D)
            {
                find = false;
                rbf = null;
            }
        }
    }
    public void SpawnCopy()
    {
        Debug.Log("Spawncopy");
        GameObject obj = Instantiate(gameObject);
        obj.GetComponent<Rigidbody2D>().velocity = rb.velocity;
        size = size / 2;
        transform.localScale = new Vector3(D(size), D(size), 1);
        obj.GetComponent<Predator>().size = size;
        obj.GetComponent<Predator>().transform.localScale = transform.localScale;
        obj.GetComponent<Predator>().genV += Random.Range(-mutagen, mutagen);
        obj.GetComponent<Predator>().lifetime = 0;
        obj.transform.position = gameObject.transform.position;
        obj.GetComponent<Predator>().deadtime = 100000 / genV;
        obj.GetComponent<SpriteRenderer>().color = new Color(0.5f, obj.GetComponent<Predator>().genV * 0.0003f, obj.GetComponent<Predator>().genV * 0.0003f, 1.0f);
        if (obj.GetComponent<Predator>().genV <= 0)
            Destroy(obj);

    }
    public float D(float Size)
    {
        return 2 * (float)System.Math.Sqrt(Size / System.Math.PI);
    }

    public GameObject FindClosestEnemy(Rigidbody2D rb)
    {
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = rb.transform.position;
        foreach (GameObject go in body)
        {
            if (!(go is null))
            {
                Vector3 diff = go.transform.position - rb.transform.position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closest = go;
                    distance = curDistance;
                }
            }
        }
        return closest;
    }

    void Update()
    {
        if (lifetime > deadtime - 1)
            sr.color = new Color(0.8f, 0.2f, 0.2f, 0.5f);
        if (lifetime > deadtime)
        {
            Destroy(this.gameObject);
        }
        else
        {
            if (size > devidesize)
                SpawnCopy();
            if (rbf == null)

            {
                body.Clear();
                body.AddRange(GameObject.FindGameObjectsWithTag("Body"));
                rbf = FindClosestEnemy(rb);


            }
            else
            if(!find)
            {

                //rb.mass = size;





                rb.AddForce((rbf.transform.position - rb.transform.position).normalized * genV * kv);

            }

        }

        lifetime += Time.deltaTime;
    }
}
