using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Slider : MonoBehaviour
{
    public GameObject Spawner;
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = FindObjectOfType<Slider>();
    }
    
    // Update is called once per frame
    void Update()
    {
        //GetComponent<Spawner>().timespawnfood= slider.GetComponent()
    }
}
