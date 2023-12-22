using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform[] backGrounds;
    public float speed;

    public float limitY = 20;

    void Update()
    {
        foreach (Transform background in backGrounds)
        {
            background.Translate(Vector3.down * Time.deltaTime * speed);

            if (background.position.y < -limitY)
            {
                var pos = background.position;
                pos.y += limitY * 2f;
                background.position = pos;
            }
        }
    }
}
