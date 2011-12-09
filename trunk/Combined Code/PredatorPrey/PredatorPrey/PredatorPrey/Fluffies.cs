﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PredatorPrey
{
    class Fluffies : Creature
    {
        private AvoidanceRule avoid;
        private SteeringRule steer;
        private AlignmentRule align;
        private GoalRule goal;
        private Vector2 currentGoal;

        public Fluffies(Vector2 position) : base(position)
        {
            brain = new NeuralNetwork(Parameters.preyNumberOfRules * Parameters.inputsPerSensedObject, 3,
                Parameters.behav_numOfHiddenLayers, Parameters.behav_numOfNeuronsPerLayer);

            avoid = new AvoidanceRule(Classification.Unknown);
            steer = new SteeringRule(Classification.Prey);
            align = new AlignmentRule(Classification.Prey);
            goal = new GoalRule();
            currentGoal = new Vector2(500, 500);
            good = false;
            score = 0;
        }

        public override void wrap(VisionContainer vc, AudioContainer ac)
        {
            //step1: update values that change with time (hunger)

            if (eating)
            {
                eat();
                if (hunger <= 0)
                {
                    eating = false;
                    canEat = false;
                    dontEatDuration = Parameters.dontEatCount;
                }
                else
                    hunger--;
            }
            else
                starve();

            // Stop the creature (fluffies or wulffies)
            if (eatDuration > 0)
            {
                eatDuration--;
                if (eatDuration == 0)
                {
                    eating = false;
                    canEat = false;
                    dontEatDuration = Parameters.dontEatCount;
                }
            }

            if (dontEatDuration > 0)
            {
                dontEatDuration--;
                if (dontEatDuration == 0)
                {
                    canEat = true;
                }
            }
            

            // update the score
            if (isAlive)
                score++;

            //step2: use prey rules (extract data from VisionContainer) to create a list of movement vectors
            List<Vector2> ruleVectors = new List<Vector2>(Parameters.preyNumberOfRules);
            for (int i = 0; i < vc.size(); i++)
            {
                if (vc.getSeenObject(i).type.Equals(Classification.Food))
                {
                    currentGoal = vc.getSeenObject(i).position;
                }
            }

            ruleVectors.Add(avoid.run(vc, ac));
            ruleVectors.Add(steer.run(vc));
            ruleVectors.Add(align.run(vc));
            ruleVectors.Add(goal.run(Vector2.Multiply(Vector2.Subtract(currentGoal, position), 0), (float)hunger));

            //step3: pass vectors into neural network to get outputs
            List<double> inputs = new List<double>(Parameters.preyNumberOfRules * Parameters.inputsPerSensedObject);
            for (int i = 0; i < ruleVectors.Count; i++)
            {
                Vector2 pos = ruleVectors[i];

                inputs.Add(pos.X);
                inputs.Add(pos.Y);
            }

            List<double> outputs = brain.run(inputs);

            //step4: update velocity, position, and direction
            Vector2 acceleration = new Vector2((float) outputs[0], (float) outputs[1]);
            if (acceleration.Length() != 0)
                acceleration = Vector2.Normalize(acceleration);
            acceleration = Vector2.Clamp(acceleration, new Vector2(-Parameters.accel_clampVal, -Parameters.accel_clampVal),
                new Vector2(Parameters.accel_clampVal, Parameters.accel_clampVal));
            acceleration = acceleration * Parameters.maxAcceleration;
            if(!eating || (Math.Max(acceleration.X,acceleration.Y)>Parameters.eatingThreshold || Math.Min(acceleration.X,acceleration.Y)<-Parameters.eatingThreshold))
            {
                if (eating)
                {
                    eating = false;
                    canEat = false;
                    dontEatDuration = Parameters.dontEatCount;
                }
                velocity = acceleration + velocity;

                if (velocity.Length() > Parameters.maxMoveSpeed)
                {
                    velocity = Vector2.Normalize(velocity) * Parameters.maxMoveSpeed;
                }

                position = position + velocity;

                if (velocity != Vector2.Zero)
                {
                    Vector2 direction = Vector2.Normalize(velocity);
                    rotation = Math.Atan2(direction.Y, direction.X) - Math.PI/2;
                }
            }

            base.wrap(vc, ac);
        }

        public void updateWeights()
        {
            // step1: calculate fitness of current state

            // step2: classify state as either good or not good

            // step3: if state is good do nothing
            //        if state is not good, attribute the bad state to 1 (or more) of the rules
            //              ???????
        }

        //this is just my first idea, feel free to change it if you think of something
        //also the weights are just temporary values right now
        public override int calculateFitness(VisionContainer vc)
        {
            int numberOfWolves = 0;
            int closestWolf = int.MaxValue;
            int numberOfFood = 0;
            int closestFood = int.MaxValue;

            for (int i = 0; i < vc.size(); i++)
            {
                if (vc.getSeenObject(i).type == Classification.Predator)
                {
                    numberOfWolves++;
                    int distance = (int)Vector2.Subtract(vc.getSeenObject(i).position, position).Length();

                    if (distance < closestWolf)
                        closestWolf = distance;
                }
                else if (vc.getSeenObject(i).type == Classification.Food)
                {
                    numberOfFood++;
                    int distance = (int)Vector2.Subtract(vc.getSeenObject(i).position, position).Length();

                    if (distance < closestFood)
                        closestFood = distance;
                }
            }

            fitness = (int)(Parameters.initFitness + numberOfWolves * Parameters.numberOfWolvesWeight + 
                        hunger * Parameters.hungerWeight + Parameters.numberOfFoodWeight * numberOfFood);

            if (closestWolf > Parameters.closestWolfDist)
            {
                if (closestWolf > 2 * Parameters.closestWolfDist)
                    fitness += Parameters.closestWolfWeight;
                else
                    fitness += Parameters.closestWolfWeight - closestWolf;
            }
            else
            {
                fitness -= Parameters.closestWolfWeight - closestWolf;
            }

            if (closestFood < Parameters.closestFoodMaxPenalty)
            {
                fitness += Parameters.closestFoodWeight * closestFood;
            }
            else
            {
                fitness -= Parameters.closestFoodMaxPenalty;
            }

            if (fitness < Parameters.minFitness)
                fitness = Parameters.minFitness;
            else if (fitness > Parameters.maxFitness)
                fitness = Parameters.maxFitness;

            return fitness;
        }

        public override void  eat()
        {
            eatDuration = Parameters.fluffieEatTime;
 	        base.eat();
        }

        public override void die()
        {
            score = (int)Math.Max(0,score- hunger);
            base.die();
        }
    }
}
