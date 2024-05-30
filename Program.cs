using System;
using SplashKitSDK;
public class Program
{
    public static void Main()
    {
        Window gameWindow = new Window("Animation with Sprite", 620, 420);

        // Create the base ground
        Rectangle ground = new Rectangle
        {
            X = 0,
            Y = 380,
            Width = gameWindow.Width,
            Height = 40
        };
        // Create player
        Player player = new Player(gameWindow, ground);

        // Start game loop
        while (!gameWindow.CloseRequested)
        {
            SplashKit.ProcessEvents();
            player.HandleInput();
            gameWindow.Clear(Color.DarkGray);
            player.Update(ground);
            player.Draw();
            gameWindow.FillRectangle(Color.DarkSlateGray, ground);
            gameWindow.Refresh(60);
        }
    }
}
