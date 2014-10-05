using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Snake.Components
{
    public class Grid
    {
        public const int EMPTY_TILE = 0;
        public const int WALL_TILE = 1;
        public const int POWERUP_TILE = 2;
        
        private readonly int[,] grid;
        private readonly Random random = new Random();

        public Grid(int x, int y) {
            grid = new int[x, y];
        }

        public Grid(int[,] grid) {
            this.grid = grid;
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
            for (int i = 0; i <= grid.GetUpperBound(0); i++) {
                for (int j = 0; j <= grid.GetUpperBound(1); j++) {
                    int value = grid[i, j];
                    DrawTile(value, i, j, spriteBatch);
                }
            }
        }

        private void DrawTile(int value, int x, int y, SpriteBatch spriteBatch) {
            throw new NotImplementedException();
            /*
            Color color = Color.White;
            if (value == EMPTY_TILE)
                color = Color.White;
            else if (value == POWERUP_TILE)
                color = Color.Green;
            else if (value == WALL_TILE)
                color = Color.Blue;

            renderEngine.Draw(x, y, value.ToString(), color);
             * */
        }

        public void Clear(int x, int y) {
            grid[x, y] = EMPTY_TILE;
        }

        public int Tile(int x, int y) {
            return grid[x, y];
        }
    }
}
