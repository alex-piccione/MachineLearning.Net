using System;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;
using Should;

using Alex75.MachineLearning.Core;

namespace Alex75.MachineLeraning.Core.Tests
{
    [TestFixture]
    public class Perceptron_Test
    {

        [Test]
        public void Contructor()
        {
            var perceptron = new Perceptron();
            perceptron.ShouldNotBeNull();
        }

        [TestCase(new float[] { 1.0f, 2.0f}, ExpectedResult = 1)]
        [TestCase(new float[] { 5.0f, 10.0f }, ExpectedResult = 1)]
        public int Process__when__Weights_are_Zero(float[] inputs)
		{
            var perceptron = new Perceptron();
            var output = perceptron.Process(inputs);
            return output;
		}


    }
}
