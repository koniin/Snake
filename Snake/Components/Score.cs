using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
namespace Snake.Components
{
    public class Score {
        private int score = 0;
        private readonly int x, y;
        private readonly ContentManager contentManager;

        public Score(ContentManager contentManager, int x, int y) {
            this.contentManager = contentManager;
            this.x = x;
            this.y = y;
        }

        public void Increase() {
            score += 1;
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.DrawString(contentManager.Get<SpriteFont>("Consolas78"), score.ToString(), new Vector2(x, y), Color.White);
        }
    }
}
