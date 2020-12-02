using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    public class InsertOrderCommand
	{
		public int CustomerId { get; set; }
		public DateTimeOffset OrderDate { get; set; }
		public double? Discount { get; set; }
		public List<(int productId, int count)> Lines { get; set; }

		public InsertOrderCommand(int customerId, double? discount = default)
		{
			CustomerId = customerId;
			OrderDate = DateTimeOffset.UtcNow;
			Discount = discount;
			Lines = new List<(int productId, int count)>();
		}
	}

}
