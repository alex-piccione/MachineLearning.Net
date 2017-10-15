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
            int epochs = 10;
            float learningRate = 0.1f;

            var trainingSet = CreateTrainingSet(inputs, target);
            
            var result = perceptron.Train(trainingSet, learningRate, epochs);

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
            int epochs = 10;
            float learningRate = 0.01f;
            var trainingSet = CreateTrainingSet(training_inputs, training_target);

            perceptron.Train(trainingSet, learningRate, epochs);

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

            var trainingSet = CreateTrainingSet(training_inputs, training_target);

            perceptron.Train(trainingSet, learningRate, epochs);

            var test_inputs = new float[] { 10f, 20f };
            var test_target = 1;

            var output = perceptron.Guess(test_inputs);

            output.ShouldNotEqual(test_target);
        }


        [Test]
        public void Guess__when__trained_with_many_data__should__Return_RightOutput()
        {
            var perceptron = new Perceptron();
            int epochs = 10;
            float learningRate = 0.01f;

            var trainingSet = new List<TrainingRecord>
            {
                // Positives
                new TrainingRecord(new float[] { 1, 5 }, 1),
                new TrainingRecord(new float[] { 1, 3 }, 1),
                new TrainingRecord(new float[] { 4, 5 }, 1),
                new TrainingRecord(new float[] { -3, 0 }, 1),
                new TrainingRecord(new float[] { -1.8f, 4 }, 1),

                // Negatives
                new TrainingRecord(new float[] { 3, 2 }, -1),
                new TrainingRecord(new float[] { 3.5f, 0 }, -1),
                new TrainingRecord(new float[] { 5.5f, 1 }, -1),
                new TrainingRecord(new float[] { -1, -3 }, -1),
                new TrainingRecord(new float[] { 1.1f, -2 }, -1),
                new TrainingRecord(new float[] { 3.8f, -3 }, -1)
            };


            perceptron.Train(trainingSet, learningRate, epochs);

            TrainingRecord[] tests = {
                new TrainingRecord(new float[] { -2, 2 }, 1),
                new TrainingRecord(new float[] { -5.1f, 6.8f }, 1),
                new TrainingRecord(new float[] { -0.8f, -5 }, -1),
                new TrainingRecord(new float[] { 7.4f, 2 }, -1),
            };

            foreach (var test in tests)
            {
                var output = perceptron.Guess(test.inputs);
                output.ShouldEqual(test.output);
            }
        }

        private IEnumerable<TrainingRecord> CreateTrainingSet(float[] inputs, int target)
        {
            return new List<TrainingRecord>() { new TrainingRecord(inputs, target) };
        }
    }
}
