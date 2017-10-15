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

        public TrainingResult Train(float[] inputs, int target, float learningRate, int epochs)
        { 
            if (weights == null) weights = new float[inputs.Length];

            var output = Guess(inputs);
            var error = target - output;
            int epochsCount = 0;
            while (epochsCount < epochs && error != 0)
            {                
                for (int x = 0; x < inputs.Length; x++)
                    weights[x] += inputs[x] * error * learningRate;  // apply delta rule by gradient descent 
                error = target - Guess(inputs);
                epochsCount++;
            }
            TrainingResult result = new TrainingResult();
            result.UsedEpochs = epochsCount;
            result.IsTrained = error == 0;
            return result;
        }
    }
}
