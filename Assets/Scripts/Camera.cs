using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Camera cam;
    public Rigidbody2D im;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnMouseDown()
    {
        //im.MovePosition(Vector3.back);
    }
    // Update is called once per frame
    void Update()
    {
        //cam.size += new Vector3(10 * Input.GetAxis("Mouse ScrollWheel"), 10 * Input.GetAxis("Mouse ScrollWheel"),0);
    }
}
