using UnityEngine;

namespace Viking.Scripts.UI.StartScreen
{
    public class BackgroundAnimation : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 10f;

        private void Update()
        {
            // Rotate the parent object around the Y-axis at a constant speed
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
}
