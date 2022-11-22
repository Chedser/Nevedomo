
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{

    [SerializeField]
    GameObject spine;

    public static CameraFollow cam;
    private Camera cam_;
    
    public float sensitivity = 3;
    [HideInInspector]
    public float mouseX, mouseY;
    public float clampSpineUp = 80;
    public float clampSpineDown = -80;
    public Transform player;
    public Transform Camera;

    public float clampUp = 0.0f;
    public float clampDown = 0.0f;

    float xRotation = 0f;

     [SerializeField] PlayerHealth health;

    private void OnBeforeTransformParentChanged()
    {
        
    }

    private void Awake(){
        cam = this;
        cam_ = this.GetComponent<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 
    }
    
    private float rotX = 0.0f, rotY = 0.0f;
    [HideInInspector]
    public float rotZ = 0.0f;
    private void Update(){

        if (health.GetCurrentHealth() <= 0 || 
            GameManager.gameOver || 
            GameManager.isPaused) { return; }
        
        // Mouse input
        mouseX = Input.GetAxisRaw("Mouse X") * sensitivity;
        mouseY = Input.GetAxisRaw("Mouse Y") * sensitivity;

        // Calculations
        rotX -= mouseY;
        rotX = Mathf.Clamp(rotX, clampUp, clampDown);
        rotY += mouseX;

        // Placing values
        transform.localRotation = Quaternion.Euler(rotX, rotY, rotZ);
        player.Rotate(Vector3.up * mouseX);
        transform.position = Camera.position;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, clampSpineUp, clampSpineDown);
    
        Camera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        spine.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); ;
   
    }

    public void Shake(float magnitude, float duration)
    {
        StartCoroutine(IShake(magnitude, duration));
    }

    private IEnumerator IShake(float mag, float dur)
    {
        WaitForEndOfFrame wfeof = new WaitForEndOfFrame();
        for(float t = 0.0f; t <= dur; t += Time.deltaTime)
        {
            rotZ = Random.Range(-mag, mag) * (t / dur - 1.0f);
            yield return wfeof;
        }
        rotZ = 0.0f;
    }
}
