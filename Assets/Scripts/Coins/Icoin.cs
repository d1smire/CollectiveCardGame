interface Icoin 
{
    string name { get; }
    int amount { get; }

    void AddCoins(int amount);

    void RemoveCoins(int amount);

}
