﻿using Shop.Domain.ValueObjects;

namespace Shop.WebUI.Entities
{
    public class Cart : ICart<int, decimal>
    {
        public int GoodId { get; set; }
        public decimal GoodCount { get; set; }
    }
}