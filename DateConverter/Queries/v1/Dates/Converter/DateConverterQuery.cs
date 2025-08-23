using MediatR;

namespace DateConverter.Domain.Queries.v1.Dates.Converter;

public sealed record DateConverterQuery : IRequest<DateConverterQueryResponse>
{
    public int Day { get; set; }
}
