using Microsoft.Xna.Framework.Graphics;
using System;
namespace Snake.Components
{
    public class Score {
        private int score = 0;
        private readonly int x, y;

        public Score(int x, int y) {
            this.x = x;
            this.y = y;
        }

        public void Increase() {
            score += 1;
        }

        public void Draw(SpriteBatch spriteBatch) {
            throw new NotImplementedException();
            //renderEngine.Draw(x, y, "Score: " + score);
        }
    }
}
