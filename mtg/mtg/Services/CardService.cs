using Microsoft.EntityFrameworkCore;
using mtg.Models;
using mtg.Services;

public class CardService
{
    private readonly MyDBContext _context;

    // Constructor injection of the DbContext
    public CardService(MyDBContext context)
    {
        _context = context;
    }
   

    public List<CardModel> GetCards(string colorFilter = "", int pageNumber = 1, int pageSize = 100)
    {
        var query = _context.Cards.AsQueryable();

        if (!string.IsNullOrEmpty(colorFilter))
        {
            query = query.Where(c => c.Color.Contains(colorFilter));
        }

        // Skip and Take for pagination
        return query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();
    }

    public int GetTotalCardCount(string colorFilter = "")
    {
        var query = _context.Cards.AsQueryable();

        if (!string.IsNullOrEmpty(colorFilter))
        {
            query = query.Where(c => c.Color.Contains(colorFilter));
        }

        return query.Count();
    }
}
