using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Explosive : Item{

    public float force;
    public float radius;
    public float hitToleranceDistance;
    protected float deadRadius;
    public int demage;
    public float jumpForce;
    protected bool isDone;
    public HeroType bombActivator = HeroType.Player;
    public GameObject bombActivatorGo;

    public HeroType lastContacter = HeroType.Lifeless;

    [SerializeField] protected GameObject exploisionEffect;

    private void Start()
    {
        deadRadius = radius / 2;
    }

    protected void OnDrawGizmosSelected(){
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius / 2f);
    }

}
