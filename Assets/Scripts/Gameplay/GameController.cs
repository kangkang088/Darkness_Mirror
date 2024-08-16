using Mirror;
using System.Collections.Generic;
using UnityEngine;

namespace Darkness
{
    public class GameController : NetworkBehaviour
    {
        public Ball ball;

        public List<Transform> ballPoints = new();

        public override void OnStartServer()
        {
            base.OnStartServer();

            foreach(Transform point in ballPoints)
            {
                var ballObj = Instantiate(ball);

                ballObj.transform.SetPositionAndRotation(point.position,Quaternion.identity);

                NetworkServer.Spawn(ballObj.gameObject);
            }
        }
    }
}
