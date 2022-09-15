using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnsMovement : MonoBehaviour
{
    // Variables
    public float speed;

    private void Update()
    {
        this.gameObject.transform.position = new Vector2(this.gameObject.transform.position.x - 1 * speed * Time.deltaTime, this.gameObject.transform.position.y);
    }
}
