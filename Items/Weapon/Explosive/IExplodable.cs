
using System.Collections;
using UnityEngine;

public interface IExplodable{
    public void Activate(HeroType heroType, GameObject activatorGo);
    public void ActivateExplodable(Collider collider);
    public void PushRigidbody(Collider collider);

    public void BreakFixedJoint(Collider collider);

    public void GiveDemage(Collider collider);

    public bool DeadDistanceDetected(Collider  collider);

    public bool RaycastIntersectionDetected(Collider collider);

}
