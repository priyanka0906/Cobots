using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    private Animator anim1,anim2,anim3,anim4;
    public GameObject R1, R2, R3, R4;
    private int R=0;

    void Start()
    {
        anim1 = R1.GetComponent<Animator>();
        anim2 = R2.GetComponent<Animator>();
        anim3 = R3.GetComponent<Animator>();
        anim4 = R4.GetComponent<Animator>();

        Debug.Log(anim1.name);
    }
    public void setValue(int v)
    {
        R = v;
    }
    public void Explode()
    {

        if (R == 1)
        {
            anim1.SetFloat("speed", 1.0f);
            anim1.Play("CobotAnimation");
        }

        else if(R==2)
        {
            anim2.SetFloat("speed", 1.0f);
            anim2.Play("TM5Animation");
        }
        else if(R==3)
        {
            anim3.SetFloat("speed", 1.0f);
            anim3.Play("KR30");
        }
        else if (R == 4)
        {
            anim4.SetFloat("speed", 1.0f);
            anim4.Play("KR8");
        }

    }

    public void StopAnimation()
    {
        anim1.enabled=false;
        anim2.enabled = false;
    }
    public void Assemble()
    {

        if (R == 1)
        {
            anim1.enabled = true;
            anim1.SetFloat("speed", -1.0f);
            anim1.Play("CobotAnimation");
        }

        else if (R == 2)
        {
            anim2.enabled = true;
            anim2.SetFloat("speed", -1.0f);
            anim2.Play("TM5Animation");
        }
        else if (R == 3)
        {
            anim3.enabled = true;
            anim3.SetFloat("speed", -1.0f);
            anim3.Play("KR30");
        }
        else if (R == 4)
        {
            anim4.enabled = true;
            anim4.SetFloat("speed", -1.0f);
            anim4.Play("KR8");
        }
    }
}
