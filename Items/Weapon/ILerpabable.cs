
using UnityEngine;

public interface ILerpabable{
    public Color Lerp3(Color a, Color b, Color c, float t);
    public void UpdateWeaponIndicator(float current, float init);
    public void UpdateWeaponIndicator();
}
