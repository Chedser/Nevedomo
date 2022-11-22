using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMychalkinFootball : AINeutral
{
    [SerializeField] Transform mychkaPlace;
    
    protected override bool AimDetected(GameObject aim)
    {
        throw new System.NotImplementedException();
    }

    protected override bool AimDetected()
    {
        throw new System.NotImplementedException();
    }

    private void Update()
    {
        navmeshAgent.destination = mychkaPlace.position;
    }

}
