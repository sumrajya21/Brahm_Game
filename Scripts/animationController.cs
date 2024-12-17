using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationController : MonoBehaviour
{
    private Animator animator;
    public Rigidbody Rigidbody;


    [SerializeField]
    private int jumpThreshold = 5;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Rigidbody.velocity.y > jumpThreshold)
        {
            animator.SetBool("Jump", true);
        }
        else if ((Mathf.Abs(Rigidbody.velocity.x) > 1 || (Mathf.Abs(Rigidbody.velocity.z) > 1)) && Input.anyKey)
        {
            animator.SetBool("Jump", false);
            animator.SetBool("Move", true);
        }
        else
        {
            animator.SetBool("Move", false);
            animator.SetBool("Jump", false);
        }
    }
}
