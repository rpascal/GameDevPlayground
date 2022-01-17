using System.Collections;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Assets.Scripts.Layer;
//using Unity.MLAgents.Sensors;
//using Unity.MLAgents.Actuators;

namespace Assets.Scripts.Player {
    public class PlayerAgent : Agent {

        Rigidbody2D rBody;

        private PlayerMovement playerMovement;
        [SerializeField] private AsteriodSpawner asteriodSpawner;

        void Start() {
            rBody = GetComponent<Rigidbody2D>();
            playerMovement = GetComponent<PlayerMovement>();
        }

        public override void OnEpisodeBegin() {
            asteriodSpawner.ResetForGameOver();
            this.transform.localPosition = new Vector3(
                Random.Range(-8f, 8f),
                 Random.Range(-3f, 3f),
                0
            );

            rBody.velocity = Vector2.zero;
            rBody.angularVelocity = 0f;
            playerMovement.setThrusting(0f);
            playerMovement.TurnDirection = 0f;

            Vector3 euler = transform.eulerAngles;
            euler.z = Random.Range(0f, 360f);
            transform.eulerAngles = euler;
        }

        public override void CollectObservations(VectorSensor sensor) {
            //sensor.AddObservation(this.transform.localPosition); // 3
            //print(transform.eulerAngles.z);
            sensor.AddObservation(rBody.velocity); // 2
            //sensor.AddObservation(rBody.angularVelocity); // 1
            sensor.AddObservation(transform.eulerAngles.z); // 1
        }

        public override void OnActionReceived(ActionBuffers actionBuffers) {
            playerMovement.setThrusting(actionBuffers.ContinuousActions[0]);
            playerMovement.TurnDirection = actionBuffers.ContinuousActions[1];
            AddReward(1f / MaxStep);
        }

        public override void Heuristic(in ActionBuffers actionsOut) {
            var continuousActionsOut = actionsOut.ContinuousActions;
            continuousActionsOut[0] = Input.GetAxis("Vertical");
            continuousActionsOut[1] = Input.GetAxis("Horizontal");
        }

        private void OnCollisionEnter2D(Collision2D collision) {
            if (Tags.hasTag(collision, Tags.BoundaryTag)) {
                AddReward(-.25f);
                EndEpisode();
            }

            if (Tags.hasTag(collision, Tags.AsteriodTag)) {
                print("Asteriod Hit");
                AddReward(-1f);
                EndEpisode();
            }

        }

    }
}