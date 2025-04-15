using System;

public class TimeConvertion
{
    public static DateTime ConvertUnixTimestampToDateTime(long timestamp)
    {
        // Unix timestamp is seconds since 1970-01-01 00:00:00 UTC
        DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(timestamp);
        return dateTimeOffset.LocalDateTime; // Converts to local time
    }
}