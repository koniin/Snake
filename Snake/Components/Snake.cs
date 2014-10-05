using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Snake.Components
{
    public class Snake {
        private readonly SortedList<int, SnakePart> tail;
        private readonly Queue<int> newParts; 
        private SnakePart Head { get; set; }
        private int currentXDirection;
        private int currentYDirection;

        public int X { get { return Head.X; } }
        public int Y { get { return Head.Y; } }

        public Snake(int xStart, int yStart, int initialLength, int xDirection, int yDirection) {
            Head = CreateHead(xStart, yStart);
            ChangeDirection(xDirection, yDirection);
            tail = new SortedList<int, SnakePart>();
            for (int i = 1; i < initialLength; i++) {
                AddTail(Head.X - i, Head.Y);
            }

            newParts = new Queue<int>();
        }
        
        public void Upate() {
            int nextX = Head.X, nextY = Head.Y;
            Head.X += currentXDirection;
            Head.Y += currentYDirection;
            foreach (var part in tail) {
                int lastX = part.Value.X, lastY = part.Value.Y;
                part.Value.X = nextX;
                part.Value.Y = nextY;
                nextX = lastX;
                nextY = lastY;
            }
            if (newParts.Count > 0) {
                AddTail(nextX, nextY);
                newParts.Dequeue();
            }
        }

        public void Draw(SpriteBatch spriteBatch) {
            throw new NotImplementedException();
            /*
            Head.Draw(renderEngine);
            foreach (var part in tail) {
                part.Value.Draw(renderEngine);
            }*/
        }

        public void ChangeDirection(int xDirection, int yDirection) {
            currentXDirection = xDirection;
            currentYDirection = yDirection;
        }

        public void AddTail() {
            newParts.Enqueue(1);
        }

        public List<Vector2> GetPositions() {
            List<Vector2> positions = tail.Select(t => new Vector2 { X = t.Value.X, Y = t.Value.Y }).ToList();
            positions.Add(new Vector2 { X = Head.X, Y = Head.Y });
            return positions;
        }

        public bool InTail(int x, int y) {
            return tail.Any(t => t.Value.X == x && t.Value.Y == y);
        }

        private void AddTail(int x, int y) {
            tail.Add(tail.Count + 1, CreateTail(x, y));
        }

        private SnakePart CreateHead(int x, int y) {
            return new SnakePart {
                X = x,
                Y = y,
                Texture = "o"
            };
        }

        private SnakePart CreateTail(int x, int y) {
            return new SnakePart {
                X = x,
                Y = y,
                Texture = "x"
            };
        }

        private class SnakePart {
            public int X { get; set; }
            public int Y { get; set; }
            public string Texture { private get; set; }

            public void Draw() {
                throw new NotImplementedException();
            }
        }
    }
}
