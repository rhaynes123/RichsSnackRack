using System;
namespace RichsSnackRack.Orders
{
    public abstract record Enumeration
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        protected Enumeration(int id, string name, string description) => (Id, Name, Description) = (id, name, description);

        public override string ToString() => Description;

    }
}

