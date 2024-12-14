using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class camara : MonoBehaviour
{
    public Transform target; // ตัวละครหรืออ็อบเจกต์ที่ต้องการให้กล้องตาม

    void Update()
    {
        if (Input.GetMouseButton(0))
        {

        }
        if (Input.GetMouseButton(0)==false)
        {
            transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        }

    } 
}
