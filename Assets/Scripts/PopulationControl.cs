using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PopulationControl : MonoBehaviour
{
    //public List<GameObject> food = new List<GameObject>();
    ///public List<GameObject> body = new List<GameObject>();
    public ArrayList pred = new ArrayList();
    public ArrayList food = new ArrayList();
    public ArrayList body = new ArrayList();
    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        text.text += "Hello";
        StartCoroutine(Stats());
    }
    public GameObject FindClosestEnemy(Rigidbody2D rb)
    {
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = rb.transform.position;
        foreach (GameObject go in food)
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

    IEnumerator Stats()
    {
        while (true)
        {
            food.Clear();
            body.Clear();
            pred.Clear();
            pred.AddRange(GameObject.FindGameObjectsWithTag("Pred"));
            food.AddRange(GameObject.FindGameObjectsWithTag("Food"));
            body.AddRange(GameObject.FindGameObjectsWithTag("Body"));
            text.text = "";
            text.text += "\nЕда: " + food.Count;
            text.text += "\nТравоядные: " + body.Count;
            text.text += "\nХищники: " + pred.Count;
            float avg = 0;
            float max = 0;
            float min = 32000;
            foreach (GameObject item in body)
            {

                if (item.GetComponent<Body>().genV > max)
                    max = item.GetComponent<Body>().genV;
                else
                    if (item.GetComponent<Body>().genV < min)
                    min = item.GetComponent<Body>().genV;
                avg += item.GetComponent<Body>().genV;
            }
            if (pred.Count != 0)
                avg = avg / body.Count;
            else
                avg = 0;
            text.text += "\nСкорость травоядных:\nМакс: " + max;
            text.text += "\nМин: " + min;
            text.text += "\nСред: " + avg;
            avg = 0;
            max = 0;
            min = 32000;
            foreach (GameObject item in pred)
            {
                if (item)
                {
                    if (item.GetComponent<Predator>().genV > max)
                        max = item.GetComponent<Predator>().genV;
                    else
                        if (item.GetComponent<Predator>().genV < min)
                        min = item.GetComponent<Predator>().genV;
                    avg += item.GetComponent<Predator>().genV;
                }
            }
            if (pred.Count != 0)
                avg = avg / pred.Count;
            else
                avg = 0;


            text.text += "\nСкорость хищников:\nМакс: " + max;
            text.text += "\nМин: " + min;
            text.text += "\nСред: " + avg;

            yield return new WaitForSeconds(0.5f);
        }


    }
    // Update is called once per frame
    void Update()
    {
        

    }
}
