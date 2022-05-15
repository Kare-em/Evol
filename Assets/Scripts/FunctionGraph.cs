using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionGraph : MonoBehaviour
{
    [SerializeField] private GameObject point;
    [SerializeField] private Transform parent;
    [SerializeField] private float count;
    [SerializeField] private float step;
    [SerializeField] private float scaleY = 1f;
    [SerializeField] private Entity entity;
    private List<Transform> points;
    private int currentPoint = 0;
    private float currentValueY = 0;
    private ArrayList target;

    private enum Entity
    {
        Predator,
        Body,
        Food
    }

    void Start()
    {
        points = new List<Transform>();
        for (int i = 0; i < count; i++)
        {
            points.Add(Instantiate(point, parent.position + Vector3.right * (i * step), Quaternion.identity, parent)
                .transform);
        }

        switch (entity)
        {
            case Entity.Body:
                target = PopulationControl.Control.body;
                break;
            case Entity.Predator:
                target = PopulationControl.Control.pred;
                break;
            case Entity.Food:
                target = PopulationControl.Control.food;
                break;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var position1 = parent.position;
        position1 += Vector3.left * step;
        parent.position = position1;
        Transform point = points[currentPoint];
        Vector3 position = point.localPosition;
        position.x += count * step;
        currentValueY = target.Count*scaleY;
        position.y = currentValueY;
        point.localPosition = position;
        currentPoint++;
        if (currentPoint > points.Count - 1)
            currentPoint = 0;
    }
}