using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/*

	Basic UI-Element class holding bounds and font.
	Version: 0.8
	Author : 2016 by Kim Oliver Schweikert
	License: CC-by

*/

namespace Uboot_Terrain
{
    //A basic UI-Element
    public class UIElement
    {
        //Font for this element
        protected SpriteFont font;
        public SpriteFont Font
        {
            get { return font; }
            set { font = value; }
        }

        //Bounds of this element
        protected Rectangle bounds;
        public Rectangle Bounds
        {
            get { return bounds; }
            set { bounds = value; }
        }

        //Create a very basic element that is just a rectangle
        public UIElement(Rectangle bounds)
        {
            this.bounds = bounds;
            this.font = null;
        }

        //Create a very basic element that has a rectangle and a font
        public UIElement(Rectangle bounds, SpriteFont font)
        {
            this.bounds = bounds;
            this.font = font;
        }
    }
}


