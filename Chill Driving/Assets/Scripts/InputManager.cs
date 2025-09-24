using UnityEngine;

public class InputManager : MonoBehaviour
{
    public float motorInput;
    public float turnInput;
    
    void Update()
    {
        motorInput = Input.GetAxis("Vertical"); 
        turnInput = Input.GetAxis("Horizontal");
    }
}
