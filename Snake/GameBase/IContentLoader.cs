using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake.GameBase {
    public interface IContentLoader {
        T Load<T>(string contentId);
        void Unload();
    }
}
