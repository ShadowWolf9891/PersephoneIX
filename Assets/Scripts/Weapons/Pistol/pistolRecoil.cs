using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pistolRecoil : MonoBehaviour
{
    public GameObject Gun;
    public Animator anim;
    public AnimationClip pr;


    private void Start()
    {
        anim = GetComponent<Animator>();
        
    }



    public void StartRecoil()
    {
        anim.Play("pistolRecoil");
        RecoilAnim();  
    }

  
    IEnumerator RecoilAnim()
    {
        anim.SetBool("isShooting", true);
        yield return new WaitForSeconds(2.0f);
        anim.SetBool("isShooting", false);
    }
}
