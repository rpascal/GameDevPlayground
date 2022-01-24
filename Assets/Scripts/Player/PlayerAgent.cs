using System.Collections;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Assets.Scripts.Layer;


namespace Assets.Scripts.Player {
    public class PlayerAgent : Agent {

        Rigidbody2D rBody;

        private PlayerMovement playerMovement;
        [SerializeField] private AsteriodSpawner asteriodSpawner;

        float allowedHits = 10.0f;
        float hits = 0f;

        float allowedStayOnBorder = 25.0f;
        float borderStays = 0f;
        EnvironmentParameters m_ResetParams;

        public override void Initialize() {
            m_ResetParams = Academy.Instance.EnvironmentParameters;
        }

        void Start() {
            rBody = GetComponent<Rigidbody2D>();
            playerMovement = GetComponent<PlayerMovement>();
        }

        public override void OnEpisodeBegin() {

            asteriodSpawner.ResetForNewEpisode(
              m_ResetParams.GetWithDefault("asteriod_spawn_rate", 5.0f),
                 m_ResetParams.GetWithDefault("asteriod_max_size", 1.5f),
                  m_ResetParams.GetWithDefault("asteriod_min_size", 0.5f)
            );
            this.transform.localPosition = new Vector3(
                Random.Range(-8f, 8f),
                Random.Range(-3f, 3f),
                0
            );

            hits = 0;
            borderStays = 0f;

            stopPlayer();

            Vector3 euler = transform.eulerAngles;
            euler.z = Random.Range(0f, 360f);
            transform.eulerAngles = euler;

            allowedHits = m_ResetParams.GetWithDefault("allowed_asteriod_hits", 10.0f);
            allowedStayOnBorder = m_ResetParams.GetWithDefault("allowed_boundary_hits", 25.0f);
        }

        public override void CollectObservations(VectorSensor sensor) {
            //sensor.AddObservation(this.transform.localPosition); // 3
            sensor.AddObservation(rBody.velocity); // 2
            sensor.AddObservation(rBody.angularVelocity); // 1
            sensor.AddObservation((int)transform.rotation.eulerAngles.z); // 1
        }

        public override void OnActionReceived(ActionBuffers actionBuffers) {
            AddReward(1f / MaxStep);

            playerMovement.setThrusting(actionBuffers.ContinuousActions[0]);
            playerMovement.TurnDirection = actionBuffers.ContinuousActions[1];
        }

        public override void Heuristic(in ActionBuffers actionsOut) {
            var continuousActionsOut = actionsOut.ContinuousActions;
            continuousActionsOut[0] = Input.GetAxis("Vertical");
            continuousActionsOut[1] = Input.GetAxis("Horizontal");

            //var discreteOut = actionsOut.DiscreteActions;
            ////print(Input.GetAxis("Vertical"));
            //if (Input.GetAxis("Vertical") > 0f) {
            //    discreteOut[0] = 1;
            //} else {
            //    discreteOut[0] = 0;
            //}
            //var horizontal = Input.GetAxis("Horizontal");
            //if (horizontal < 0) {
            //    discreteOut[1] = 1;
            //} else if (horizontal > 0) {
            //    discreteOut[1] = 2;
            //} else {
            //    discreteOut[1] = 0;
            //}

        }

        private void OnCollisionEnter2D(Collision2D collision) {
            if (Tags.hasTag(collision, Tags.BoundaryTag)) {
                AddReward(-.25f);
            } else if (Tags.hasTag(collision, Tags.AsteriodTag)) {
                Destroy(collision.gameObject);
                stopPlayer();
                AddReward(-1f / allowedHits);
                hits++;
                if (hits >= allowedHits) {
                    EndEpisode();
                }
            }

        }

        private void OnCollisionStay2D(Collision2D collision) {
            if (Tags.hasTag(collision, Tags.BoundaryTag)) {
                AddReward(-.25f / allowedStayOnBorder);

                borderStays++;
                if (borderStays >= allowedStayOnBorder) {
                    EndEpisode();
                }

            }
        }

        private void OnCollisionExit2D(Collision2D collision) {
            if (Tags.hasTag(collision, Tags.BoundaryTag)) {
                borderStays = 0;
            }
        }

        private void stopPlayer() {
            rBody.velocity = Vector2.zero;
            rBody.angularVelocity = 0f;
            playerMovement.setThrusting(0f);
            playerMovement.TurnDirection = 0f;
        }
    }
}