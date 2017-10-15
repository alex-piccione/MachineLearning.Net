using System;
using System.Collections.Generic;
using System.Linq;

namespace Alex75.MachineLearning.Core
{
    public class Perceptron : IPerceptron
    {
        private float[] weights;

        public int Guess(float[] inputs)
        {
            if (weights == null) weights = new float[inputs.Length];

            float netInput = 0;
            for (int x = 0; x < inputs.Length; x++)
                netInput += inputs[x] * weights[x];
            return netInput >= 0 ? 1 : -1;
        }

        public TrainingResult Train(IEnumerable<TrainingRecord> trainingSet, float learningRate = 0.01f, int epochs = 1)
        {
            if (weights == null) weights = new float[trainingSet.First().inputs.Length];

            int epochsCount = 0;
            bool isTrained = false;
            while (!isTrained && epochsCount++ < epochs)
            {
                int setError = 0;
                foreach (var training in trainingSet)
                {
                    var error = training.output - Guess(training.inputs);
                    setError += setError;

                    if (error != 0)
                        for (int x = 0; x < training.inputs.Length; x++)
                            weights[x] += training.inputs[x] * error * learningRate;  // apply delta rule by gradient descent                     
                }
                isTrained = setError == 0;
            }
            
            TrainingResult result = new TrainingResult();
            result.UsedEpochs = epochsCount;
            result.IsTrained = isTrained;
            return result;
        }
    }
}
