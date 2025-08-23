namespace DateConverter.Domain.Models;

public class DateFactory
{
    private readonly static DateTime _dateNow = DateTime.Now;
    private readonly static int _dayOne = 1;

    public int Day { get; set; }

    public static DateFactory Create(int day)
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

    public DateTime GetDateWithTurnOfMounth()
    {
        return new DateTime(year: _dateNow.Year, month: _dateNow.Month - 1, day: Day);
    }

    public DateTime GetDate()
    {
        return new DateTime(year: _dateNow.Year, month: _dateNow.Month, day: Day);
    }
}