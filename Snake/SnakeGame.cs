#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using Snake.Components;
using Snake.GameBase;
#endregion

namespace Snake {
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class SnakeGame : Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Grid grid;
        private Snake.Components.Snake snake;
        private Score score;
        private CollisionManager collisionManager;
        private GameContentManager contentManager;
        private GameState gameState;

        public SnakeGame()
            : base() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 640;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = 640;   // set this value to the desired height of your window
            graphics.ApplyChanges();
        }

        protected override void Initialize() {
            collisionManager = new CollisionManager();
            contentManager = new GameContentManager(new XnaContentManagerAdapter(Content));
            score = new Score(contentManager, 290, 250);
            snake = new Snake.Components.Snake(contentManager, 4, 4, 4, 1, 0);
            grid = new Grid(contentManager, new int[,] {
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 ,1, 1, 1, 1, 1, 1 }, 
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 }, 
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 }, 
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 }, 
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 }, 
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 }, 
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 }, 
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 }, 
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 }, 
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 }, 
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 }, 
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 }, 
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 }, 
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 }, 
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 }, 
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 }, 
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 }, 
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 }, 
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 }, 
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 ,1, 1, 1, 1, 1, 1 } 
            });
            grid.AddRandomPowerUp(snake.GetPositions());

            gameState = GameState.Playing;

            base.Initialize();
        }

        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            contentManager.Add<Texture2D>("darkblue");
            contentManager.Add<Texture2D>("darkgreen");
            contentManager.Add<Texture2D>("red");
            contentManager.Add<Texture2D>("lightblue");
            contentManager.Add<SpriteFont>("Consolas78");
        }

        protected override void UnloadContent() {
            contentManager.UnloadAll();
        }

        protected override void Update(GameTime gameTime) {
            if (gameState == GameState.Playing) {

                HandleInput(Keyboard.GetState());

                snake.Upate(gameTime);

                HandleCollisions();

                base.Update(gameTime);
            }
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            grid.Draw(spriteBatch);
            snake.Draw(spriteBatch);
            score.Draw(spriteBatch);

            if (gameState == GameState.GameOver)
                DrawGameOver();

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void HandleInput(KeyboardState keyState) {
            if (keyState.IsKeyDown(Keys.Escape))
                Exit();

            if (keyState.IsKeyDown(Keys.W)) {
                snake.ChangeDirection(0, -1);
            }
            else if (keyState.IsKeyDown(Keys.S)) {
                snake.ChangeDirection(0, 1);
            }
            else if (keyState.IsKeyDown(Keys.A)) {
                snake.ChangeDirection(-1, 0);
            }
            else if (keyState.IsKeyDown(Keys.D)) {
                snake.ChangeDirection(1, 0);
            }
        }

        private void HandleCollisions() {
            CollisionType collision = collisionManager.Collision(grid, snake);
            if (collision == CollisionType.PowerUp) {
                snake.AddTail();
                grid.Clear(snake.X, snake.Y);
                grid.AddRandomPowerUp(snake.GetPositions());
                score.Increase();
            }

            if (collision == CollisionType.Fatal) {
                gameState = GameState.GameOver;
            }
        }

        private void DrawGameOver() {
            spriteBatch.DrawString(contentManager.Get<SpriteFont>("Consolas78"), "GAME OVER", new Vector2(70, 150), Color.Black);
        }
    }
}
