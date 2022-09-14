using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelRotate : MonoBehaviour
{
    // Variables
    public float acceleration = 1f; // Increases wheel rotation
    [SerializeField] private float rotation_speed = 6f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //float new_rotation = this.gameObject.transform.rotation.z - 1;
        //this.gameObject.transform.rotation = new Quaternion(this.gameObject.transform.rotation.x, this.gameObject.transform.rotation.y, new_rotation * acceleration,this.gameObject.transform.rotation.w * Time.deltaTime);
        this.transform.RotateAroundLocal(Vector3.back, rotation_speed * acceleration * Time.deltaTime);
    }
}