using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteveAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    public Animation animation;

    void Run()
    {
        animation.Play("Run");
    }
}
