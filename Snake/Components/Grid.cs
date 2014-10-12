using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Snake.GameBase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Snake.Components {
    public class Grid {
        public const int EMPTY_TILE = 0;
        public const int WALL_TILE = 1;
        public const int POWERUP_TILE = 2;

        private int tileSizeX = 32, tileSizeY = 32;

        private readonly int[,] grid;
        private readonly Random random = new Random();

        private readonly GameContentManager contentManager;

        public Grid(GameContentManager contentManager, int x, int y) {
            grid = new int[x, y];
            this.contentManager = contentManager;
        }

        public Grid(GameContentManager contentManager, int[,] grid) {
            this.grid = grid;
            this.contentManager = contentManager;
        }

        public bool AddRandomPowerUp(List<Vector2> occupied) {
            List<Vector2> availablePositions = GetAvailablePositions(occupied);
            if (availablePositions.Count > 0) {
                AddPowerUp(availablePositions);
                return true;
            }
            return false;
        }

        private void AddPowerUp(List<Vector2> availablePositions) {
            Vector2 pos = availablePositions[random.Next(availablePositions.Count - 1)];
            grid[(int)pos.X, (int)pos.Y] = POWERUP_TILE;
        }

        private List<Vector2> GetAvailablePositions(List<Vector2> occupied) {
            List<Vector2> positions = new List<Vector2>();
            for (int i = 0; i <= grid.GetUpperBound(0); i++) {
                for (int j = 0; j <= grid.GetUpperBound(1); j++) {
                    if (grid[i, j] == 0 && !occupied.Any(o => o.X == i && o.Y == j))
                        positions.Add(new Vector2 { X = i, Y = j });
                }
            }
            return positions;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int y = 0; y <= grid.GetUpperBound(1); y++) {
                for (int x = 0; x <= grid.GetUpperBound(0); x++) {
                    int value = grid[x, y];
                    DrawTile(value, x * tileSizeX, y * tileSizeY, spriteBatch);
                }
            }
        }

        private void DrawTile(int value, int x, int y, SpriteBatch spriteBatch) {
            if (value == POWERUP_TILE)
                spriteBatch.Draw(contentManager.Get<Texture2D>("red"), new Vector2(x, y), Color.Red);
            else if (value == WALL_TILE)
                spriteBatch.Draw(contentManager.Get<Texture2D>("darkblue"), new Vector2(x, y), Color.DarkBlue);
        }

        public void Clear(int x, int y) {
            grid[x, y] = EMPTY_TILE;
        }

        public int Tile(int x, int y) {
            return grid[x, y];
        }
    }
}
