﻿#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using Snake.Components;
#endregion

namespace Snake {
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Grid grid;
        private Snake.Components.Snake snake;
        private Score score;
        private CollisionManager collisionManager;

        public Game1()
            : base() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            collisionManager = new CollisionManager();
            score = new Score(0, 12);
            grid = new Grid(new int[,] {
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, 
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 }, 
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 }, 
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 }, 
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 }, 
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 }, 
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 }, 
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 }, 
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 }, 
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 } 
            });
            snake = new Snake.Components.Snake(4, 4, 4, 1, 0);

            grid.AddRandomPowerUp(snake.GetPositions());

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            snake.Upate();
            
            HandleCollisions();
            
            base.Update(gameTime);
        }

        private void HandleCollisions() {
            CollisionType collision = collisionManager.Collision(grid, snake);
            if (collision == CollisionType.PowerUp) {
                snake.AddTail();
                grid.Clear(snake.X, snake.Y);
                grid.AddRandomPowerUp(snake.GetPositions());
                score.Increase();
            }

            if (collision == CollisionType.Fatal)
                throw new NotImplementedException("Fatal collision - GAME OVER");
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            grid.Draw(spriteBatch);
            snake.Draw(spriteBatch);
            score.Draw(spriteBatch);

            base.Draw(gameTime);
        }
    }
}