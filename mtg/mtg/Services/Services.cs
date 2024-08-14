using Microsoft.EntityFrameworkCore;
using mtg.Models;
namespace mtg.Services
{
    public class Services
    {
        readonly MyDBContext dBContext = new();

        public IQueryable<string> GetDistinctColors()
        {
            IQueryable<string> distinctColors = dBContext.Colors.Select(c => c.Name).Distinct();
            return distinctColors;
        }

        public IQueryable<CardModel> GetCardsByColor(string color)
        {
            IQueryable<CardModel> cards = dBContext.CardColors
                .Include(c => c.Color)
                .Where(c => !string.IsNullOrEmpty(c.Card.Image))
                .Where(w => w.CardId >= 1000 && w.CardId < 1100)
                .Where(c => c.Color.Name == color)
                .Select(p => new CardModel
                {
                    Id = p.CardId,
                    Name = p.Card.Name,
                    Image = p.Card.OriginalImageUrl,
                    Color = p.Color.Name
                })
                .OrderBy(o => o.Name).Distinct();
                

            return cards;
        }

        public IQueryable<CardModel> GetCards()
        {
            IQueryable<CardModel> cards = dBContext.CardColors
                .Where(c => !string.IsNullOrEmpty(c.Card.Image))
                .Where(w => w.CardId >= 1000 && w.CardId < 1100)
                .Select(p => new CardModel
                {
                    Id = p.CardId,
                    Name = p.Card.Name,
                    Image = p.Card.OriginalImageUrl,
                    Color = p.Color.Name
                })
                .Distinct();

            return cards;
        }

        public CardModel? GetRandomCard()
        {
            var cardsWithImage = dBContext.Cards.Where(c => !string.IsNullOrEmpty(c.Image)).ToList();

            if (cardsWithImage.Count == 0)
            {
                return null;
            }

            Random random = new();
            int randomIndex = random.Next(0, cardsWithImage.Count);

            var randomCard = cardsWithImage[randomIndex];

            CardModel cardModel = new()
            {
                Id = randomCard.Id,
                Name = randomCard.Name,
                Image = randomCard.OriginalImageUrl,
                ManaCost = randomCard.ManaCost,
                Type = randomCard.Type,
                Rarity = randomCard.RarityCode,
                
                
            };

            return cardModel;
        }

        public class CardModel
        {
            public long? Id { get; init; }
            public string? Name { get; init; }
            public string? Image { get; init; }
            public string? Color { get; init; }
            public string? ManaCost { get; init; }
            public string? Type { get; init; }
            public string? Rarity { get; init; }
            public string? Artist { get; init; }
        }
    }
}
