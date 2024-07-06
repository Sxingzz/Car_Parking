using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ContactInfo
{
    public bool contacted;
    public Vector3 point;
    public Collider collider;
    public Transform transform;
}

public class RaycastDetector
{
    public ContactInfo RayCast(int layerMask)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool hit = Physics.Raycast(ray, out RaycastHit hitInfo, float.PositiveInfinity, 1<<layerMask);

        // 1 << layerMask là một phép toán bit shift trái để tạo ra một layer mask từ một số nguyên layerMask.

        return new ContactInfo
        {
            contacted = hit,
            point     = hitInfo.point,
            collider  = hitInfo.collider,
            transform = hitInfo.transform,

        };
    }
}
