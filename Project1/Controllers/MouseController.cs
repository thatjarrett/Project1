using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Project1.Interfaces;

namespace Project1.Controllers
{
    internal class MouseController : IController
    {
        public void Update()
        {
            MouseState mouseState = Mouse.GetState();
        }
    }
}
