using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public GameObject target;
    Transform Play;

    private float velocity = 0.0f;
    public float smoothTime = 0.1f;

    public AudioClip audioBackground;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Play = target.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            audioSource.clip = audioBackground;
            audioSource.Play();
        }

        float x_position = Mathf.SmoothDamp(transform.position.x, Play.position.x, ref velocity, smoothTime);

        transform.position = new Vector3(x_position, Play.position.y, transform.position.z);
    }
}
