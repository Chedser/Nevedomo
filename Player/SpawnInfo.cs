using UnityEngine;
public enum HeroGroup {Human, Alien}
public enum HeroType { Player, Enemy, Neutral, Helper, Lifeless }

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class SpawnInfo:MonoBehaviour{

    public HeroType heroType;
    public HeroGroup heroGroup;
}
