using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake.GameBase {
    public class XnaContentManagerAdapter : IContentLoader {
        private ContentManager contentManager;
        public XnaContentManagerAdapter(ContentManager contentManager) {
            this.contentManager = contentManager;
        }

        public T Load<T>(string contentId) {
            return contentManager.Load<T>(contentId);
        }

        public void Unload() {
            contentManager.Unload();
        }
    }
}
