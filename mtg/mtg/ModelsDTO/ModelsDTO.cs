using System;
using System.Collections.Generic;

namespace mtg.ModelsDTO;
{
	public record CardModel(long IdCard, string ImageInfo, string CardName, string ArtistName) {}
}