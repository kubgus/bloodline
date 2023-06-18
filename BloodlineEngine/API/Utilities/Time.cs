﻿using System.Diagnostics;
using System.Timers;

namespace BloodlineEngine
{
    public static class Time
    {
        public static float ElapsedMilliseconds => m_Stopwatch.ElapsedMilliseconds;
        public static float ElapsedSeconds => (float)m_Stopwatch.Elapsed.TotalSeconds;
        public static float ElapsedTicks { get; private set; } = 0;
        public static float ElapsedFrames { get; private set; } = 0;

        public static float DeltaTime => GetDeltaTimeMilliseconds() / 1000f;
        public static float FixedDeltaTime { get; private set; } = (float)m_TickInterval / 1000f;
        private static double m_TickInterval = 20;

        public static float GetDeltaTimeMilliseconds()
        {
            float deltaTime = ElapsedMilliseconds - m_LastFrameTime;
            m_LastFrameTime = ElapsedMilliseconds;
            return deltaTime;
        }

        public static event EventHandler? FixedUpdate;

        private static Stopwatch m_Stopwatch = new();
        private static System.Timers.Timer m_Timer = new(m_TickInterval); // Tick interval of 16 milliseconds (60FPS)
        private static float m_LastFrameTime;

        public static void ResetTime () { m_Stopwatch.Reset(); m_Stopwatch.Start(); m_Timer.Elapsed += Tick; m_Timer.Start(); }

        private static void Tick (object? sender, ElapsedEventArgs e) { ElapsedTicks++; FixedUpdate?.Invoke(null, EventArgs.Empty); }
        public static void BLNextFrame() { ElapsedFrames++; }
    }
}
