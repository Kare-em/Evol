using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public PopulationControl PopulationControl;
    //pred
    public float startspeedpred;
    public float defpredsize;


    //body
    public float startspeed;
    public float defbodysize;


    //food
    public GameObject foodi;
    public GameObject predi;
    float DF = 10f;


    public GameObject bodyi;
    public float R = 750.0f;

    public float timespawnfood = 5.0f;
    public int countspawnfood = 40;
    public int maxCountFood = 500;

    IEnumerator SpawnFood()
    {
        while (true)
        {
            if (maxCountFood > PopulationControl.food.Count)
            {
                for (int i = 0; i < countspawnfood; i++)
                {
                    GameObject obj = Instantiate(foodi, new Vector3(Random.Range(-R, R), Random.Range(-R, R), 0),
                        Quaternion.identity);

                    obj.transform.localScale = Vector3.one*DF;
                }
            }

            yield return new WaitForSeconds(timespawnfood);
        }
    }

    public float D(float Size)
    {
        return 2 * (float) System.Math.Sqrt(Size / System.Math.PI);
    }
    
    public GameObject SpawnBody()
    {
        GameObject obj = Instantiate(bodyi);
        obj.GetComponent<Body>().genV = startspeed;
        obj.GetComponent<Body>().lifetime = 0;
        obj.GetComponent<Body>().deadtime = 200000 / obj.GetComponent<Body>().genV;
        obj.GetComponent<Body>().size = defbodysize;
        float bufD = D(defbodysize);
        obj.GetComponent<Body>().transform.localScale = new Vector3(bufD, bufD, 1);
        obj.GetComponent<Body>().transform.position = new Vector3(-500, 0, 0);

        return obj;
    }

    public GameObject SpawnPred()
    {
        GameObject obj = Instantiate(predi);
        obj.GetComponent<Predator>().genV = startspeedpred;
        obj.GetComponent<Predator>().lifetime = 0;
        obj.GetComponent<Predator>().deadtime = 20000 / obj.GetComponent<Predator>().genV;
        obj.GetComponent<Predator>().size = defpredsize;
        float bufD = D(defpredsize);
        obj.GetComponent<Predator>().transform.localScale = new Vector3(bufD, bufD, 1);
        obj.GetComponent<Predator>().transform.position = new Vector3(10, 0, 0);

        return obj;
    }

    void Start()
    {
        StartCoroutine(SpawnFood());
        SpawnBody();
        SpawnPred();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            SpawnPred();
    }
}