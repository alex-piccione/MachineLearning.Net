using System;
using System.Collections.Generic;

namespace Alex75.MachineLearning.Core
{
    public interface IPerceptron
    {
        int Guess(float[] inputs);
        ///<summary>Train the perceptron and return True if output is equal to target.</summary>
        TrainingResult Train(float[] inputs, int target, float learningRate, int epochs);
        
    }
}
