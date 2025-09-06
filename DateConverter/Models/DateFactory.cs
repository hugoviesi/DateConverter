namespace DateConverter.Domain.Models;

public class DateFactory
{
    private readonly static DateTime _dateNow;
    private readonly static int _dayOne = 1;
    private readonly static int _monthOne = 1;
    private readonly static int _monthTwelve = 12;

    public int Day { get; set; }

    public static DateFactory WithDay(int day)
    {
        return new DateFactory
        {
            Day = day
        };
    }

    public bool IsTurnedMonth()
    {
        return Day > _dateNow.Day && _dateNow.Day == _dayOne;
    }

    public bool IsTurnedYear()
    {
        return IsTurnedMonth() && _dateNow.Month == _monthOne;
    }

    public DateTime GetDateWithTurnOfMounth()
    {
        return new DateTime(year: _dateNow.Year, month: _dateNow.Month - 1, day: Day);
    }

    public DateTime GetDateWithTurnOfYear()
    {
        return new DateTime(year: _dateNow.Year - 1, month: _monthTwelve, day: Day);
    }

    public DateTime GetDate()
    {
        return new DateTime(year: _dateNow.Year, month: _dateNow.Month, day: Day);
    }

    public DateTime GetCalculatedDate()
    {
        if (IsTurnedYear())
        {
            return GetDateWithTurnOfYear();
        }

        if (IsTurnedMonth())
        {
            return GetDateWithTurnOfMounth();
        }

        return GetDate();
    }
}