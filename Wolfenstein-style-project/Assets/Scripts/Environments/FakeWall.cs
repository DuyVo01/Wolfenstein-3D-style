using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeWall : MonoBehaviour, IDamagable
{
    public Animator fakeWallAnimator;
    private bool isHit;
    // Start is called before the first frame update
    void Start()
    {
        isHit = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(float damageAmount)
    {
        isHit = true;
        fakeWallAnimator.SetBool("isHit", isHit);
        
    }

    public void InactiveSelf()
    {
        gameObject.SetActive(false);
    }

}
