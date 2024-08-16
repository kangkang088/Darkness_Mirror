using Mirror;
using UnityEngine;

namespace Darkness
{
    public class Ball : NetworkBehaviour
    {
        public int Score { get; set; } = 10;

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player"))
            {
                Destroy(gameObject);
            }
        }
    }
}

