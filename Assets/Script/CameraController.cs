using System.Collections;
using UnityEngine;

namespace Assets.Script
{
    public class CameraController : MonoBehaviour
    {
        private float size = 5;
        private readonly int speed = 5;
        private Camera _camera;
        

        
        // Start is called before the first frame update
        void Start()
        {
            _camera = GetComponent<Camera>();
        }





        // Update is called once per frame
        void Update()
        {           

            //Move in four direction
            if (Input.GetKey(KeyCode.RightArrow) && transform.position.x < 30)
            {
                transform.Translate(new Vector3(Time.deltaTime * speed, 0, 0));
            }
            if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x > -30)
            {
                transform.Translate(new Vector3(-Time.deltaTime * speed, 0, 0));
            }

            if (Input.GetKey(KeyCode.UpArrow) && transform.position.y < 30)
            {
                transform.Translate(new Vector3(0, Time.deltaTime * speed, 0));
            }

            if (Input.GetKey(KeyCode.DownArrow) && transform.position.y > -30)
            {
                transform.Translate(new Vector3(0, -Time.deltaTime * speed, 0));
            }

            //Zoom in & out

            if (Input.GetKey(KeyCode.PageDown))
            {
                size += 0.5f;
                if (size > 70) size = 70;
                _camera.orthographicSize = size;
            }
            if (Input.GetKey(KeyCode.PageUp))
            {
                size -= 0.5f;
                if (size < 2) size = 2;
                _camera.orthographicSize = size;
            }
        }
    }
}
