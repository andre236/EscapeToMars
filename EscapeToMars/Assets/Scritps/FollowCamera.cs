﻿using System.Collections;
using UnityEngine;

public class FollowCamera : MonoBehaviour {

    [SerializeField]
    private float _moveSpeed = 3f;

    private GameObject followObject;
    private Vector2 threshold;
    private Rigidbody2D rb;
    
    public Vector2 followOffset = new Vector2(6, 3);

    private void Awake() {
        StartCoroutine("Reference");
    }

    void FixedUpdate() {
        Vector2 follow = followObject.transform.position;
        float xDifference = Vector2.Distance(Vector2.right * transform.position.x, Vector2.right * follow.x);
        float yDifference = Vector2.Distance(Vector2.up * transform.position.y, Vector2.up * follow.y);

        Vector3 newPosition = transform.position;
        if(Mathf.Abs(xDifference) >= threshold.x) {
            newPosition.x = follow.x;
        }
        if(Mathf.Abs(yDifference) >= threshold.y) {
            newPosition.y = follow.y;
        }
        float moveSpeed = rb.velocity.magnitude > _moveSpeed ? rb.velocity.magnitude : _moveSpeed;
        transform.position = Vector3.MoveTowards(transform.position, newPosition, moveSpeed*Time.deltaTime);

    }

    private Vector3 CalculateThreshold() {
        Rect aspect = Camera.main.pixelRect;
        Vector2 t = new Vector2(Camera.main.orthographicSize * aspect.width / aspect.height, Camera.main.orthographicSize);
        t.x -= followOffset.x;
        t.y -= followOffset.y;
        return t;
    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Vector2 border = CalculateThreshold();
        Gizmos.DrawWireCube(transform.position, new Vector3(border.x * 2, border.y * 2, 1));
    }

    IEnumerator Reference()
    {
        yield return new WaitForSeconds(1f);
        followObject = GameObject.FindGameObjectWithTag("Player");
        threshold = CalculateThreshold();
        rb = followObject.GetComponent<Rigidbody2D>();
    }
}
