﻿using UnityEngine;
using System.Collections;

public class LaZerController : MonoBehaviour
{
    private AstroidGenerator generator;
    private Rigidbody2D rb;
    // Use this for initialization
    void Start()
    {
        generator = GameObject.Find("AstroidGenerator").GetComponent<AstroidGenerator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = new Vector2(transform.up.x, transform.up.y);
        rb.velocity = direction * Time.fixedDeltaTime * 300;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Astroids")
        {
            RaycastHit hit;
            Vector3 contactPoint = new Vector3();
            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                contactPoint = hit.point;
            }

            //blow up astroid
            generator.blowAstroidUp(other.gameObject, contactPoint);

            this.gameObject.SetActive(false);
        }


    }

}