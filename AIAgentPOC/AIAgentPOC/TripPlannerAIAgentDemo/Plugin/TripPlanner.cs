﻿using Microsoft.SemanticKernel;
using System.ComponentModel;

namespace AIAgentPOC.TripPlannerAIAgentDemo.Plugin
{
    public class TripPlanner
    {
        [KernelFunction]
        [Description("Returns back the required steps necessary to plan a one day travel to a destination by an electric car.")]
        [return: Description("The list of steps needed to plan a one day travel by an electric car.")]
        public async Task<string> GenerateRequiredStepsAsync(
            Kernel kernel,
            [Description("A 2-3 sentence description of where is a good place to go to today")] string destination,
            [Description("The time of the day to start the trip")] string timeOfDay)
        {
            var result = await kernel.InvokePromptAsync(
                $"""
                I'm going to plan a short one day vacation to {destination}. I would like to start around {timeOfDay}.
                Before I do that, can you succinctly recommend the top 2 steps I should take in a numbered list?
                I want to make sure I don't forget to pack anything for the weather at my destination and my car is sufficiently charged before I start the journey.
                """, new() {
                {"destination" , destination},
                {"timeofday", timeOfDay},
                });

            return result.ToString();
        }
    }
}