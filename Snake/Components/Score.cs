using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Snake.GameBase;
using System;
namespace Snake.Components
{
    public class Score {
        private int score = 0;
        private readonly int x, y;
        private readonly GameContentManager contentManager;

        public Score(GameContentManager contentManager, int x, int y) {
            this.contentManager = contentManager;
            this.x = x;
            this.y = y;
        }

        public void Increase() {
            score++;
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.DrawString(contentManager.Get<SpriteFont>("Consolas78"), score.ToString(), new Vector2(x, y), Color.White);
        }
    }
}
