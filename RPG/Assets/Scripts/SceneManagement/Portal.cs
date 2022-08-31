using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour 
    {
        [SerializeField] int sceneToLoad = -1;
        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player" == false) return;
            SceneManager.LoadScene(sceneToLoad);
        }
    }

}