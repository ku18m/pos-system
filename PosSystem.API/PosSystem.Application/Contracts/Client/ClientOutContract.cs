﻿namespace PosSystem.Application.Contracts.Client
{
    public class ClientOutContract
    {
        public string Id { get; set; }
        public int Number { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}
