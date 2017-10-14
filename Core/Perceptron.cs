using System;
using System.Collections.Generic;
using System.Linq;

namespace Alex75.MachineLearning.Core
{
    public class Perceptron : IPerceptron
    {
        public int Process(float[] inputs)
        {
            float netInput = 0;
            for (int x = 0; x < inputs.Length; x++)
                netInput += inputs[x]*0;
            return netInput >= 0 ? 1 : -1;
        }
    }
}
