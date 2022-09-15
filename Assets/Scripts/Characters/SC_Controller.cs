using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Controller : MC_Controller
{
    public float position_offset = 0f;

    // Child class for side characters
    private void Update()
    {
        // If I am not pushed back by a column, go back to default position
        if (moving_back == false)
        {
            this.transform.position = new Vector2(middle_of_skateboard - position_offset, this.transform.position.y);
        }
    }
}
