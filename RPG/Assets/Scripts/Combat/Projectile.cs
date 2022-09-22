using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float projectileSpeed = 1;

    void Update()
    {
        if (target == null) return;
        transform.LookAt(GetAimLocation());
        transform.Translate(Vector3.forward * Time.deltaTime * projectileSpeed);
    }

    Vector3 GetAimLocation()
    {
        var targetCollider = target.GetComponent<BoxCollider>();
        if (targetCollider == null)
        {
            return target.position;
        }
        return target.position + Vector3.up * targetCollider.size.y / 2;
    }
}
