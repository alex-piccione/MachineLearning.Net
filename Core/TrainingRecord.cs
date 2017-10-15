using System;
using System.Collections.Generic;
using System.Linq;

namespace Alex75.MachineLearning.Core
{
    public class TrainingRecord
    {
        public float[] inputs;
        public int output;

        public TrainingRecord(float[] inputs, int output)
        {
            this.inputs = inputs;
            this.output = output;
        }
    }
}
