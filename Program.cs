using System;

namespace cardgame 
{
	class Program 
	{
		static void Main(string[] args)
		{
			Deck deck1 = new Deck("Deck 1 ");
			Card card1 = new Card(1, 0);
			Deck throwpile = new Deck("throwpile");
			
			Console.WriteLine("--------------- CARD1 DETAILS ------------------------");
			Console.WriteLine(card1.Face + card1.Suit);
			Console.WriteLine("---------------------------------------");
			deck1.generateStandardDeck();
			Console.WriteLine("---------------- DECK1 DETAILS -----------------------");
			foreach (Card card in deck1.deckOfCards) 
			{
				Console.WriteLine(card.Face + card.Suit);
			}
			Console.WriteLine("---------------------------------------");
			Hand hand1 = new Hand("Hand 1 ", deck1, throwpile);
			deck1.MoveCardFromDeck(hand1, deck1.deckOfCards[0]);
			Console.WriteLine("-------------- DECK1 COUNT AFTER MOVING FIRST ITEM TO HAND1-------------------------");
			Console.WriteLine(deck1.deckOfCards.Count());
			Console.WriteLine("---------------------------------------");
			hand1.AddCard(card1);
			hand1.turnPICK = true;
			Console.WriteLine("----------------HAND 1 AFTER PICKING FROM DECK AND PAIRING-----------------------");
			hand1.pickCardFromDeckIndex(1);
			hand1.Pair(1, 2);
			foreach (Card card in hand1.deckOfCards) 
			{
				Console.WriteLine(card.Face + card.Suit);
			}
			Console.WriteLine("---------------- HAND 1 COUNT -----------------------");
			Console.WriteLine(hand1.deckOfCards.Count());
			Console.WriteLine("----------------- HAND 1 FIRST ELEMENT PAIR BOOL ----------------------");
			Console.WriteLine(hand1.deckOfCards[0].inPair);
			Console.WriteLine("------------------ CURRENT PAIRED ITEMS ---------------------");
			foreach (Tuple<Card, Card> tuple in hand1.Pairs)
			{
				Console.WriteLine(tuple.Item1.Face + tuple.Item2.Face);
			}
			hand1.turnTHROW = true;
			hand1.ThrowCard(2);
			Console.WriteLine("---------------- HAND1 AFTER THROW -----------------------");
			foreach (Card card in hand1.deckOfCards) 
			{
				Console.WriteLine(card.Face + card.Suit);
			}
			Console.WriteLine("---------------- HAND 1 COUNT AFTER THROW -----------------------");
			Console.WriteLine(hand1.deckOfCards.Count());
			Console.WriteLine("-----------------UPDATED THROWPILE----------------------");
			foreach (Card card in throwpile.deckOfCards) 
			{
				Console.WriteLine(card.Face + card.Suit);
			}
			Console.WriteLine("---------------CHECKING WHETHER THE THROWPILE AND HAND ELEMENTS ARE STILL IN PAIR------------------------");
			Console.WriteLine(throwpile.deckOfCards[0].inPair);
			Console.WriteLine(hand1.deckOfCards[0].inPair);
			Console.WriteLine(hand1.deckOfCards[^1].inPair);
			
			Console.WriteLine("---------------THROWING THE LAST ITEM AND CHECKING IF PAIR CHANGED-----------------------");
			hand1.ThrowCard(2);
			Console.WriteLine("hand 1: ");
			foreach (Card card in hand1.deckOfCards) 
			{
				Console.WriteLine(card.Face + card.Suit);
				Console.WriteLine(card.inPair);
			}
			Console.WriteLine("throwpile 1: ");
			foreach (Card card in throwpile.deckOfCards) 
			{
				Console.WriteLine(card.Face + card.Suit);
				Console.WriteLine(card.inPair);
			}
			
		}
	}
	
	
	/// Card class
	
	class Card 
	{
		public string Face;
		public char Suit;
		public bool inPair;
		public static readonly char[] StandardSuits = {'D', 'H', 'S', 'C'};
		public static readonly string[] StandardFaces = {"A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K"};
		
		public Card(int SuitIndex, int FaceIndex)
		{
			this.Suit = StandardSuits[SuitIndex];
			this.Face = StandardFaces[FaceIndex];
			this.inPair = false;
		}
		
		public Card() 
		{
			this.Suit = 'J';
			this.Face = "J";
			this.inPair = false;
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
		public List<Tuple<Card, Card>> Pairs = new List<Tuple<Card, Card>>();		
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
		public bool ThrowCard(int CardPosition)
		{
			if (turnTHROW == true)
			{
				Card card2throw = deckOfCards[CardPosition-1];
				if (card2throw.inPair == true) 
				{
					foreach(Tuple<Card, Card> cardTuple in Pairs) 
					{
						if ((cardTuple.Item1 == card2throw) || (cardTuple.Item2 == card2throw))
						{
							cardTuple.Item1.inPair = false;
							cardTuple.Item2.inPair = false;
							Pairs.Remove(cardTuple);
							MoveCardFromDeck(ThrowPile, card2throw);
							return true;
						}
					}
				}
				else
				{
					MoveCardFromDeck(ThrowPile, card2throw);
					turnTHROW = false;
					return true;
				}
			}
			return false;
		
		}
		
		public bool Pair(int CardPosition1, int CardPosition2)
		{
			if ((CardPosition1 > deckOfCards.Count()+1) || (CardPosition2 > deckOfCards.Count()+1) || (CardPosition1 == CardPosition2)) 
			{
				return false;
			}
			else 
			{
				Card card1 = deckOfCards[CardPosition1-1];
				Card card2 = deckOfCards[CardPosition2-1];
				if ((card1.Face == card2.Face) && (card1.inPair == false) && (card2.inPair == false))
				{
					Tuple<Card, Card> cardTuple = Tuple.Create(card1, card2);
					Pairs.Add(cardTuple);
					card1.inPair = true;
					card2.inPair = true;
					return true;
				}
				return false;
			}
		}
		
		
		
	}
}