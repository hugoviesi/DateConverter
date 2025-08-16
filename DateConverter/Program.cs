var _dayOne = 1;
var _dateNow = DateTime.Now;

Console.WriteLine("Enter the day: ");

var input = Console.ReadLine();

if (!int.TryParse(input, out var day))
{
    Console.WriteLine($"Incorrect value: {day}");

    Console.ReadKey();
}

var date = IsTurnedMonth(day)
    ? GetDateWithTurnOfMounth(day)
    : GetDate(day);

Console.WriteLine($"Date: {date}");

Console.ReadKey();

bool IsTurnedMonth(int day)
{
    return day > _dateNow.Day && _dateNow.Day == _dayOne;
}

DateTime GetDateWithTurnOfMounth(int day)
{
    return new DateTime(year: _dateNow.Year, month: _dateNow.Month - 1, day: day);
}

DateTime GetDate(int day)
{
    return new DateTime(year: _dateNow.Year, month: _dateNow.Month, day: day);
}