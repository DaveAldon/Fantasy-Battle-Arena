using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

namespace PC2D
{
    public class MotorMonitor : NetworkBehaviour
    {
        [SyncVar(hook = "OnChangeState")]
        public string state;
        public Text motorStateTextBox;
        public PlatformerMotor2D motorToWatch;
        public GameObject visualChild;
        public Animator playerAnim;

        public PlatformerMotor2D.MotorState _savedMotorState;

        void Start() {
            playerAnim = visualChild.GetComponent<Animator>();
        }

        void OnChangeState(string state) {
            motorStateTextBox.text = state;
        }

        private PlatformerMotor2D.MotorState MotorState
        {
            set
            {
                if (_savedMotorState != value)
                {
                    //playerAnim.Play("Dash");
                    //prevMotorStateText.text = string.Format("Prev Motor State: {0}", _savedMotorState);
                    state = string.Format("{0}", value);
                }
                _savedMotorState = value;
            }
        }

        // Update is called once per frame
        void Update()
        {
            
            MotorState = motorToWatch.motorState;
        }
    }
}
