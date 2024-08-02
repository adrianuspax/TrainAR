using UnityEngine;

namespace TreinAR
{
    public enum Format
    {
        D = 0,
        DD = 1
    };

    public enum Count
    {
        up = 1,
        down = -1
    };

    public class Timer
    {
        private static float pure;
        private static float mm, ss;
        private static float min;
        private static float sec;
        private static float hun;
        private static bool isRunning;

        public static void Start(float initialCount)
        {
            InitialCount = initialCount;
            isRunning = true;
        }

        public static void Stop()
        {
            isRunning = false;
            pure = 0f;
        }

        public static string MMSS(Count count)
        {
            if (isRunning)
            {
                pure += ((int)count) * Time.deltaTime;
                mm = pure;
                ss = pure;

                isRunning = (count == Count.up) || (pure > 0f);
            }
            else if (count == Count.down)
            {
                mm = 0f;
                ss = 0f;
            }

            return string.Format("{0:00}", Mathf.FloorToInt(mm / 60f)) + ":" + string.Format("{0:00}", Mathf.FloorToInt(ss % 60f));
        }

        public static string Minutes(Count count, Format format)
        {
            if (isRunning)
            {
                pure += ((int)count) * Time.deltaTime;
                min = pure;

                isRunning = (count == Count.up) || (pure > 0f);
            }
            else if (count == Count.down)
            {
                min = 0f;
            }

            return format switch
            {
                Format.D => Mathf.FloorToInt((min) / 60f).ToString(),
                Format.DD => string.Format("{0:00}", Mathf.FloorToInt((min) / 60f)),
                _ => "mm"
            };
        }

        public static string Seconds(Count count, Format format)
        {
            if (isRunning)
            {
                pure += ((int)count) * Time.deltaTime;
                sec = pure;

                isRunning = (count == Count.up) || (pure > 0f);
            }
            else if (count == Count.down)
            {
                sec = 0f;
            }

            return format switch
            {
                Format.D => Mathf.FloorToInt((sec) % 60f).ToString(),
                Format.DD => string.Format("{0:00}", Mathf.FloorToInt((sec) % 60f)),
                _ => "ss"
            };
        }

        public static string Hundredths(Count count, Format format)
        {
            if (isRunning)
            {
                pure += ((int)count) * Time.deltaTime;
                hun = pure;

                isRunning = (count == Count.up) || (pure > 0f);
            }
            else if (count == Count.down)
            {
                hun = 0f;
            }

            return format switch
            {
                Format.D => Mathf.FloorToInt((hun) * 100f % 100f).ToString(),
                Format.DD => string.Format("{0:00}", Mathf.FloorToInt((hun) * 100f % 100f)),
                _ => "hh"
            };
        }

        private static float InitialCount
        {
            set
            {
                if (isRunning)
                {
                    return;
                }
                else
                {
                    pure = value;
                }
            }
        }

        public static bool IsRunning => isRunning;
        public static float TimePureCount => pure;
    }
}