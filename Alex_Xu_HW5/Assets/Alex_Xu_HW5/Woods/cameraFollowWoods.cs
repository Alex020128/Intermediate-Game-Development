using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollowWoods : MonoBehaviour
{

    public float followSpeed;
    public float yPos;
    private Transform player;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 targetPos = player.position;
        Vector2 smoothPos = Vector2.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);

        smoothPos.x = Mathf.Clamp(smoothPos.x, -36.5f, 2);
        smoothPos.y = Mathf.Clamp(smoothPos.y, -14, 14);
        transform.position = new Vector3(smoothPos.x, smoothPos.y + yPos, -15.0f);
    }
}
