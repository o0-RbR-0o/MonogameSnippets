using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

/*

	Simple Textured / Font Button class for generating user inferfaces.
	Version: 0.3
	Author : 2016 by Kim Oliver Schweikert
	License: CC-by

*/

namespace UserInterfaces
{
    //A Button :)
    public class UIButton : UIElement
    {
        public enum Status { Idle, MouseOver, Pressed };

        //Current state
        Status State = Status.Idle;
        public Status State1
        {
            get { return State; }
            set { State = value; }
        }

        //Action to invoke with this button
        Action action;
        public Action Action
        {
            get { return action; }
            set { action = value; }
        }

        //Mouse states
        MouseState mstate, mstate_old;
        public MouseState Mstate
        {
            get { return mstate; }
            set { mstate = value; }
        }

        //Texture for this Button when idle
        Texture2D texture_idle;
        public Texture2D Texture_idle
        {
            get { return texture_idle; }
            set { texture_idle = value; }
        }

        //Texture for this button when pressed
        Texture2D texture_pressed;
        public Texture2D Texture_pressed
        {
            get { return texture_pressed; }
            set { texture_pressed = value; }
        }

        //Texture for this button on MouseOver
        Texture2D texture_mouseover;
        public Texture2D Texture_mouseover
        {
            get { return texture_mouseover; }
            set { texture_mouseover = value; }
        }

        //Color of font in idle state
        Color color_font_idle;
        public Color Color_font_idle
        {
            get { return color_font_idle; }
            set { color_font_idle = value; }
        }

        //Color of font in pressed state
        Color color_font_pressed;
        public Color Color_font_pressed
        {
            get { return color_font_pressed; }
            set { color_font_pressed = value; }
        }

        //Color of font on MouseOver
        Color color_font_mouseover;
        public Color Color_font_mouseover
        {
            get { return color_font_mouseover; }
            set { color_font_mouseover = value; }
        }

        //Scalefactor of texture in idle state
        float scale_tex_idle;
        public float Scale_tex_idle
        {
            get { return scale_tex_idle; }
            set { scale_tex_idle = value; }
        }

        //Scalefactor of texture in pressed state
        float scalefactor_tex_pressed;
        public float Scalefactor_tex_pressed
        {
            get { return scalefactor_tex_pressed; }
            set { scalefactor_tex_pressed = value; }
        }

        //Scalefactor of texture on MouseOver
        float scalefactor_tex_mouseover;
        public float Scalefactor_tex_mouseover
        {
            get { return scalefactor_tex_mouseover; }
            set { scalefactor_tex_mouseover = value; }
        }

        //Scalefactor of font in idle state
        float scale_font_idle;
        public float Scale_font_idle
        {
            get { return scale_font_idle; }
            set { scale_font_idle = value; }
        }

        //Scalefactor of font in pressed state
        float scalefactor_font_pressed;
        public float Scalefactor_font_pressed
        {
            get { return scalefactor_font_pressed; }
            set { scalefactor_font_pressed = value; }
        }

        //Scalefactor of font MouseOver
        float scalefactor_font_mouseover;
        public float Scalefactor_font_mouseover
        {
            get { return scalefactor_font_mouseover; }
            set { scalefactor_font_mouseover = value; }
        }

        //Use font?
        bool use_font;

        //Use textures?
        bool use_textures;

        //Font-Only-Button
        public UIButton(Rectangle bounds, SpriteFont font, Color font_color_idle, Color font_color_pressed, Color font_color_mouseover, Action a) : base(bounds, font){
            this.use_textures=false;
            this.use_font=true;

            this.color_font_idle=font_color_idle;
            this.color_font_mouseover=font_color_mouseover;
            this.color_font_pressed=font_color_pressed;
            this.scale_font_idle=1.0f;
            this.scalefactor_font_mouseover=1.0f;
            this.scalefactor_font_pressed=1.0f;

            this.action=a;

        }

        //Texture-Only-Button
        public UIButton(Rectangle bounds, Texture2D button_tex_idle, Texture2D button_tex_pressed, Texture2D button_tex_mouseover, Action a) : base(bounds, null){
            this.use_textures=true;
            this.use_font=false;

            this.texture_idle=button_tex_idle;
            this.texture_pressed=button_tex_pressed;
            this.texture_mouseover=button_tex_mouseover;
            this.scalefactor_tex_mouseover=1.0f;
            this.scalefactor_tex_pressed=1.0f;

            this.action=a;
        }

        //Texture-and-Font-Button
        public UIButton(Rectangle bounds, Texture2D button_tex_idle, Texture2D button_tex_pressed, Texture2D button_tex_mouseover,SpriteFont font, Color font_color_idle, Color font_color_pressed, Color font_color_mouseover, Action a) :this(bounds,button_tex_idle,button_tex_pressed,button_tex_mouseover,a){
            this.color_font_idle=font_color_idle;
            this.color_font_mouseover=font_color_mouseover;
            this.color_font_pressed=font_color_pressed;
            this.scale_font_idle=1.0f;
            this.scalefactor_font_mouseover=1.0f;
            this.scalefactor_font_pressed=1.0f;

            this.action=a;

            this.use_textures=true;
            this.use_font=true;
        }

        //Checks for mouse click and invokes the method of this button
        public void work()
        {
            this.mstate = Mouse.GetState();

            //Mouse collides with this button
            if (this.bounds.Intersects(new Rectangle(this.mstate.Position, new Point(1, 1))))
            {
                this.State = Status.MouseOver;

                //Button is pressed and mouse is colliding
                if (this.mstate.LeftButton == ButtonState.Pressed && this.mstate.LeftButton != this.mstate_old.LeftButton)
                {
                    this.State = Status.Pressed;
                    this.action.Invoke();
                }
            }
            //No collision with mouse
            else
            {
                this.State = Status.Idle;
            }
            this.mstate_old = this.mstate;
        }

        //Draw the menu - This doesn't take care of the centered option yet. Depends of where i put that part...
        public void draw(SpriteBatch sb)
        {
            //Todo: Add drawing code
        }
    }
}

