using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speedX = 6.0f;
    public float speedY = 350.0f;
    public float gravity = 4f;

    bool tocandoPiso = false;

    private Rigidbody2D rigidBody;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rigidBody.gravityScale = gravity;

        rigidBody.velocity = new Vector2(Input.GetAxis("Horizontal") * speedX, rigidBody.velocity.y);

        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, speedY);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 origenRayoIzquierda = new Vector3(transform.position.x - transform.localScale.x / 2, transform.position.y, transform.position.z);
        Vector3 direccionRayo = transform.TransformDirection(Vector3.down);

        RaycastHit hitInfo;

        if (Physics.Raycast(origenRayoIzquierda, direccionRayo, out hitInfo, Mathf.Infinity))
        {
            Debug.DrawRay(origenRayoIzquierda, direccionRayo * hitInfo.distance, Color.green, 3);
            Debug.Log("Raycast DID hit");
        }
        else
        {
            Debug.DrawRay(origenRayoIzquierda, direccionRayo * 30, Color.red, 3);
            Debug.Log("Raycast DID NOT hit");
        }

        // Solo estoy tocando el piso si el objeto es mapa, y si es desde arriba
        //if (collision.gameObject.CompareTag("Mapa"))
        //{
        //    tocandoPiso = true;
        //}
    }

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Mapa"))
    //        tocandoPiso = false;
    //}
}

