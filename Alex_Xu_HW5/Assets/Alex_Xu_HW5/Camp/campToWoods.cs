using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class campToWoods : MonoBehaviour
{

    private BoxCollider2D bc;

    private void Awake()
    {
        bc = GetComponent<BoxCollider2D>();
        bc.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            GameObject.Find("Player").transform.position = new Vector2(8.39f, 0);
            SceneManager.LoadScene("Woods");
        }

    }
}