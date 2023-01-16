using System;

namespace cardgame 
{
	class Program 
	{
		static void Main(string[] args)
		{
			Deck deck1 = new Deck("Deck 1 ");
			Card card1 = new Card(2, 6);
			Console.WriteLine(card1.Face + card1.Suit);
			deck1.generateStandardDeck();
			deck1.shuffleDeck();
			Deck deck2 = new Deck("Deck 2");
			deck1.MoveCardFromDeck(deck2, card1);
			Console.WriteLine(deck1.CheckIfDeckContains(card1));
			Console.WriteLine(deck1.deckOfCards.Count());
			foreach (Card card in deck2.deckOfCards) 
			{
				Console.WriteLine(card.Face + card.Suit);
			}
			Console.WriteLine(deck2.deckOfCards.Count());
		}
	}
	
	
	/// Card class
	
	class Card 
	{
		public string Face;
		public char Suit;
		public static readonly char[] StandardSuits = {'D', 'H', 'S', 'C'};
		public static readonly string[] StandardFaces = {"A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K"};
		
		public Card(int SuitIndex, int FaceIndex)
		{
			this.Suit = StandardSuits[SuitIndex];
			this.Face = StandardFaces[FaceIndex];
		}
		
		public Card() 
		{
			this.Suit = 'J';
			this.Face = "J";
		}
	}
	
	class Deck 
	{
		public List<Card> deckOfCards = new List<Card>();
		public string name; 
		public Card Joker = new Card();
		public static readonly char[] StandardSuits = {'D', 'H', 'S', 'C'};
		public static readonly string[] StandardFaces = {"A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K"};
		
		public Deck(string name) 
		{
			this.name = name;
		}
		
		public void generateStandardDeck() 
		{
			for (int i=0; i<13; i++) 
			{ 
				for (int z=0; z<4; z++) 
				{
					Card new_card = new Card(z, i);
					deckOfCards.Add(new_card);
				}
			}
		}
		
		public void shuffleDeck()
		{
			for(int i=0; i<=10000; i++) 
			{
				Random randint = new Random();
				int randomDeckIndex = randint.Next(0, deckOfCards.Count());
				int randomDeckIndex2 = randint.Next(0, deckOfCards.Count());
				Card temp = deckOfCards[randomDeckIndex];
				deckOfCards[randomDeckIndex] = deckOfCards[randomDeckIndex2];
				deckOfCards[randomDeckIndex2] = temp;
			}
		}
		
		public bool AddCard(Card card2add)
		{
			Tuple<bool, Card> result = CheckIfDeckContains(card2add);
			if (result.Item1 == true)
			{
				return false;
			}
			else
			{
				deckOfCards.Add(result.Item2);
				return true;	
			}
		}
		
		public bool removeCard(Card card2remove)
		{
			Tuple<bool, Card> result = CheckIfDeckContains(card2remove);
			if (result.Item1 == true)
			{
				deckOfCards.Remove(result.Item2);
				return true;
			}
			else 
			{
				return false;
			}
		}
		
		public Tuple<bool, Card> CheckIfDeckContains(Card card2bechecked)
		{
			foreach (Card card in deckOfCards)
			{
				if ((card.Face, card.Suit) == (card2bechecked.Face, card2bechecked.Suit))
				{
					return Tuple.Create(true, card);
				}
			}
			return Tuple.Create(false, card2bechecked);
		}
		
		public bool MoveCardFromDeck(Deck inputDeck, Card card2move)
		{
			Tuple<bool, Card> result = CheckIfDeckContains(card2move);
			if (result.Item1 == true) 
			{
				if (inputDeck.AddCard(card2move)) 
				{
					removeCard(card2move);
					return true;
				}
				else
				{
					return false;
				}
			}
			else
			{
				return false;
			}

		}
		public void getJoker() 
		{
			Random randint = new Random();
			int jokerIndex = randint.Next(0, 13);
			Joker.Face = StandardFaces[jokerIndex];
		}
		
	}
	
	class Hand : Deck
	{
		public Deck mainDeck;
		public Deck ThrowPile = new Deck("ThrowPile");
		public bool turnPICK;
		public bool turnTHROW;
		public List<Tuple<Card, Card>> Pairs;		
		public Hand(string name, Deck mainDeck, Deck ThrowPile) :base(name)
		{
			this.mainDeck = mainDeck;
			this.ThrowPile = ThrowPile;
			this.name = name;
			this.turnPICK = false;
			this.turnTHROW = false;
		}
		
		public void pickCardFromDeckIndex(int index)
		{
			if (turnPICK == true)
			{
				switch (index) 
				{
					case 1: 
					{
						if (mainDeck.deckOfCards.Count() > 0) 
						{
							Card cardOnTop = mainDeck.deckOfCards[^1];
							if (mainDeck.removeCard(cardOnTop))
							{
								deckOfCards.Add(cardOnTop);
								turnPICK = false;
								break;
							}
						}	
						break;
					}
					
					case 2:
					{
						if (ThrowPile.deckOfCards.Count() > 0)
						{
							Card cardOnTop = ThrowPile.deckOfCards[^1];
							if (ThrowPile.removeCard(cardOnTop))
							{
								deckOfCards.Add(cardOnTop);
								turnPICK = false;
								break;
							}
						}
						break;
					}
				
				}
			}
		}
		public void ThrowCard(int CardPosition)
		{
			if (turnTHROW == true)
			{
				Card card2throw = deckOfCards[CardPosition-1];
				MoveCardFromDeck(ThrowPile, card2throw);
				turnTHROW = false;
			}
		}
		
		public void CalculatePair(Card card1, Card card2)
		{
			
		}
		
	}
}