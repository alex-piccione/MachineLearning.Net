using System;
using System.Collections.Generic;

namespace Alex75.MachineLearning.Core
{
    public interface IPerceptron
    {
        int Guess(float[] inputs);        
        TrainingResult Train(IEnumerable<TrainingRecord> trainingSet, float learningRate, int epochs = 1);
    }
}
