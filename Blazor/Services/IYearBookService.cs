using Blazor.Entities;

namespace Blazor.Services;

public interface IYearBookService
{
    Task<YearBookEntry> AddYearBookEntryAsync(YearBookEntry yearBookEntry);
    Task<YearBookEntry?> GetYearBookEntryByIdAsync(int id);
    Task<IEnumerable<YearBookEntry>> GetAllYearBookEntriesAsync();
    Task UpdateYearBookEntryAsync(int id, YearBookEntry yearBookEntry);
    Task DeleteYearBookEntryAsync(int id);
}