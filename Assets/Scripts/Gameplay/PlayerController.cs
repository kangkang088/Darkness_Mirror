using Mirror;
using UltraReal.MobaMovement;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace Darkness
{
    public class PlayerController : NetworkBehaviour
    {
        [SyncVar(hook = nameof(UpdatePlayerName))]
        private string mName;

        public Text mPlayerName;

        public Text mTime;

        private void Start()
        {
            if(isLocalPlayer)
            {
                GetComponent<NavMeshAgent>().enabled = true;

                GetComponent<MobaMover>().enabled = true;

                GetComponent<MobaAnimate>().enabled = true;

                UIController.Instance.mBtnSetName.onClick.AddListener((() =>
                {
                    TellServerNameChanged(UIController.Instance.mInputName.text);

                    UIController.Instance.mInputName.text = "";
                }));
            }
        }

        private void Update()
        {
            if(isLocalPlayer)
            {
                TellServerUpdateTime(UIController.Instance.time);
            }

            if(Input.GetKeyDown(KeyCode.Space) && isLocalPlayer && isClientOnly)
            {
                TellServerOnePlayerDead(gameObject);
            }
            else if(Input.GetKeyDown(KeyCode.Space) && isLocalPlayer && isServer)
            {
                NetworkManager.Shutdown();
            }
        }

        [Command]
        private void TellServerOnePlayerDead(GameObject gameObject)
        {
            Destroy(gameObject);

            TellClientOnePlayerDead(gameObject);
        }

        [ClientRpc]
        private void TellClientOnePlayerDead(GameObject gameObject)
        {
            Destroy(gameObject);

            if(isLocalPlayer && isClientOnly)
            {
                NetworkClient.Disconnect();
            }
        }

        [Command]
        private void TellServerUpdateTime(float time)
        {
            mTime.text = Mathf.FloorToInt(time + 0.5f).ToString();

            TellClientsUpdateTime(time);
        }

        [ClientRpc]
        private void TellClientsUpdateTime(float time)
        {
            mTime.text = Mathf.FloorToInt(time + 0.5f).ToString();
        }

        [Command]
        private void TellServerNameChanged(string newName)
        {
            mName = newName;
        }

        private void UpdatePlayerName(string _oldValue,string _newValue)
        {
            mPlayerName.text = mName;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Ball") && isLocalPlayer)
            {
                UIController.Instance.AddScore(other.GetComponent<Ball>().Score);
            }
        }
    }
}
