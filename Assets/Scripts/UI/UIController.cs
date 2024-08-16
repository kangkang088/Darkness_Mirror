using Mirror;
using UnityEngine;
using UnityEngine.UI;

namespace Darkness
{
    public class UIController : NetworkBehaviour
    {
        private static UIController mInstance;

        public static UIController Instance
        {
            get
            {
                return mInstance;
            }
        }

        public Button mBtnSetName;

        public InputField mInputName;

        public Text mTime;

        public float time = 100;

        private void Awake()
        {
            if(mInstance == null)
            {
                mInstance = this;
            }

            mTime = transform.Find("Time").GetComponent<Text>();
        }

        private void Update()
        {
            if(isClient)
            {
                time -= Time.deltaTime;

                mTime.text = Mathf.FloorToInt(time + 0.5f).ToString();
            }
        }

        public override void OnStopClient()
        {
            base.OnStopClient();

            time = 100;

            mTime.text = "";
        }
    }
}
