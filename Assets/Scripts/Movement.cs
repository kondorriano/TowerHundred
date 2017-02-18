using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public Animator anim;
	
	// Update is called once per frame
	void Update () {
        anim.SetFloat("HorizontalAxis", Input.GetAxis("Horizontal"));
        anim.SetFloat("VerticalAxis", Input.GetAxis("Vertical"));
    }
}
