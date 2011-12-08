﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PredatorPrey
{
    static class Parameters
    {
        public const int weightRange = 1;
        public const int numberOfInputs = 4;
        public const int numberOfOutputs = 2;
        public const int numberOfHiddenLayers = 0;
        public const int numberOfNeuronsPerHiddenLayer = 6;
        public const float responseCurve = 1F;
        public const float bias = -1F;
        public const float crossoverRate = 0.7F;
        public const float mutationRate = 0.3F;
        public const int numberOfFittestCopies = 5;

        public const int numberOfWolves = 0;
        public const int numberOfSheep = 30;
        public const int numberOfUpdates = 200;

        public const float maxRotation = 0.1F;
        public const float maxMoveSpeed = 2F;
        public const float maxAcceleration = .25F;
        public const int minDistanceToTouch = 20;

        public const int k = 1;
        public const int fluffiesScore = 1;
        public const int wulffiesScore = 1;

        public static int worldWidth;
        public static int worldHeight;

        public static Random random;

        //these are values for the creatures
        public const float startingHunger = 5F;
        public const float maxHunger = 20F;
        public const int eating = -1;
        public const float starving =.1F;

        //these are values for the weights that determin the fitness function
        public const int hungerWeight = -10;
        public const int numberOfWolvesWeight = -3;
        public const int closestWolfDist = 50;
        public const int closestWolfWeight = 50;
        public const int numberOfFoodWeight = 3;
        public const int closestFoodWeight = -1;
        public const int closestFoodMaxPenalty = 50;
        public const int numberOfSheepWeight = 3;
        public const int closestSheepWeight = -1;
        public const int closestSheepMaxPenalty = 50;
        public const int maxFitness = 300;
        public const int initFitness = 100;
        public const int minFitness = 0;

        //these are for size of vision
        //these were used for object detection
        /*
        public const int predatorVisionWidth = 30;
        public const int predatorVisionHeight = 30;
        public const int predatorVisionOffset = predatorVisionHeight/2;
        public const int preyVisionWidth = 30;
        public const int preyVisionHeight = 30;
        public const int preyMaxVisionDist = 25;
        */
        public const int wulffiesVisionThreashold = 200;
        public const int fluffiesVisionThreashold = 200;

        public const int boxNum1 = 10;
        public const int boxNum2 = 10;
        public const double histThreshold = 3;

        //Duration of eating
        public const int eatTime = 20;

        //constants for rules
        public const int preyNumberOfRules = 4;
        public const int predatorNumberOfRules = 3;
        public const int inputsPerSensedObject = 2;
        public const int maxVisionInput = 15 * inputsPerSensedObject;
        public const int maxHearInput = 15 * inputsPerSensedObject;

        //behavior selection
        public const int behav_numOfHiddenLayers = 1;
        public const int behav_numOfNeuronsPerLayer = 5;

        // avoidance
        public const int avoid_numOfHiddenLayers = 0;
        public const int avoid_numOfNeuronsPerLayer = 2;

        // steering
        public const int steer_numOfHiddenLayers = 0;
        public const int steer_numOfNeuronsPerLayer = 2;

        // alignment
        public const int align_numOfHiddenLayers = 0;
        public const int align_numOfNeuronsPerLayer = 2;

        // goal
        public const int goal_numberOfExtraInputs = 1;
        public const int goal_numOfHiddenLayers = 0;
        public const int goal_numOfNeuronsPerLayer = 2;

        // steering modification
        public const float accel_clampVal = 0.5F;//0.015F;
    }
}
