using System;


public class Player
{
    private string name;
    private byte health = 100;
    private int score = 0;
    private string spell = "NONE";

    public Player(string name)
    {
        this.name = name;
    }
}