using System;
using System.Collections.Generic;
using System.Linq;

namespace ENCollect.Dyna.Examples;

public static class Demo1_StaticLinqExample
{
    public static void Run()
    {
        Console.WriteLine("== Demo 1: Static (Hard-Coded) LINQ ==");

        var recommenders = new List<Recommender>
        {
            new Recommender { Branch = "BLR101", Region = "Karnataka", ThresholdRecommendationAmount = 100 },
            new Recommender { Branch = "MUM999", Region = "Maharashtra", ThresholdRecommendationAmount = 150 },
            new Recommender { Branch = "NDL450", Region = "Delhi", ThresholdRecommendationAmount = 200 }
        };

        Console.WriteLine("We do: (Region == 'Karnataka') AND (ThresholdRecommendationAmount >= 120)");
        var results = recommenders
            .Where(r => r.Region == "Karnataka" && r.ThresholdRecommendationAmount >= 120)
            .ToList();

        Console.WriteLine($"Matched {results.Count} recommenders:");
        foreach (var r in results)
        {
            Console.WriteLine($"  Branch={r.Branch}, Region={r.Region}, Threshold={r.ThresholdRecommendationAmount}");
        }

        Console.WriteLine();
    }
}