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
        public const double responseCurve = 1;
        public const double bias = -1;
        public const double crossoverRate = 0.7;
        public const double mutationRate = 0.3;
        public const int numberOfFittestCopies = 5;
        public const int numberOfWolves = 4;
        public const int numberOfSheep = 1;
        public const int numberOfUpdates = 2000;
        public const double maxRotation = 0.1;
        public const double maxMoveSpeed = 2;
        public const int minDistanceToTouch = 20;

        public static int worldWidth;
        public static int worldHeight;

        public static Random random;

        //these are values for the creatures
        public const double startingHunger = 5;
        public const int eatingAddition = 1;
        public const double starvingSubtract =.1;

        //these are values for the weights that determin the fitness function
        public const double hungerWeight = .5;
        public const double numberOfWolvesWeight = .2;
        public const double closestWolfWeight = .3;

        //these are for size of vision
        public const int predatorVisionWidth = 40;
        public const int predatorVisionHeight = 50;
        public const int predatorVisionOffset = predatorVisionHeight/2;
        public const int preyVisionWidth = 50;
        public const int preyVisionHeight = 50;
        public const int preyMaxVisionDist = 25;

        //Duration of eating
        public const int eatTime = 20;

        //constants for rules

        // avoidance
        public const int avoid_numOfHiddenLayers = 1;
        public const int avoid_numOfNeuronsPerLayer = 10;

    }
}