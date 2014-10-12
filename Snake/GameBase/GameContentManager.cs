using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake.GameBase {
    public class GameContentManager {
        private Dictionary<string, ContentContainer> content = new Dictionary<string, ContentContainer>();
        private IContentLoader contentManager;

        public GameContentManager(IContentLoader content) {
            contentManager = content;
        }

        public void UnloadAll() {
            for (int i = 0; i < content.Keys.Count; i++)
                Unload(content.Keys.ElementAt(i));
            
            content = null;
            contentManager.Unload();
        }

        public T Get<T>(string contentId) {
            return (T)content[contentId].Content;
        }

        public void Unload(string contentId) {
            Unload(content[contentId]);
            content.Remove(contentId);
        }

        public void Add<T>(string name) {
            content.Add(name, Create(name, typeof(T), contentManager.Load<T>(name)));
        }

        private void Unload(ContentContainer contentContainer) {
            contentContainer.Content = null;
            contentContainer.Name = null;
            contentContainer.Type = null;
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
