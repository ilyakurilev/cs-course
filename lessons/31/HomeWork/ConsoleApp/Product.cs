﻿namespace ConsoleApp
{
    public class Product
	{
		public int Id { get; }
		public string Name { get; }
		public decimal Price { get; }

		public Product(int id, string name, decimal price)
		{
			Id = id;
			Name = name;
			Price = price;
		}

		public override string ToString() =>
			$"Product with id: {Id} for {Price} with name: {Name}";
	}

}
