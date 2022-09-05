using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using RPG.Saving;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        enum DestinationIdentifier 
        {
            A, B, C, D, E
        }


        [SerializeField] int sceneToLoad = -1;
        [SerializeField] Transform spawnPoint;
        [SerializeField] DestinationIdentifier destination;
        [SerializeField] float fadingOutTime = 2f;
        [SerializeField] float fadingInTime = 1f;

        SavingWrapper savingWrapperScript;

        void Awake()
        {
            savingWrapperScript = FindObjectOfType<SavingWrapper>();
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player" == false) return;
            StartCoroutine(Transition());
        }

        IEnumerator Transition()
        {
            DontDestroyOnLoad(gameObject);

            var fader = FindObjectOfType<Fader>();

            yield return fader.FadeOut(fadingOutTime);
            //savingWrapperScript.Save();
            yield return SceneManager.LoadSceneAsync(sceneToLoad);

            //savingWrapperScript.Load();

            var otherPortal = GetOtherPortal();
            UpdatePlayerPosition(otherPortal);

            //savingWrapperScript.Save();

            yield return fader.FadeIn(fadingInTime);

            Destroy(gameObject);
        }

        void UpdatePlayerPosition(Portal otherPortal)
        {
            var player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
            player.transform.rotation = otherPortal.spawnPoint.rotation;
        }

        Portal GetOtherPortal()
        {
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) continue;
                if (portal.destination != destination) continue;
                return portal;
            }
            return null;
        }
    }
}