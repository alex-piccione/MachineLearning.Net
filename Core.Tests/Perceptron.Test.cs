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
        [TestCase(new float[] { -1f, -2f }, ExpectedResult = 1)]
        public int Guess__when__not_trained__should__Return_One(float[] inputs)
		{
            var perceptron = new Perceptron();
            var output = perceptron.Guess(inputs);
            return output;
		}

        [TestCase(new float[] { 1.0f, 1.0f }, -1)]
        public void Train__when__trained__should__Return_True(float[] inputs, int target)
        {
            var perceptron = new Perceptron();
            int epochs = 100;
            float learningRate = 0.1f;
            
            var result = perceptron.Train(inputs, target, learningRate, epochs);

            result.IsTrained.ShouldBeTrue();
            result.UsedEpochs.ShouldBeLessThan(epochs);
        }

        [TestCase(new float[] { 1.0f, -2.0f }, -1)]
        [TestCase(new float[] { 1.0f, -2.0f }, 1)]
        [TestCase(new float[] { 10.0f, -200.0f }, -1)]
        [TestCase(new float[] { -10.0f, -200.0f }, -1)]
        public void Guess__when__trained__should__Return_RightOutput(float[] training_inputs, int training_target)
        {
            var perceptron = new Perceptron();
            int epochs = 100;
            float learningRate = 0.01f;

            perceptron.Train(training_inputs, training_target, learningRate, epochs);

            var test_inputs = new float[] { 10f, 20f };
            var test_target = 1;

            var output = perceptron.Guess(test_inputs);

            output.ShouldEqual(test_target);
        }

        [TestCase(new float[] { 10.0f, 20.0f }, -1)]
        public void Guess__when__trained_with_wrong_data__should__Return_WrongOutput(float[] training_inputs, int training_target)
        {
            var perceptron = new Perceptron();
            int epochs = 100;
            float learningRate = 0.01f;

            perceptron.Train(training_inputs, training_target, learningRate, epochs);

            var test_inputs = new float[] { 10f, 20f };
            var test_target = 1;

            var output = perceptron.Guess(test_inputs);

            output.ShouldNotEqual(test_target);
        }
    }
}
