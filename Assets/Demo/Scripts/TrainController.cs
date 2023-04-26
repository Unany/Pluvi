// Created by: William Dye - 2023
// License Type: Proprietary

using UnityEngine;

namespace Mosuva.Pluvi.Demo
{
    public class TrainController : MonoBehaviour
    {
        #region Variables

        #region Public
        public Vector3 defaultPosition;
        public Vector3 endPosition;
        public float trainSpeed = 1.0f;
        #endregion

        #region Private
        private float nextTime = 0;
        private float randomTime;
        private bool trainEnabled = false;
        private float startTime;
        private float trackDistance;
        private float currentDistance;
        private float evaluatedDistance;
        #endregion

        #endregion

        void Start()
        {
            InitialiseTrain();

            trackDistance = Vector3.Distance(defaultPosition, endPosition);
            //trackDistance = defaultPosition.x - endPosition.x;
        }

        void Update()
        {
            // Lerping train between two positions if trains active
            if (!trainEnabled)
            {
                if (Time.time > nextTime)
                {
                    trainEnabled = true;
                    startTime = Time.time;
                }
            }
            else
            {
                LerpTrain();
            }

            // Resets train
            if (this.transform.localPosition == endPosition)
            {
                this.transform.localPosition = defaultPosition;

                InitialiseTrain();
            }
        }

        private void InitialiseTrain()
        {
            randomTime = Random.Range(5.0f, 10.0f);
            nextTime = Time.time + randomTime;
            trainEnabled = false;
        }

        private void LerpTrain()
        {
            currentDistance = (Time.time - startTime) * trainSpeed;
            evaluatedDistance = currentDistance / trackDistance;
            this.transform.localPosition = Vector3.Lerp(defaultPosition, endPosition, evaluatedDistance);
        }
    }
}