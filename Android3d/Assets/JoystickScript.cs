using UnityEngine;

public class JoystickScript : MonoBehaviour
{
    [SerializeField] GameObject joyBase;

    public static JoystickScript singl;
    public Vector2 dir;
    public float offset;


    private void Awake()
    {
        singl = this;
    }


    void Update()
    {
        //if(Input.touchCount > 0)
        {
            dir = (transform.position - joyBase.transform.position).normalized;
            offset = (transform.position - joyBase.transform.position).magnitude;
        }
        
    }

   
    private void OnMouseDrag()
    {
        //print(dir);
        
        transform.position = joyBase.transform.position + Vector3.back +
            Vector3.ClampMagnitude(Input.mousePosition - joyBase.transform.position, 100f);        
    }

    private void OnMouseUp()
    {
        transform.position = new Vector3(joyBase.transform.position.x, joyBase.transform.position.y, -2f);
    }

}
