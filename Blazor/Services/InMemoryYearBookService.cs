using Blazor.Entities;

namespace Blazor.Services;

public class InMemoryYearBookService : IYearBookService
{
    private List<YearBookEntry> yearBookEntries;

    public InMemoryYearBookService()
    {
        yearBookEntries = new()
        {
            new YearBookEntry()
            {
                Id = 1,
                Name = "Jakob Doe",
                Pronouns = "He/Him",
                FunFact = "I have climbed Mount Everest.",
                ImageUrl = "https://img.freepik.com/premium-vector/male-icon-symbol-logo-vector_636116-413.jpg",
                StartingYear = 2020
            },
            new YearBookEntry()
            {
                Id = 2,
                Name = "Susan Smith",
                Pronouns = "She/Her",
                FunFact = "I can speak 5 languages.",
                ImageUrl = "https://techzide-web.web.app/img/testimonials/t7.jpeg",
                StartingYear = 2019
            },
            new YearBookEntry()
            {
                Id = 3,
                Name = "Jenna Johnson",
                Pronouns = "They/Them",
                FunFact = "I have a collection of over 1000 comic books.",
                ImageUrl = "https://techzide-web.web.app/img/testimonials/t7.jpeg",
                StartingYear = 2021
            },
            new YearBookEntry()
            {
                Id = 4,
                Name = "Emily Davis",
                Pronouns = "She/Her",
                FunFact = "I once swam across the English Channel.",
                ImageUrl = "https://techzide-web.web.app/img/testimonials/t7.jpeg",
                StartingYear = 2018
            },
            new YearBookEntry()
            {
                Id = 5,
                Name = "Troels Brown",
                Pronouns = "He/Him",
                FunFact = "I have a black belt in karate.",
                ImageUrl = "https://img.freepik.com/premium-vector/male-icon-symbol-logo-vector_636116-413.jpg",
                StartingYear = 2022
            }
        };
    }

    public async Task<YearBookEntry> AddYearBookEntryAsync(YearBookEntry yearBookEntry)
    {
        yearBookEntry.Id = yearBookEntries.Any() ? yearBookEntries.Max(e => e.Id) + 1 : 1;
        yearBookEntries.Add(yearBookEntry);
        return await Task.FromResult(yearBookEntry);
    }

    public async Task<YearBookEntry?> GetYearBookEntryByIdAsync(int id)
    {
        var yearBookEntry = yearBookEntries.FirstOrDefault(y => y.Id == id);
        return await Task.FromResult(yearBookEntry);
    }

    public async Task<IEnumerable<YearBookEntry>> GetAllYearBookEntriesAsync()
    {
        return await Task.FromResult(yearBookEntries.OrderBy(e => e.StartingYear).AsEnumerable());
    }

    public async Task UpdateYearBookEntryAsync(int id, YearBookEntry yearBookEntry)
    {
        var existingEntry = yearBookEntries.FirstOrDefault(e => e.Id == id);
        if (existingEntry != null)
        {
            var index = yearBookEntries.IndexOf(existingEntry);
            yearBookEntries[index] = yearBookEntry;
        }
        await Task.CompletedTask;
    }

    public async Task DeleteYearBookEntryAsync(int id)
    {
        var entry = yearBookEntries.FirstOrDefault(e => e.Id == id);
        if (entry != null)
        {
            yearBookEntries.Remove(entry);
        }
        await Task.CompletedTask;
    }
}