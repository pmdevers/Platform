﻿
namespace FinSecure.Platform.Common.Helpers;
internal static class DebuggerHelpers
{
    public static string GetDebugText(string key, object value)
        => $"'{key}' - {value}";

}
