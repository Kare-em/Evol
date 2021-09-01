using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    //Properties
    public SpriteRenderer sr;
    public GameObject bodyi;
    public Rigidbody2D rb;
    public CircleCollider2D cc2d;
    public bool find = false;
    PopulationControl population = new PopulationControl();

    float devidesize = 500.0f;
    GameObject rbf = null;

    public float k;

    public float size;

    public float lifetime;

    public float deadtime;

    public float genV;

    public float mutagen;
    private bool danger = false;

    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Pred")
        {

            danger = true;
            rbf = null;
        }

    }

    private void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.tag == "Pred")
        {
            coll.GetComponent<Predator>().find = true;

            rb.AddForce((rb.transform.position - coll.gameObject.transform.position).normalized * genV * k);
            coll.GetComponent<Predator>().rb.AddForce((rb.transform.position - coll.gameObject.transform.position).normalized * coll.GetComponent<Predator>().genV * coll.GetComponent<Predator>().kv);

        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.tag == "Pred")
        {
            coll.GetComponent<Predator>().find = false;
            danger = false;
            Debug.Log("no danger");
        }

    }

    public void SpawnBody()
    {
        Debug.Log("Spawnbody");
        GameObject obj = Instantiate(this.gameObject);
        obj.GetComponent<Rigidbody2D>().velocity = rb.velocity;
        this.size = this.size / 2;
        this.transform.localScale = new Vector3(D(this.size), D(this.size), 1);
        obj.GetComponent<Body>().size = this.size;
        obj.GetComponent<Body>().transform.localScale = this.transform.localScale;
        obj.GetComponent<Body>().transform.position += new Vector3(2 * this.transform.localScale.x, 2 * this.transform.localScale.x, 0);
        obj.GetComponent<Body>().genV += Random.Range(-mutagen, mutagen);
        obj.GetComponent<Body>().lifetime = 0;
        obj.GetComponent<Body>().deadtime = 200000 / genV;
        obj.GetComponent<SpriteRenderer>().color = new Color(0.5f, obj.GetComponent<Body>().genV * 0.0003f, obj.GetComponent<Body>().genV * 0.0003f, 1.0f);
        if (obj.GetComponent<Body>().genV <= 0)
            Destroy(obj);

    }
    public float D(float Size)
    {
        return 2 * (float)System.Math.Sqrt(Size / System.Math.PI);
    }



    void Update()
    {

        if (lifetime > deadtime - 1)
            sr.color = new Color(0.8f, 0.2f, 0.2f, 0.5f);
        if (lifetime > deadtime)
            Destroy(this.gameObject);
        else
        {
            if (size > devidesize)
                /*if (Random.Range(0, 100) < 40)
                {
                    Sprite.
                    Spawner predator = new Spawner();
                    predator.SpawnPred();
                }
                else*/
                    SpawnBody();
            if (!danger)
            {
                if (rbf == null)
                {
                    population.food.Clear();
                    population.food.AddRange(GameObject.FindGameObjectsWithTag("Food"));
                    rbf = population.FindClosestEnemy(rb);



                }
                else
                {
                    //rb.mass = size;
                    lifetime += Time.deltaTime;




                    rb.AddForce((rbf.transform.position - rb.transform.position).normalized * genV * k);

                }
            }
            else
            {
                rbf = null;
            }

        }
    }
}
