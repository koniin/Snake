using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake {
    public class ContentManager {
        private Dictionary<string, ContentContainer> content = new Dictionary<string, ContentContainer>();
        private Microsoft.Xna.Framework.Content.ContentManager xnaContentManager;

        public void LoadAllContent(Microsoft.Xna.Framework.Content.ContentManager content) {
            xnaContentManager = content;

            Add<Texture2D>("darkblue");
            Add<Texture2D>("darkgreen");
            Add<Texture2D>("red");
            Add<Texture2D>("lightblue");
            Add<SpriteFont>("Consolas78");
        }

        public T Get<T>(string contentId) {
            return (T)content[contentId].Content;
        }

        internal void UnloadAll() {
        /*
            foreach (var key in content.Keys) {
                ContentContainer container = content[key];
                Unload((typeof(container.Type))container.Content);
            }
         * */
            content = null;
        }

        private void Add<T>(string name) {
            content.Add(name, Create(name, typeof(T), xnaContentManager.Load<T>(name)));
        }

        private ContentContainer Create(string name, Type type, object content) {
            return new ContentContainer { Name = name, Type = type, Content = content };
        }

        private struct ContentContainer {
            public Type Type;
            public string Name;
            public object Content;
        }
    }
}
