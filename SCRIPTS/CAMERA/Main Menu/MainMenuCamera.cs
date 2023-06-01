using UnityEngine;

namespace Jobegiar99.Camera
{
    public class MainMenuCamera : MonoBehaviour
    {
        // Sets the movement of the camera and the speed at which it moves * Time.deltaTime;
        [SerializeField] Vector3 direction;
        [SerializeField] float speed;
        [Space(10)]
        //Location within the world to move the camera back to when it traverses the scene.
        [SerializeField] Transform startPoint;


        // Update is called once per frame
        void Update()
        {
            transform.position += direction * speed * Time.deltaTime;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "MainMenuCameraEndPoint")
                transform.position = startPoint.position;
        }
    }
}
