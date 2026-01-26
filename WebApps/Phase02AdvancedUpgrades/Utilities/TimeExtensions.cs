namespace Phase02AdvancedUpgrades.Utilities;
public static class TimeExtensions
{
    extension (double progress)
    {
        public string GetTimeString
        {
            get
            {
                TimeSpan time = TimeSpan.FromSeconds(progress);
                return time.GetTimeString;
            }
        }
    }
    extension (TimeSpan time)
    {
        public TimeSpan ApplyWithMinTotalForBatch(double multiplier, int batchSize, TimeSpan reducedBy)
        {
            if (multiplier <= 0)
            {
                throw new CustomBasicException("Time multiplier must be > 0");
            }

            if (batchSize <= 0)
            {
                throw new CustomBasicException("Batch size must be > 0");
            }

            // ----- Step 1: compute ORIGINAL batch total (no multiplier yet)
            long basePerUnitTicks = time.Ticks;
            if (basePerUnitTicks <= 0)
            {
                basePerUnitTicks = 1;
            }

            long baseBatchTotalTicks = basePerUnitTicks * (long)batchSize;

            // ----- Step 2: apply reduction ONCE per batch (still in original-time space)
            long reducedTicks = reducedBy.Ticks;
            if (reducedTicks < 0)
            {
                reducedTicks = 0;
            }

            long batchAfterReductionTicks = baseBatchTotalTicks - reducedTicks;
            if (batchAfterReductionTicks < 1)
            {
                batchAfterReductionTicks = 1;
            }

            // ----- Step 3: enforce minimum total time for the entire batch (pre-multiplier)
            long minTotalTicks = TimeSpan.FromSeconds(2).Ticks;
            if (batchAfterReductionTicks < minTotalTicks)
            {
                batchAfterReductionTicks = minTotalTicks;
            }

            // ----- Step 4: convert reduced batch back to per-unit (ceiling so total >= batchAfterReduction)
            long perUnitAfterReductionTicks =
                (long)Math.Ceiling(batchAfterReductionTicks / (double)batchSize);

            if (perUnitAfterReductionTicks <= 0)
            {
                perUnitAfterReductionTicks = 1;
            }

            // ----- Step 5: apply multiplier AFTER reduction
            long finalPerUnitTicks =
                (long)Math.Round(perUnitAfterReductionTicks * multiplier, MidpointRounding.AwayFromZero);

            if (finalPerUnitTicks <= 0)
            {
                finalPerUnitTicks = 1;
            }

            // ----- Step 6: enforce minimum TOTAL again after multiplier
            // (because multiplier could drive it below the minimum)
            long finalBatchTotalTicks = finalPerUnitTicks * (long)batchSize;
            if (finalBatchTotalTicks < minTotalTicks)
            {
                finalPerUnitTicks = (long)Math.Ceiling(minTotalTicks / (double)batchSize);
                if (finalPerUnitTicks <= 0)
                {
                    finalPerUnitTicks = 1;
                }
            }

            return TimeSpan.FromTicks(finalPerUnitTicks);
        }
        public TimeSpan Apply(double multiplier)
        {
            if (multiplier <= 0)
            {
                throw new CustomBasicException("Time multiplier must be > 0");
            }

            // preserve precision
            var ticks = (long)Math.Round(time.Ticks * multiplier, MidpointRounding.AwayFromZero);
            
            var minTicks = TimeSpan.FromSeconds(5).Ticks;

            if (ticks < minTicks)
            {
                ticks = minTicks;
            }

            return TimeSpan.FromTicks(ticks);
        }

        public string GetTimeCompact
        {
            get
            {
                if (time.TotalSeconds < 1)
                {
                    return "0s";
                }

                // Days + Hours
                if (time.TotalDays >= 1)
                {
                    return $"{time.Days}d {time.Hours}h";
                }

                // Hours + Minutes
                if (time.TotalHours >= 1)
                {
                    return $"{time.Hours}h {time.Minutes}m";
                }
                if (time.TotalMinutes >= 1)
                {
                    // Minutes + Seconds
                    return $"{time.Minutes}m {time.Seconds}s";
                }
                return $"{time.Seconds}s";
            }
        }
        public string GetTimeString
        {
            get
            {
                if (time.TotalSeconds < 1)
                {
                    return "0s";
                }

                var parts = new BasicList<string>();

                if (time.Days > 0)
                {
                    parts.Add($"{time.Days}d");
                }

                if (time.Hours > 0)
                {
                    parts.Add($"{time.Hours}h");
                }

                if (time.Minutes > 0)
                {
                    parts.Add($"{time.Minutes}m");
                }

                // Only show seconds if:
                // - there are no larger units, OR
                // - seconds > 0
                if (time.Seconds > 0 || parts.Count == 0)
                {
                    parts.Add($"{time.Seconds}s");
                }

                return string.Join(" ", parts);
            }
        }
    }
}
