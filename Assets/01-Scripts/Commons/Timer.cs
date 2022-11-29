using UnityEngine;
public enum Format
{
    D = 0,
    DD = 1
};

public enum Count
{
    Up = 0,
    Down = 1
};

public class Timer
{
    private static float pure;
    private static float mmss;
    private static float min;
    private static float s;
    private static float hundredths;
    private static bool countRunning;

    public static void StartCount(float initialCount)
    {
        InitialCount = initialCount;
        countRunning = true;
    }

    public static void StopCount()
    {
        countRunning = false;
        pure = 0f;
        mmss = 0f;
        min = 0f;
        s = 0f;
        hundredths = 0f;
    }

    public static string CountMinutsAndSeconds(Count count)
    {
        switch (count)
        {
            case Count.Up:
                if (countRunning)
                {
                    mmss += Time.deltaTime;
                    pure += Time.deltaTime;
                }
                break;
            case Count.Down:
                if (countRunning && mmss > 0.1f)
                {
                    mmss -= Time.deltaTime;
                    pure -= Time.deltaTime;
                }
                else
                {
                    StopCount();
                    mmss = 0f;
                }
                break;
        }

        return string.Format("{0:00}", mmss / 60f) + ":" + string.Format("{0:00}", mmss % 60f);
    }

    public static string CountMinutes(Count count, Format format)
    {
        switch (count)
        {
            case Count.Up:
                if (countRunning)
                {
                    min += Time.deltaTime;
                    pure += Time.deltaTime;
                }
                break;
            case Count.Down:
                if (countRunning && min > 0.1f)
                {
                    min -= Time.deltaTime;
                    pure -= Time.deltaTime;
                }
                else
                {
                    StopCount();
                    min = 0f;
                }
                break;
        }

        return format switch
        {
            Format.D => Mathf.FloorToInt((min) / 60f).ToString(),
            Format.DD => string.Format("{0:00}", Mathf.FloorToInt((min) / 60f)),
            _ => "mm"
        };
    }

    public static string CountSeconds(Count count, Format format)
    {
        switch (count)
        {
            case Count.Up:
                if (countRunning)
                {
                    s += Time.deltaTime;
                    pure += Time.deltaTime;
                }
                break;
            case Count.Down:
                if (countRunning && s > 0.1f)
                {
                    s -= Time.deltaTime;
                    pure -= Time.deltaTime;
                }
                else
                {
                    StopCount();
                    s = 0f;
                }
                break;
        }

        return format switch
        {
            Format.D => Mathf.FloorToInt((s) % 60f).ToString(),
            Format.DD => string.Format("{0:00}", Mathf.FloorToInt((s) % 60f)),
            _ => "ss"
        };
    }

    public static string CountHundredths(Count count, Format format)
    {
        switch (count)
        {
            case Count.Up:
                if (countRunning)
                {
                    hundredths += Time.deltaTime;
                    pure += Time.deltaTime;
                }
                break;
            case Count.Down:
                if (countRunning && hundredths > 0.1f)
                {
                    hundredths -= Time.deltaTime;
                    pure -= Time.deltaTime;
                }
                else
                {
                    StopCount();
                    hundredths = 0f;
                }
                break;
        }

        return format switch
        {
            Format.D => Mathf.FloorToInt((hundredths) * 100f % 100f).ToString(),
            Format.DD => string.Format("{0:00}", Mathf.FloorToInt((hundredths) * 100f % 100f)),
            _ => "hh"
        };
    }

    private static float InitialCount
    {
        set
        {
            if (countRunning)
            {
                return;
            }
            else
            {
                pure = value;
                mmss = value;
                min = value;
                s = value;
                hundredths = value;
            }
        }
    }

    public static bool CountIsRunning => countRunning;
    public static float TimePureCount => pure;
}
